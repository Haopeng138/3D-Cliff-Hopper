using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseTile : MonoBehaviour {
    public bool baseDebug = false;
    [Header("Tile Controller Properties")]
    [Tooltip("The tilemap controller that spawned this tile, these options are only shown for debug purposes, you should not change")]
    public TileMapController tileMapController;
    [Tooltip("The orientation of the tile")]
    public Direction tileOrientation; 
    [Tooltip("The offset of the tile in the tilemap")]
    public int offset;
    

    [Header("Tile Options")]    
    [Tooltip("The area in which the tile will listen for taps")]
    public float area = .25f;
    [Tooltip("If true, the tile will override the default behaviour (jump)")]
    public bool overrideTap = false;

    public AudioClip footstepSound;
    public AudioClip tapSound;
    public Material footstepParticleMaterial;


    protected void Start() {
        if (tileOrientation == Direction.X) transform.parent.Rotate(0, 90, 0);
    }

    public virtual void onColission(EntityController entity){
        if (!nextTileSet) tileMapController.NexTile();
        nextTileSet = true;
        //if (baseDebug) Debug.Log("[BASETILE] Collision");
    }

    public virtual void onExit(EntityController entity){
        //if (baseDebug) Debug.Log("[BASETILE] Exit");
    }

    // Returns false if the tile does not override the default behaviour (jump)
    public virtual bool onTap(EntityController entity){
        if (baseDebug) Debug.Log("[BASETILE] Tapped", this);
        return overrideTap;
    }

    // External tile trigger (mostly for traps)
    public virtual void onTriggerEnter(EntityController entity){
        if (baseDebug) Debug.Log("[BASETILE] Entered Trigger", this);
    }
        // External tile trigger (mostly for traps)
    public virtual void onTriggerExit(EntityController entity){
        if (baseDebug) Debug.Log("[BASETILE] ExitedTrigger", this);
    }

    public bool inArea(Vector3 entityPosition){
        bool inXArea = Mathf.Abs(entityPosition.x - transform.position.x) < area;
        bool inZArea = Mathf.Abs(entityPosition.z - transform.position.z) < area;
        if (inXArea && inZArea) entityInArea();
        return (inXArea && inZArea); /* && tileOrientation != entity.currentDirection*/
    }
    
    // InArea Callback, called when the entity is in the area of the tile
    protected virtual void entityInArea(){
        if (baseDebug) Debug.Log("[BASETILE] Entity in area", this);
    }

}