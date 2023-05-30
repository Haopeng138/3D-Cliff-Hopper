using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    [SerializeField]
    private Vector3 velocity = Vector3.zero;
    public float distance;
    [Tooltip("The angle between the camera and the ground in degrees")]
    public float heightAngle = 45;
    public float smoothTime;

    void Start(){
        transform.position = new Vector3(1, Mathf.Cos(Mathf.Deg2Rad * heightAngle), 1);
        transform.LookAt(Vector3.zero);
        transform.position = target.position + Vector3.one *distance;
    }

    void Update() {
        if (target != null) FollowTarget(target);
    }

    // void LateUpdate() {
    //     Vector3 localPos = transform.localPosition;
    //     transform.localPosition = new Vector3(Mathf.Clamp(localPos), Mathf.Clamp(localPos.y), localPos.z);
    // }

    public void FollowTarget(Transform t) {
        Vector3 localPos = transform.position;
        Vector3 targetLocalPos = t.transform.position;
        //float xz = (targetLocalPos.x + targetLocalPos.z)/2 * Mathf.Cos(Mathf.PI/4) + distance;
        float xz = (targetLocalPos.x > targetLocalPos.z ? targetLocalPos.x : targetLocalPos.z )  + distance; 
        //Mathf.Sqrt(distance * distance - Mathf.Pow(targetLocalPos.y - height ,2))/2 +(targetLocalPos.x + targetLocalPos.z/2) ;
        transform.position = Vector3.SmoothDamp(localPos, new Vector3(xz, targetLocalPos.y + distance, xz), ref velocity, smoothTime);
        //transform.LookAt(new Vector3(-1,-1,-1) + localPos, Vector3.up);
    }
    
}