using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    void LateUpdate(){
        Vector3 desiredPoistion = (target1.position + target2.position)/2 + offset;
        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPoistion, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
