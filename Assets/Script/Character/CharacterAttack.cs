using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] float DefaultAttackRange = 5f;
    private Weapon weapon;


    public float AttackRange
    {
        get
        {
            return weapon ? weapon.weaponData.AttachRange : DefaultAttackRange;
        }
    }

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    void HandleChangeWeapon()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    public void Attack(Transform target)
    {
        weapon?.Attack(target);
    }
}
