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
    }
    protected virtual void OnEnable()
    {
        Invoke("ReturnPool", projectileData.TimeExistence);
    }
    protected abstract void ReturnPool();

    public abstract void MoveToTarget(Vector3 target);
}
