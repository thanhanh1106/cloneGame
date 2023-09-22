using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime = 0.4f;

    Vector3 velocity;
    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position,target.position + offset,ref velocity,smoothTime);
    }
}
