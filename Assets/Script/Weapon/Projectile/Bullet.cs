using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public override void MoveToTarget(Vector3 target)
    {
        Vector3 targetNotY = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(targetNotY);
        rb.velocity = transform.forward * projectileData.MoveSpeed;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun"))
            return;
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        
        if ( damageable != null)
        {
            damageable.TakeDamage(projectileData.Damage);
        }
        Invoke("ReturnPool", 0.2f);
    }
    protected override void ReturnPool()
    {
        ObjectPooler.Instance.ReturnGameObjectToPool(projectileData.ProjectileName.ToString(), this.gameObject);
    }
}
