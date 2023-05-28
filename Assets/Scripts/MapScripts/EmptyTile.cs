using System.Collections.Generic;
using UnityEngine;

public class EmptyTile: BaseTile
{
    public BoxCollider collider;
    void Start(){
        base.Start();
        collider = GetComponent<BoxCollider>();

    }

    
    public override void onColission(EntityController entity){
        if (entity.entityData.GodMode){
            if (debug) Debug.Log("[EMPTY] GodMode " );
            collider.center = new Vector3(0, 0.5f, 0);
         }
         else {
            if (debug) Debug.Log("[EMPTY] NotGodMode " + entity.name);
            collider.center = new Vector3(0, 5f, 0);
         }
            
    }
}