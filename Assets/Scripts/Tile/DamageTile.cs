using System.Collections.Generic;
using UnityEngine;

public class DamageTile : TriggerTile {
    [SerializeField]
    int damage = 1;
    [SerializeField, Tooltip("The name of the gameObject with the trigger collider")]
    protected bool damageDebug = false;


    public override void onTriggerEnter(EntityController entity) {
        if (damageDebug) Debug.Log("[DAMAGETILE] Trigger entered");
        if (animator != null) {
            animator.SetTrigger("play");
        }
        entity.TakeDamage(damage);
    }

/*
    public override void onTriggerExit(EntityController entity)
    {
        if (animator != null) {
            animator.ResetTrigger("play");
        }
    }
*/
}