using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Direction { Z = +1, X = -1 };


public class TileMapController : MonoBehaviour
{

    #region Variables
    public bool debug = false;
    private Vector3 tileSpawnLocation;
    
    [Header("Water")]
    public GameObject water;
    private GameObject waterParent;
    [Tooltip("Distance between water tiles (waterTileWidth)")]
    public float waterOffset = 10f;
    [Tooltip("Numer of diagonal water tiles to spawn")]
    public int waterWidth = 6; 

    [Header("Tile Types")]
    public BaseTileSO rotateTile;
    public BaseTileSO defaultTile;
    private BaseTileSO currentTile;

    [Header("Coin")]
    public GameObject coin;
    [Tooltip("Number of tiles between coins")]
    public int coinSpawnTileCount = 10;
    [Space]

    // Direction of the next tile
    Direction currentDirection = Direction.Z;
    [Header("Spawner Options")]
    [Tooltip("Number of tiles to spawn at start")]
    public int initialTileCount;

    [Header("Center Offset Settings")]
    // Current center offset from (X = Z)
    private int tileCount = 0;
    private int centerOffset = 0;
    public int centerOffsetMin = 2;
    public int centerOffsetMax = 4;
    public bool ReachedMaxOffset { get {return centerOffset * (int)currentDirection > centerOffsetMax; } }


    [Header("Height")]
    private int currentHeight = 0;
    public int maxHeight = 10;
    public int minHeight = -3;

    public bool ReachedMaxHeight { get { return currentHeight >= maxHeight; } }
    public bool ReachedMinHeight { get { return currentHeight <= minHeight; } }

    #endregion

    void Start()
    { 
        waterParent = new GameObject("WaterParent");
        waterParent.transform.parent = gameObject.transform;
        
        for (tileCount = -60; tileCount < 0; tileCount = tileCount + 2*(int)waterOffset) SpawnWater();
        
        tileSpawnLocation = transform.position;
        centerOffset = (int)(tileSpawnLocation.z - tileSpawnLocation.x);
        currentDirection = centerOffset > 0 ? Direction.X : Direction.Z;
        
        currentTile = defaultTile;
        
        CreateMap();
        
    }

    // Spawns initial tiles determined by initialTileCount
    void CreateMap(){
        tileCount = 0;
        // Spawn first tile
        spawnCurrentTile();
        
        for (int i = 0; i < initialTileCount; i++)
            NexTile();
    }

    // Iterates next tile and spawns it
    public void NexTile()
    {
        getNextTile();    
        centerOffset += (int)currentDirection;
        spawnCurrentTile();

        if (tileCount % coinSpawnTileCount == 9) spawnCoint();
        SpawnWater();

        tileCount++;
    }

    // Spawns diagonal water tiles
    void SpawnWater(){
        Debug.Log("Spawn Water: " + tileCount, this);
        float tilesBetweenDiagonals = waterOffset*2;
        int offset = (int)(tileCount % tilesBetweenDiagonals);
        if (offset != 0) 
            return; 
    
        Vector3 referencePosition = new Vector3(tileCount/tilesBetweenDiagonals, 0, tileCount/tilesBetweenDiagonals);
    
        for (int i = 0; i < 2; i++) {
            for (int j = -waterWidth/2; j < waterWidth/2 + waterWidth%2 ; j++) {
                int x = - (j);
                int z = j + i;
                Vector3 waterPosition = (referencePosition + new Vector3 (x, 0, z) ) * (waterOffset);
                waterPosition.y = minHeight;
                GameObject w = Instantiate(water, waterPosition, Quaternion.identity);

                w.name = "Water " + waterPosition.x + " " + waterPosition.z;
                w.transform.parent = waterParent.transform;
            }
        }
    }

    // Setst currentTile to the next tile in the tileMapController (based on previous tile's nextTile() function)
    void getNextTile(){
        addStride(false);
        currentTile = currentTile.nextTile(this);
        if (currentTile == rotateTile){
            if(debug) Debug.Log("Rotate Tile", currentTile);
        } 
        else addStride(true);
    
    }

    // Spawns the current tile in the tileMapController
    BaseTile spawnCurrentTile(){
        BaseTile newTile = currentTile.spawnTile(tileSpawnLocation, currentDirection, this);
        newTile.offset = centerOffset;
        newTile.gameObject.transform.parent.transform.parent = gameObject.transform;
        return newTile;
    }

    void spawnCoint(){
        Vector3 coinPosition = tileSpawnLocation;
        coinPosition.y += 1.5f;
        Instantiate(coin, coinPosition, Quaternion.identity);
    }

    // Adds TileStride to the tileSpawnLocation (offset from tile to edge and from edge to tile)
    void addStride(bool height){
        switch(currentDirection) 
        {
        case Direction.Z:
            tileSpawnLocation.z += currentTile.nextTileStride.x;
        break;
        case Direction.X:
            tileSpawnLocation.x += currentTile.nextTileStride.x;
        break;
        }

        if (height) {
            tileSpawnLocation.y += currentTile.nextTileStride.y;
            currentHeight += (int)currentTile.nextTileStride.y;
        }
    }

    // Changes the direction of the next tile in the controller 
    // Returns a tile that allows Rotation the tile to be rotated
    public BaseTileSO changeDirection(){
        addStride(false);
        switch(currentDirection) 
        {
        case Direction.Z:
            currentDirection = Direction.X;
        break;
        case Direction.X:
            currentDirection = Direction.Z;
        break;
        }
        return rotateTile;
    }

    // Roll for "rotateTile" in relation to centerOffset
    // Return -> TRUE: Rotate, FALSE: Don't Rotate
    public bool rollRotateTileByDistance(){
        //if (!currentTile.canBeReplaced) return false; 
        if (ReachedMaxOffset) return true;

        bool roll = false;

        if (currentDirection == Direction.X) {
            roll = centerOffset < Random.Range(-centerOffsetMax, -centerOffsetMin);
        } else {
            roll = centerOffset > Random.Range(centerOffsetMin, centerOffsetMax) ;

        }

        return roll;
    }


}
