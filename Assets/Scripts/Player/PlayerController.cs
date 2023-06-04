using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PlayerController : EntityController
{
    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController playerAnimatorController;

    [Header("Animator parameters")]
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
    //TODO: Remove this
    //public float timeScale = 1f;

    void Start()
    {
        
        base.Start();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = playerAnimatorController;
        if (entityData.Skin != null){
            var renders = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var render in renders){
                render.material = entityData.Skin;
            }
        }

    }

    void Update(){
        // TDOO: Remove this
        // Time.timeScale = timeScale;

        base.Update();
        animator.SetFloat("movementSpeed", ((velocity.x > velocity.z) ? velocity.x : velocity.z) / moveSpeed);

        //TODO: Remove this
        // if (Input.GetKeyDown(KeyCode.Escape)) {
        //     timeScale = (timeScale == 0f) ? 1f : 0f;
        // }

        if (Input.GetKeyDown(KeyCode.G)) {
            GodMode = !GodMode;
            if (GodMode) {
                Debug.Log("[PLAYER] GodMode ON");
                animator.SetTrigger("enterGodMode");
            } else {
                Debug.Log("[PLAYER] GodMode OFF");
                animator.ResetTrigger("enterGodMode");
            }
        }
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

    // Muy poco "seguro" - Check typos 
    // animationTrigger hace refereferencia a un trigger del Animator 
    public void setAnimationTrigger(string animationTrigger){
        animator.SetTrigger(animationTrigger);
    }

    public void resetAnimationTrigger(string animationTrigger){
        animator.ResetTrigger(animationTrigger);
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
        AudioManager.instance.PlaySFX("Jump");
    }

    override protected void enterFallingState(){
        animator.ResetTrigger(jumpParameter);
        animator.SetTrigger(fallParameter);
    }

    override protected void enterDeadState(){
        animator.SetTrigger(deadParameter);
        AudioManager.instance.PlaySFX("Lose");
        ScoreManager.Instance.UpdateHighScore();
        //Debug.Log("Scene : " + SceneStateManager.sceneState);
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