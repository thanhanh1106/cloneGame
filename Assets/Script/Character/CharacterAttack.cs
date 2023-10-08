using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] float DefaultAttackRange = 5f;
    [SerializeField] float DefaultAttackDamage = 2f; // dame nếu dùng tay không
    
    private Weapon weapon;



    public Action<bool> OnAttack;

    private void OnEnable()
    {
        if(weapon && weapon is Gun gun)
            gun.OnChangedProjectile += HandlerChangedProjectile;

         
    }
    private void OnDisable()
    {
        if(weapon && weapon is Gun gun)
            gun.OnChangedProjectile -= HandlerChangedProjectile;
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
        if(weapon)
            weapon.OnAttack = HandlerOnAttackWeapon;
    }


    public void HandleChangeWeapon()
    {
        weapon = GetComponentInChildren<Weapon>();
        if (weapon)
            weapon.OnAttack = HandlerOnAttackWeapon;
    }

    public void Attack(Transform target)
    {
        weapon?.Attack(target);
        // đoạn này phải sửa lại về tầm đánh
        if(weapon == null)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable.TakeDamage(DefaultAttackDamage);
        }
    }

    public AnimatorCharacterController.AttackType GetAnimationAttackType()
    {
        return weapon.attackTypeAnimation;
    }

    // thiết kế này đang sai vì weapon ở đây không nhất thiết là súng 
    public void HandlerChangedProjectile(int currentProjectile)
    {
        // viết logic để xử lý ui số lượng đạn có trong băng ở đây
    }
    protected void HandlerOnAttackWeapon(bool isAttacking)
    {
        OnAttack?.Invoke(isAttacking);
    }
}
