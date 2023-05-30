using System.Collections.Generic;
using UnityEngine;

public class TriggerComponent : MonoBehaviour {
    [SerializeField]
    BaseTile tile;
    bool debug = false;

    public void init(BaseTile tile, bool debug = false){
        this.tile = tile;
        this.debug = debug;
    }
    
    private void OnTriggerEnter(Collider other) {
        EntityController entity = other.GetComponent<EntityController>();
        if (entity == null) Debug.LogError("[TriggerComponent] No entity controller found", this);
        else if (tile != null) {
            if (debug) Debug.Log("[TriggerComponent] " + tile.transform.parent, this);
            tile.onTriggerEnter(entity);
        } else Debug.LogError("[TriggerComponent] Tile is null", this);    
    }

    private void OnTriggerExit(Collider other) {
        EntityController entity = other.GetComponent<EntityController>();
        if (entity == null) Debug.LogError("[TriggerComponent] No entity controller found", this);
        else if (tile != null) {
            if (debug) Debug.Log("[TriggerComponent] " + tile.transform.parent, this);
            tile.onTriggerExit(entity);
        } else Debug.LogError("[TriggerComponent] Tile is null", this);
    }

}