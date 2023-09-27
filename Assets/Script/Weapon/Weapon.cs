using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponSO weaponData;
    public Transform SpawnPoint;

    protected int currentProjectile;
    public Action<int> OnChangedProjectile;

    protected virtual void Start()
    {
        Reaload();
    }

    public abstract void Attack(Transform target);
    public abstract void Reaload();
}
