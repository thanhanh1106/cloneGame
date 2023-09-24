using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : Weapon
{
    float countTime = 0;
    public override void Attack(Transform target)
    {
        if(countTime == 0)
        {
            GameObject bulletObj = ObjectPooler.Instance.GetGameObjectFormPool("Bullet",SpawnPoint.position,Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.MoveToTarget(target.position);
        }
        countTime += Time.deltaTime;
        if (countTime >= weaponData.FireRate)
            countTime = 0;
    }
}
