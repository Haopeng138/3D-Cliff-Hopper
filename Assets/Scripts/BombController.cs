using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : EntityController
{
   
    protected override void moveStateUpdate(){

        base.moveStateUpdate();
        

        //velocity = directionVector * moveSpeed * Time.deltaTime;
    }
/*
    protected override void fallStateUpdate(){
        velocity.y += gravity * Time.deltaTime * fallMultiplier;
    }

    protected override void jumpStateUpdate(){
        velocity.y += gravity * Time.deltaTime;        
    }
    
    public override void moveEnity(){
        if (move){
            controller.Move(velocity * Time.deltaTime); 
        }
    }
*/
/*
    public override bool Jump(){
        // No deberia saltar por si solo (lo invoca la Tile)
        return  false;
    }
*/


    
}
