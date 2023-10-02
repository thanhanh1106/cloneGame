using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapon
{
    protected int currentProjectile;
    public Transform SpawnPoint;
    public Action<int> OnChangedProjectile;
    protected virtual void Start()
    {
        Reaload();
    }
    public abstract void Reaload();


}
