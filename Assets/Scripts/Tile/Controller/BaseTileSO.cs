using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public struct TileWeight
{
    public BaseTileSO tile;
    public int weight;
    [Tooltip("If true, this tileOtion can be replaced by Rotation Tile")]
    public bool canBeRotate; // IDK NAME YET
};


[CreateAssetMenu(fileName = "BaseTileSO", menuName = "Tile/BaseTile", order = 0)]
public class BaseTileSO : ScriptableObject {

    // Max Y level to spawn scaffold tiles
    public static int depth = -5;
    
   // Top tile - CANNOR BE NULL
   [Header("Tile")]
    public GameObject tilePrefab;
    // Tiles to fill bellow - Null if no scaffold
    public GameObject tileScaffold;
    public float scaffoldHeight = 1f;
    [Header("Tile Settings")]
    //Movement to/from center of edge (2D -> X, Y))
    [Tooltip("Movement to/from center to edge (2D -> X/Z, Y)")]
    public Vector2 nextTileStride = new Vector2(.5f,0f);
    [Tooltip("Offset from tile position to tile prefab position")]
    public Vector3 localOffset = Vector3.zero;
    public Quaternion tileRotation = Quaternion.identity;
    public bool isTrap = false;

    [SerializeField]
    
    private List<TileWeight> tileWeights;

    
    
    public BaseTileSO nextTile(TileMapController tileMapController) {

        bool reachedMaxHeight = tileMapController.ReachedMaxHeight;
        bool ReachedMinHeight = tileMapController.ReachedMinHeight;
        bool reachedMaxOffset = tileMapController.ReachedMaxOffset;
        
        // Random indication to rotate tile ("optional")
        bool rotateRoll = tileMapController.rollRotateTileByDistance();
        
        
        int totalWeight = 0;
        foreach (TileWeight tileOption in tileWeights) totalWeight += tileOption.weight;

        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (TileWeight tileOption in tileWeights)
        {
            currentWeight += tileOption.weight;
            
            float yStride = tileOption.tile.nextTileStride.y;

            if (currentWeight > randomWeight) // TileOptionSelected
            {
                if (reachedMaxHeight && yStride > 0) continue;
                if (ReachedMinHeight && yStride < 0) continue;
                if ((reachedMaxOffset || rotateRoll) && tileOption.canBeRotate) 
                    return tileMapController.changeDirection();
                return tileOption.tile;
            }
        }

        Debug.LogError("No tile selected: ReachedMaxOffset?: " + reachedMaxOffset, this);
        if (reachedMaxOffset) foreach (TileWeight tileOption in tileWeights) if (tileOption.canBeRotate) return tileMapController.changeDirection();
        Debug.LogError("No tile selected, returning first tile!", this);
        return tileWeights[0].tile;
    }



    public BaseTile spawnTile(Vector3 tileLocation, Direction currentDirection, TileMapController tileMapController){
        GameObject newTile = Instantiate(tilePrefab, tileLocation + localOffset, tileRotation);
        BaseTile tile = newTile.GetComponentInChildren<BaseTile>();
        tile.tileOrientation = currentDirection;

       
        tile.tileMapController = tileMapController;
        
        if (tileScaffold != null){
            for (float y = tile.transform.position.y - 1; y > depth; y = y - scaffoldHeight){
                GameObject scaffold = Instantiate(tileScaffold, new Vector3(tileLocation.x, y, tileLocation.z), tileRotation);
                
                scaffold.transform.parent = tile.transform;

                var colliders = scaffold.GetComponents<Collider>();
                foreach(var collider in colliders) collider.enabled = false;

            }
        }
        
        return tile;
    }
}
