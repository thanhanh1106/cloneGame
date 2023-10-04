using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponSO weaponData;
    public AnimatorCharacterController.AttackType attackTypeAnimation;
    public WeaponName weaponName;

    public Action<bool> OnAttack;

    public abstract void Attack(Transform target);
    
}
public enum WeaponName
{
    MiniGun,
    SawGun
}
