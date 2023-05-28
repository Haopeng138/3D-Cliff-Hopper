using System.Collections.Generic;
using UnityEngine;

public class JumpTile: BaseTile
{
    public override bool onTap(EntityController entity){
        
        if (inArea(entity.transform.position)) {
            Debug.Log("[JUMP] Tapped");
            //entity.Jump();
            entity.changeState(EntityController.EntityState.JUMPING);
            return true;
        }
        return false;
    }
}