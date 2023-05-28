using System.Collections.Generic;
using UnityEngine;

public class RotateTile: BaseTile
{

    void Start(){
        base.Start();
        
    }

    public override bool onTap(EntityController entity){
        if (inArea(entity.transform.position)){
            Debug.Log("[ROTATE] Tapped");
            entity.rotateEntity(transform.position, tileOrientation);
            return true;
        }
        return false;
    }
}