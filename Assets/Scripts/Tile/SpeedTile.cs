using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTile : BaseTile
{
    [SerializeField]
    protected bool speedDebug = false;
    [Tooltip("The speed multiplier for the entity on entering")]
    [SerializeField]
    protected float speedMultiplier;
    [field: SerializeField, Tooltip("The speed reducer for the entity on exiting, calculated from the speed multiplier")]
    protected float speedReducer { get {return 1/speedMultiplier; } }
    // Start is called before the first frame update
    private void applySpeedMultiplier(EntityController entity, float speedMultiplier) {
        if (speedDebug) Debug.Log("[SpeedTile] Applying multiplier: " + speedMultiplier + " to entity: " + entity.gameObject.name, this);
        entity.applySpeedMultiplier(speedMultiplier);
    }
    
    public override void onColission(EntityController entity) {
        base.onColission(entity);
        applySpeedMultiplier(entity, speedMultiplier);
    }
    public override void onExit(EntityController entity) { 
        base.onExit(entity);
        applySpeedMultiplier(entity, speedReducer);
    }
}
