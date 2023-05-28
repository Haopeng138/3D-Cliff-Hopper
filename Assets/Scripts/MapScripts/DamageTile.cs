using System.Collections.Generic;
using UnityEngine;

class DamageTile : BaseTile {
    protected Collider triggerCollider;

    void Start() {
        base.Start();
        triggerCollider = GetComponentInChildren<Collider>();
        if (debug) {
            if (triggerCollider == null) Debug.LogError("[DAMAGETILE] No collider found");
            else if (triggerCollider.isTrigger == false) Debug.LogError("[DAMAGETILE] Collider is not a trigger");
            else if (triggerCollider.gameObject.name != "trigger") Debug.LogError("[DAMAGETILE] Collider is not named 'Trigger'");
            else Debug.Log("[DAMAGETILE] Collider found");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (debug) Debug.Log("[DAMAGETILE] Trigger entered");
    }
}