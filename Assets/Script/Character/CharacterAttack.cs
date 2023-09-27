using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] float DefaultAttackRange = 5f;
    private Weapon weapon;

    private void OnEnable()
    {
        if(weapon)
            weapon.OnChangedProjectile += HandlerChangedProjectile;
    }
    private void OnDisable()
    {
        if(weapon)
            weapon.OnChangedProjectile -= HandlerChangedProjectile;
    }

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

    // thiết kế này đang sai vì weapon ở đây không nhất thiết là súng 
    public void HandlerChangedProjectile(int currentProjectile)
    {
        // viết logic để xử lý ui số lượng đạn có trong băng ở đây
    }
}
