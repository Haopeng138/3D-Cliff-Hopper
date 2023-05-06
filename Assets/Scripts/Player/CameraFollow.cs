using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector3 offset = new Vector3(75,75,75), velocity = Vector3.zero;
    public float smoothTime;

    void Update() {
        if (target != null) FollowTarget(target);
    }

    // void LateUpdate() {
    //     Vector3 localPos = transform.localPosition;
    //     transform.localPosition = new Vector3(Mathf.Clamp(localPos), Mathf.Clamp(localPos.y), localPos.z);
    // }

    public void FollowTarget(Transform t) {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = t.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + 75, 75, targetLocalPos.z + 75), ref velocity, smoothTime);
    }
    
}