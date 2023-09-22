using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Projectile preflabs")]
    public GameObject Projectile;

    [Header("Config")]
    public float AttachRange;
    public float FireRate;
    public float ReloadTime;
    public float ProjectileSpeed;
}
