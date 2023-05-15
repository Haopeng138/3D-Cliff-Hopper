using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public struct TileWeight
{
    public BaseTileSO tile;
    public int weight;
};


[CreateAssetMenu(fileName = "BaseTileSO", menuName = "3D-Cliff-Hopper/BaseTileSO", order = 0)]
public class BaseTileSO : ScriptableObject {

    // Max Y level to spawn scaffold tiles
    public static int depth = -4;
    
   // Top tile - CANNOR BE NULL
    public GameObject tileGO;
    // Tiles to fill bellow - Null if no scaffold
    public GameObject tileScaffoldGO;
    public float scaffoldHeight = 1f;
    //Movement to/from center of edge (2D -> X, Y))
    public Vector3 nextTileStride = new Vector2(1f,0f);
    public Vector3 localOffset = Vector3.zero;
    public Quaternion tileRotation = Quaternion.identity;
    public bool canBeReplaced = true;

    [SerializeField]
    public List<TileWeight> tileWeights;

    
    
    public BaseTileSO nextTile(TileMapController tileMapController) {
        int totalWeight = 0;
        bool mustBeSafe = tileMapController.reachedMaxOffset();
        foreach (TileWeight tileOption in tileWeights) totalWeight += tileOption.weight;

        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;
        foreach (TileWeight tileOption in tileWeights)
        {
            currentWeight += tileOption.weight;
            float yStride = tileOption.tile.nextTileStride.y;

            if (tileMapController.currentHeight + yStride <= depth || tileMapController.currentHeight + yStride >= tileMapController.maxHeight) Debug.Log("Can't go any lower"); 
            else if (randomWeight < currentWeight)
            {
                if (mustBeSafe && !tileOption.tile.canBeReplaced)
                {
                    Debug.Log("Selected unsafe tile, rerolling");
                }
                else return tileOption.tile;
            }
        }
        
        Debug.Log("No tile selected, returning first safe tile");
        foreach (TileWeight tile in tileWeights) if (tile.tile.canBeReplaced) return tile.tile;
        
        Debug.Log("[WARN] No safe tiles available, returning first tile");
        return tileWeights[0].tile;
    }



    public BaseTile spawnTile(Vector3 tileLocation, Direction currentDirection, TileMapController tileMapController){
        GameObject newTile = Instantiate(tileGO, tileLocation + localOffset, tileRotation);
        BaseTile tile = newTile.GetComponent<BaseTile>();
        tile.tileOrientation = currentDirection;
        tile.tileMapController = tileMapController;
        
        if (tileScaffoldGO != null){
            for (float y = tile.transform.position.y - 1; y > depth; y = y - scaffoldHeight){
                Instantiate(tileScaffoldGO, new Vector3(tileLocation.x, y, tileLocation.z), tileRotation);
            }
        }
        
        return tile;
    }
}
