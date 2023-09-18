using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 positionOffset;
    [SerializeField] Vector3 rotationOffset;
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] float maxZ;
    [SerializeField] float minZ;
    [SerializeField] float smoothTime = 0.2f;

    Vector3 velocity;
    private void LateUpdate()
    {
        Vector3 target = this.target.position;
        target.x = Mathf.Clamp(target.x, minX, maxX);
        target.z = Mathf.Clamp(target.z, minZ, maxZ);
        transform.eulerAngles = rotationOffset;
        transform.position = Vector3.SmoothDamp(transform.position,target + positionOffset,ref velocity,smoothTime);
    }
}
