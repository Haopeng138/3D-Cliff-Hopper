using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
/*
    // Hotizontal and Vertical move
    public float speed;
    private enum PlayerDirection {Stop,Forward,Right};
    private PlayerDirection playerDirection;
    private Vector3 targetPosition;

    private float horizontalMove;
    private float verticalMove;
    public CharacterController player;  
    private Vector3 playerMovement;
    // Jump and Gravity
    Rigidbody rg;
    public float jumpForce = 5.0f;
    bool isJumping=false;
    public int maxJumps = 2;
    private int jumpsLeft;
*/
    public Material playerSkin;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController playerAnimatorController;

    [Tooltip("Animator parameters")]
    [SerializeField]
    private string idleParameter = "enterIdle";
    [SerializeField]
    private string moveParameter = "enterMoving";
    [SerializeField]
    private string jumpParameter = "enterJumping";
    [SerializeField]
    private string fallParameter = "enterFalling";
    [SerializeField]
    private string deadParameter = "-";

    void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = playerAnimatorController;
        if (playerSkin != null){
            var renders = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var render in renders){
                render.material = playerSkin;
            }
        }
    }

    void Update(){
        base.Update();
        animator.SetFloat("movementSpeed", ((velocity.x > velocity.z) ? velocity.x : velocity.z) / moveSpeed);
    }


    override public bool Jump(){
        bool jumpPressed = Input.GetKeyDown("space");//Input.GetButtonDown("Jump");
        //Debug.Log("[PLAYER] Jump: " + Input.GetButtonDown("Jump")); 
        if (currentTile != null && jumpPressed){
            //onTap returns TRUE if the tile overrides the default behaviour (jump)
            if (currentTile.onTap(this)){
                jumpPressed = false;
            }
        }
        return  jumpPressed;
    }

    #region Enter States

    override protected void enterIdleState(){
        animator.SetTrigger(idleParameter);
    }

    override protected void enterMovingState(){
        animator.SetTrigger(moveParameter);
    }

    override protected void enterJumpingState(){
        animator.SetTrigger(jumpParameter);
    }

    override protected void enterFallingState(){
        animator.SetTrigger(fallParameter);
    }

    override protected void enterDeadState(){
        animator.SetTrigger(deadParameter);
    }

    #endregion

    #region Exit States

    override protected void exitIdleState(){
        animator.ResetTrigger(idleParameter);
    }
    override protected void exitMovingState(){
        animator.ResetTrigger(moveParameter);
    }
    override protected void exitJumpingState(){
        animator.ResetTrigger(jumpParameter);
    }
    override protected void exitFallingState(){
        animator.ResetTrigger(fallParameter);
    }
    override protected void exitDeadState(){
        animator.ResetTrigger(deadParameter);
    }
    #endregion
}