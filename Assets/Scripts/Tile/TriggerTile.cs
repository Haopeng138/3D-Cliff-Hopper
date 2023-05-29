using System.Collections.Generic;
using UnityEngine;

public class TriggerTile : BaseTile {

    [SerializeField, Tooltip("The name of the gameObject with the trigger collider")]
    protected string triggerName = "trigger";
    [SerializeField]
    protected bool triggerDebug = false;
    protected Animator animator;

    protected void Start() {
        base.Start();
        
        animator = transform.parent.GetComponent<Animator>();
        if (triggerDebug) Debug.Log("[TRIGGER] Animator found: " + animator);
        
        setupTrigger();
    }

    public override void onTriggerEnter(EntityController entity) {
        if (triggerDebug) Debug.Log("[TRIGGER] Trigger entered");
        playAnimation();
    }

    public override void onTriggerExit(EntityController entity)
    {
        if (triggerDebug) Debug.Log("[TRIGGER] Trigger exited");
        resetAnimation();
    }

    // If gameObject has animator, SetTrigger("play")
    public void playAnimation(){
        if (animator != null) {
            animator.SetTrigger("play");
        }
    }

    // If gameObject has animator, ResetTrigger("play")
    public void resetAnimation(){
        if (animator != null) {
            animator.ResetTrigger("play");
        }
    }

    private void setupTrigger(){
        Collider triggerCollider = null;
        foreach(Collider collider in GetComponentsInChildren<Collider>()) {
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
            triggerCollider.gameObject.AddComponent<TriggerComponent>().init(this);
        }
    }
}