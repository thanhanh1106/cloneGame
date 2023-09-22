using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public ProjectileSO projectileData;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        this.DelayLamda(() =>
        {
            // viết logic trả lại viên đạn vào pool ở đây
            Destroy(gameObject);
        }, projectileData.TimeExistence);
    }

    public abstract void MoveToTarget(Vector3 target);
}
