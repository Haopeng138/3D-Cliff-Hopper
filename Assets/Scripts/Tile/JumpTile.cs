using System.Collections.Generic;
using UnityEngine;


public class JumpTile: TriggerTile
{
    [SerializeField]
    protected bool jumpDebug = false;
    [SerializeField]
    private bool isTrap = false;
    public float jumpForce = 15f;

    public override bool onTap(EntityController entity){
        if (inArea(entity.transform.position)) {
            if (jumpDebug) Debug.Log("[JUMP] Tapped");
            
            if (isTrap) 
             entity.forceJump(jumpForce);
            else entity.forceJump();

            return true;
        }
        return false;
    }

    public override void onTriggerEnter(EntityController entity)
    {
        if (jumpDebug) Debug.Log("[JUMP] Trigger entered");
        entity.forceJump(jumpForce);
        playAnimation();
    }

}