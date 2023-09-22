using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBrain : CharacterBrain
{
    [SerializeField] protected Joystick joystick;

    protected override CharacterBrain target =>
        GameManager.Instance.Enemies
        .Where(enemy => Vector3.Distance(transform.position,enemy.transform.position) <= characterAttack.AttackRange)
        .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).FirstOrDefault();

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
}
