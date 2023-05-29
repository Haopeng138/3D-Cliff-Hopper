using System.Collections.Generic;
using UnityEngine;

public class TriggerComponent : MonoBehaviour {
    [SerializeField]
    BaseTile tile;

    public void init(BaseTile tile){
        this.tile = tile;
    }
    
    private void OnTriggerEnter(Collider other) {
        EntityController entity = other.GetComponent<EntityController>();
        if (entity == null) Debug.LogError("[TriggerComponent] No entity controller found");
        else if (tile != null) {
            Debug.Log("[TriggerComponent] " + tile);
            tile.onTriggerEnter(entity);
        } else Debug.LogError("[TriggerComponent] Tile is null");    
    }

    private void OnTriggerExit(Collider other) {
        EntityController entity = other.GetComponent<EntityController>();
        if (entity == null) Debug.LogError("[TriggerComponent] No entity controller found");
        else if (tile != null) {
            Debug.Log("[TriggerComponent] " + tile);
            tile.onTriggerExit(entity);
        } else Debug.LogError("[TriggerComponent] Tile is null");
    }

}