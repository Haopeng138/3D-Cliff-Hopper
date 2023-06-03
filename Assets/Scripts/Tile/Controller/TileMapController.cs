using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Direction { Z = +1, X = -1 };


public class TileMapController : MonoBehaviour
{
    public bool debug = false;
    public int maxTiles;
    public Vector3 tileSpawnLocation;
    [Header("Water")]
    public GameObject water;

    [Header("Tile Types")]
    public BaseTileSO currentTile;
    public BaseTileSO rotateTile;
    public BaseTileSO defaultTile;
    [Header("Coin")]
    public GameObject coin;
    public int coinSpawnTileCount = 10;

    // Direction of the next tile
    Direction currentDirection = Direction.Z;

// Offset from center of the map
    // Current center offset from (0,0)
    private int centerOffset = 0;
    public int centerOffsetMin = 2;
    public int centerOffsetMax = 4;
    public bool ReachedMaxOffset { get {return centerOffset * (int)currentDirection > centerOffsetMax; } }


// HEIGHT
    private int currentHeight = 0;
    public int maxHeight = 10;
    public int minHeight = -3;


    public bool ReachedMaxHeight { get { return currentHeight >= maxHeight; } }
    public bool ReachedMinHeight { get { return currentHeight <= minHeight; } }

    // Start is called before the first frame update
    void Start()
    { 
        if (water == null) water = GameObject.Find("WaterPosition");
        tileSpawnLocation = transform.position;
        centerOffset = (int)(tileSpawnLocation.z - tileSpawnLocation.x);
        currentDirection = centerOffset > 0 ? Direction.X : Direction.Z;
                
        CreateMap();

        print("Water");
        print(tileSpawnLocation);
        float xz = Mathf.Max(Mathf.Abs(tileSpawnLocation.x), Mathf.Abs(tileSpawnLocation.z))/2;
        Vector3 waterPosition = new Vector3(xz, minHeight, xz);
        print (waterPosition);
        water.transform.position = waterPosition;
        water.transform.localScale = new Vector3(xz, 1, xz);
        print (water.transform.position);
        print(water.transform.localScale);
        water.SetActive(true);
    }


    void CreateMap(){
        
        // Spawn first tile
        spawnCurrentTile();
        for (int i = 0; i < maxTiles; i++)
        {
            nextTile();    
            centerOffset += (int)currentDirection;
            spawnCurrentTile();

            if (i % coinSpawnTileCount == 9) spawnCoint();
        }

    }

    void nextTile(){
        addStride(false);
        currentTile = currentTile.nextTile(this);
        if (currentTile == rotateTile){
            if(debug) Debug.Log("Rotate Tile", currentTile);
        } 
        else addStride(true);
    
    }

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
