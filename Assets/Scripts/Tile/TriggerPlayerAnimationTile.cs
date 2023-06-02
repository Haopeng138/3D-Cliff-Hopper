using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class TriggerPlayerAnimationTile : TriggerTile
{
    [SerializeField, Tooltip("The name of the Player's animation to play")]
    protected string animationName = "stumble";

    public override void onTriggerEnter(EntityController entity){
        base.onTriggerEnter(entity);

    }

    // Return true to stop the player from jumping
    public override bool onTap(EntityController entity){
        return overrideTap;
    }
    
    public override void onTriggerExit(EntityController entity){
        base.onTriggerExit(entity);
        PlayerController player = entity.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            player.changeState(EntityController.EntityState.MOVING);
        }
    }
    
}
*/