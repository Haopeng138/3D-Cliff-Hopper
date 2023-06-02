using System.Collections.Generic;
using UnityEngine;

public class RotateTile: BaseTile
{
    [SerializeField]
    protected bool rotateDebug = false;

    protected void Start(){
        base.Start();
        
    }

    public override bool onTap(EntityController entity){
        if (inArea(entity.transform.position)){
            if (rotateDebug) Debug.Log("[ROTATE] Tapped", entity);
            entity.rotateEntity(transform.position, tileOrientation);
            //entity.addScore(1);
            return true;
        }
        return false;
    }

    protected override void entityInArea(){
        if (rotateDebug) Debug.Log("[ROTATE] Entity in area");
        // Spawnear particula para indicar poder hacer tap?
    }
}