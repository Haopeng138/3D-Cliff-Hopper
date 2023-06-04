using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : EntityController
{


    private void OnCollisionEnter(Collision other) {

        
    }

    protected new void OnControllerColliderHit(ControllerColliderHit hit){
        base.OnControllerColliderHit(hit);
         
        PlayerController player = hit.gameObject.GetComponent<PlayerController>();
        if (player != null){
            Debug.Log("Player touched the bomb");
            player.Kill();
        }
    }

    public override void addScore(int score){
        // Don't add score
        //if (ScoreManager.Instance != null) ScoreManager.Instance.addScore(score);
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
