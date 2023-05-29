using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseTile : MonoBehaviour {
    public TileMapController tileMapController;
    [Tooltip("The orientation of the tile")]
    public Direction tileOrientation; 
    [Tooltip("The area in which the tile will listen for taps")]
    public float area = .25f;
    [Tooltip("The offset of the tile in the tilemap")]
    public int offset;
    public bool baseDebug = false;
    
    protected void Start() {
        if (tileOrientation == Direction.X) transform.parent.Rotate(0, 90, 0);
    }

    public virtual void onColission(EntityController entity){
        //if (baseDebug) Debug.Log("[BASETILE] Collision");
    }

    // Returns false if the tile does not override the default behaviour (jump)
    public virtual bool onTap(EntityController entity){
        if (baseDebug) Debug.Log("[BASETILE] Tapped");
        return false;
    }

    // External tile trigger (mostly for traps)
    public virtual void onTriggerEnter(EntityController entity){
        if (baseDebug) Debug.Log("[BASETILE] Entered Trigger");
    }
        // External tile trigger (mostly for traps)
    public virtual void onTriggerExit(EntityController entity){
        if (baseDebug) Debug.Log("[BASETILE] ExitedTrigger");
    }

    public bool inArea(Vector3 entityPosition){
        bool inXArea = Mathf.Abs(entityPosition.x - transform.position.x) < area;
        bool inZArea = Mathf.Abs(entityPosition.z - transform.position.z) < area;
        return (inXArea && inZArea); /* && tileOrientation != entity.currentDirection*/
    }

    protected virtual void entityInArea(){
        if (baseDebug) Debug.Log("[BASETILE] Entity in area ");
    }

}