using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PlayerController : EntityController
{
    [Header("Player Controller")]
    [Space]
    [SerializeField]
    private ParticleSystem dustParticles;
    [SerializeField]
    private ParticleSystem hitParticles;

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
    public float timeScale = 1f;

    private GameObject ragdollRoot;

    void Start()
    {
        ragdollRoot = transform.Find("Rig1").gameObject;
        setRagdollState(false);


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

    public void setRagdollState (bool active){
        if (ragdollRoot == null){
             Debug.LogError("[PLAYER] Ragdoll root not found");
             return;
        }
        //if (active) ragdollRoot.transform.Translate(new Vector3(1,0,0) *.35f);

        Stack<GameObject> ragdollStack = new Stack<GameObject>();
        ragdollStack.Push(ragdollRoot);

        while(ragdollStack.Count > 0){
            GameObject current = ragdollStack.Pop();

            Rigidbody rb = current.GetComponent<Rigidbody>();
            Collider col = current.GetComponent<Collider>();
            
            if (rb != null && col != null){
                //rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                col.enabled = active;
                
            }

            foreach (Transform child in current.GetComponentInChildren<Transform>()){
                if (child != null) ragdollStack.Push(child.gameObject);
            }
        }

        //if (active) ragdollRoot.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
        
    }

    void Update(){
        Time.timeScale = timeScale;

        base.Update();
        animator.SetFloat("movementSpeed", ((velocity.x > velocity.z) ? velocity.x : velocity.z) / moveSpeed);


        if (Input.GetKeyDown(KeyCode.Escape)) {
            timeScale = (timeScale == 0f) ? 1f : 0f;
        }

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

        if (state == EntityState.DEAD){
            deathFrames++;
            
            if (deathFrames == 1){
                ragdollRoot.transform.SetLocalPositionAndRotation(new Vector3(ragdollRoot.transform.localPosition.x, Mathf.Max(0.608f, ragdollRoot.transform.localPosition.y), ragdollRoot.transform.localPosition.z), ragdollRoot.transform.localRotation);
            }
            
            if (deathFrames > 2){
                if (state == EntityState.DEAD && !controller.enabled){
                    setRagdollState(true);
                }

            } 
        }
    }
    int deathFrames = 0;  
    private void LateUpdate() {
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
        //animator.SetTrigger(deadParameter);
        animator.enabled = false;
        controller.enabled = false;
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