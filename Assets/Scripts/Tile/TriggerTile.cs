using System.Collections.Generic;
using UnityEngine;

public class TriggerTile : BaseTile {

    [SerializeField, Tooltip("The name of the gameObject with the trigger collider")]
    protected string triggerName = "trigger";
    [Header("Animations")]
    [SerializeField, 
     Tooltip("The name of the Tile's animation to play")]
    protected string tileAnimationName = "play";
    [SerializeField]
    protected bool triggerOnce = false;
    [SerializeField,
     Tooltip("The name of the Tile's animation to play")]
    protected string playerAnimationName = "";
    [SerializeField]
    protected bool triggerDebug = false;
    protected Animator animator;
    [SerializeField]
    protected AudioClip triggerSound;

    protected void Start() {
        base.Start();
        
        animator = transform.parent.GetComponent<Animator>();
        if (triggerDebug) Debug.Log("[TRIGGER] Animator found: " + animator);
        
        setupTrigger();
    }

    public override void onTriggerEnter(EntityController entity) {
        if (triggerDebug) Debug.Log("[TRIGGER] Trigger entered");
        playAnimation();
        
        PlayerController player = entity.gameObject.GetComponent<PlayerController>();
        if (player != null && playerAnimationName != "") 
            player.setAnimationTrigger(playerAnimationName);
        
    }

    public override void onTriggerExit(EntityController entity)
    {
        if (triggerDebug) Debug.Log("[TRIGGER] Trigger exited");
        if (!triggerOnce) resetAnimation();

        PlayerController player = entity.gameObject.GetComponent<PlayerController>();        
        
        if (player != null && playerAnimationName != "") 
            player.resetAnimationTrigger(playerAnimationName);
    }

    // If gameObject has animator, SetTrigger("play")
    public void playAnimation(){
        if (animator != null && tileAnimationName != "") {
            animator.SetTrigger(tileAnimationName);
        }
    }

    // If gameObject has animator, ResetTrigger("play")
    public void resetAnimation(){
        if (animator != null && tileAnimationName != "") {
            animator.ResetTrigger(tileAnimationName);
        }
    }

    private void setupTrigger(){
        Collider triggerCollider = null;
        foreach(Collider collider in transform.parent.gameObject.GetComponentsInChildren<Collider>()) {
            if (collider.isTrigger && collider.gameObject.name == triggerName) {
                triggerCollider = collider;
                break;
            }
        }

        if (triggerDebug) {
            if (triggerCollider == null) 
                Debug.LogError("[TRIGGER] No collider found");
            else if (triggerCollider.isTrigger == false)
                Debug.LogError("[TRIGGER] Collider is not a trigger");
            else if (triggerCollider.gameObject.name != "trigger") 
                Debug.LogError("[TRIGGER] Collider is not named 'Trigger'");
            else Debug.Log("[TRIGGER] Collider found: " + triggerCollider);
        }

        if (triggerCollider != null) {
            triggerCollider.gameObject.AddComponent<TriggerComponent>().init(this, triggerDebug);
        }
        else Debug.LogError("[TRIGGER] No collider found", this);
    }
}