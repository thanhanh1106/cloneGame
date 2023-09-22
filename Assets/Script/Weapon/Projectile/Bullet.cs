using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public override void MoveToTarget(Vector3 target)
    {
        transform.LookAt(target);
        gameObject.SetActive(true);
        rb.velocity = transform.forward * projectileData.MoveSpeed;
    }
    protected void OnTriggerEnter(Collider other)
    {
        projectileData.ProjectileFX?.gameObject.SetActive(false);
        projectileData.HitFX?.gameObject.SetActive(false);
        projectileData.HitFX?.Play();
    }
}
