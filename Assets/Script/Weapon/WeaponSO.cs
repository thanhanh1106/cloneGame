using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Projectile preflabs")]
    public GameObject Projectile;
    public ProjectileName ProjectileName;

    [Header("Config")]
    public float AttachRange;

    [Header("Config Gun")]
    public float FireRate;
    public float ReloadTime;
    public int NumOfProjectile;

    [Header("Config Melee")]
    public float DamageMelee;
}
public enum ProjectileName
{
    Bullet1,
    Bullet2
}
