using System.Collections.Generic;
using UnityEngine;

public class EmptyTile: TriggerTile
{
    public Collider collider;
    [SerializeField] protected bool emptyDebug = false;

    protected void Start(){
        base.Start();
        if (collider == null) collider = GetComponent<BoxCollider>();
        
    }

    
    public override void onColission(EntityController entity){
            
    }

    public override void onTriggerEnter(EntityController entity){
        if (emptyDebug) Debug.Log("[EMPTY] Trigger entered", this);
        
        collider.enabled = entity.GodMode;

    }

    public override void onTriggerExit(EntityController entity){
        if (emptyDebug) Debug.Log("[EMPTY] Trigger exited", this);
        //collider.enabled = entity.entityData.GodMode;

    }
}