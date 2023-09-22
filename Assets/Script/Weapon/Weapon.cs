using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponSO weaponData;
    public Transform SpawnPoint;

    public abstract void Attack(Transform target);
}