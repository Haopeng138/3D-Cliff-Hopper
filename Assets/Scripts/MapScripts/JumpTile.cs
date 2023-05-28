using System.Collections.Generic;
using UnityEngine;

//UNUSED
public class JumpTile: BaseTile
{
    public float jumpForce = 15f;
    public override bool onTap(EntityController entity){
        
        if (inArea(entity.transform.position)) {
            Debug.Log("[JUMP] Tapped");
            //entity.Jump();
            entity.forceJump(jumpForce);
            return true;
        }
        return false;
    }
}