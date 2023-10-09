using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBrain : CharacterBrain
{
    [SerializeField] protected Joystick joystick;
    [SerializeField] List<Weapon> weaponsList = new List<Weapon>();
    Dictionary<WeaponName, GameObject> weaponsDic = new Dictionary<WeaponName, GameObject>();
    WeaponName currentWeapon;

    protected override CharacterBrain target =>
        GameManager.Instance.Enemies
        .Where(enemy => Vector3.Distance(transform.position,enemy.transform.position) <= characterAttack.AttackRange)
        .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).FirstOrDefault();

    protected override void HandlerDie()
    {
        //Debug.Log("Player die");
    }
    private void Awake()
    {
        foreach (Weapon weapon in weaponsList)
        {
            weaponsDic[weapon.weaponName] = weapon.gameObject;
        }
    }
    private void OnEnable()
    {
        GameManager.Instance.UiSwapGunManager.OnSwapGun += HandlerSwapWeapon;
    }
    private void OnDisable()
    {
        GameManager.Instance.UiSwapGunManager.OnSwapGun -= HandlerSwapWeapon;
    }

    protected void Update()
    {
        if(joystick.Direction == Vector2.zero)
        {
            if(CanAttack)
                Attack();
            else
                animator.SetMovement(AnimatorCharacterController.MovementType.Idle);
        }
        else
        {
            agent.SetSpeed(joystick.Direction.magnitude);
            animator.SetMovement(AnimatorCharacterController.MovementType.Run);
            Vector3 direction = new Vector3(joystick.Direction.x,0,joystick.Direction.y);
            agent.MoveInDir(direction);
        }
    }

    void HandlerSwapWeapon(WeaponName name)
    {
        weaponsDic[currentWeapon].SetActive(false);
        currentWeapon = name;
        GameObject currentWeaponObj = weaponsDic[currentWeapon];
        currentWeaponObj.SetActive(true);
        Gun gun = currentWeaponObj.GetComponent<Gun>();
        gun?.Reaload();
        
        characterAttack.HandleChangeWeapon();

    }
}
