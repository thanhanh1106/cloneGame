using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubmachineGun : Gun
{
    float countTime = 0;

    bool isOnReloading = false;
    public override void Attack(Transform target)
    {
        if (currentProjectile == 0 && !isOnReloading)
        {
            isOnReloading = true;
            this.DelayLamda(() =>
            {
                Reaload();
                isOnReloading = false;
            }, weaponData.ReloadTime);
        }

        OnAttack?.Invoke(!isOnReloading);
        if (isOnReloading) return;

        if (countTime == 0)
        {
            GameObject bulletObj = ObjectPooler.Instance.GetGameObjectFormPool(weaponData.ProjectileName.ToString(), SpawnPoint.position, Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.MoveToTarget(target.position);
            currentProjectile--;
            OnChangedProjectile?.Invoke(currentProjectile);
        }
        countTime += Time.deltaTime;
        if (countTime >= weaponData.FireRate)
            countTime = 0;
    }

    public override void Reaload()
    {
        currentProjectile = weaponData.NumOfProjectile;
        OnChangedProjectile?.Invoke(currentProjectile);
    }
}
