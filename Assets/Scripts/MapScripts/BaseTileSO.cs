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
   
    public GameObject tileGO;
    public float tileStride = 0.5f;
    public Vector3 offset = Vector3.zero;
    public Quaternion tileRotation = Quaternion.identity;
    public bool isSafe = true;

    [SerializeField]
    public List<TileWeight> tileWeights;

    
    
    public BaseTileSO nextTile(TileMapController tileMapController) {
        int totalWeight = 0;
        bool mustBeSafe = tileMapController.reachedMaxOffset() || !tileMapController.currentTile.isSafe;
        foreach (TileWeight tileOption in tileWeights) totalWeight += tileOption.weight;

        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;
        foreach (TileWeight tileOption in tileWeights)
        {
            currentWeight += tileOption.weight;
            if (randomWeight < currentWeight)
            {
                if (mustBeSafe && !tileOption.tile.isSafe)
                {
                    Debug.Log("Selected unsafe tile, rerolling");
                }
                else return tileOption.tile;
            }
        }
        
        Debug.Log("No tile selected, returning first safe tile");
        foreach (TileWeight tile in tileWeights) if (tile.tile.isSafe) return tile.tile;
        
        Debug.Log("[WARN] No safe tiles available, returning first tile");
        return tileWeights[0].tile;
    }



    public BaseTile spawnTile(Vector3 tileLocation, Direction currentDirection){
        GameObject newTile = Instantiate(tileGO, tileLocation + offset, tileRotation);
        BaseTile tile = newTile.GetComponent<BaseTile>();
        tile.tileOrientation = currentDirection;
        return tile;
    }
}
