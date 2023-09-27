using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBrain : CharacterBrain
{
    [SerializeField] float followRage;

    protected List<Vector3> wayPoint;
    protected int currentPointIndex;

    protected override CharacterBrain target => GameManager.Instance.Player;

    protected bool arrived = false;
    protected bool following = false;
    protected bool attacking = false;
    protected bool die = false;

    protected bool onWeaponAttacking;

    protected override void Awake()
    {
        base.Awake();

        // chú ý: để ý Id của con bot không thì bị throw, đảm bảo tính đúng đắn dữ liệu
        wayPoint = GameManager.Instance.EnemiesWayPoints
            .SingleOrDefault(wayPoints => wayPoints.TargetEnemyId.Equals(Id))?.listPoints
            .Select(point => point.position).ToList();

        agent.OnArived += HandlerArrived;
        characterAttack.OnAttack = HandlerWeaponAttaking;
    }

    

    protected virtual void Update()
    {
        if (die)
        {
            animator.SetTrigger("Die");
            return;
        }
        attacking = Vector3.Distance(transform.position, target.transform.position) <= characterAttack.AttackRange;
        following = Vector3.Distance(transform.position,target.transform.position) <= followRage 
            && !attacking && HadSeenPlayer();
        
        if(attacking)
        {
             
            Attack();
            if (onWeaponAttacking)
            {
                agent.AgentBody.isStopped = true;
                // dừng lại không di chuyển nữa
            }
            else Follow(0.5f);
        }

        if (following) Follow(1);
        if (!arrived && !following && !attacking)
        {
            animator.SetMovement(AnimatorCharacterController.MovementType.Run);
            agent.SetSpeed(0);
            agent.SetDestination(wayPoint[currentPointIndex]);  
        }
        
    }

    void Follow(float speedLerpValue)
    {
        animator.SetMovement(AnimatorCharacterController.MovementType.Run);
        agent.SetSpeed(speedLerpValue);
        agent.SetDestination(target.transform.position);
    }

    protected void HandlerArrived()
    {
        arrived = true;
        animator.SetMovement(AnimatorCharacterController.MovementType.Idle);
        this.DelayLamda(() =>
        {
            currentPointIndex++;
            if(currentPointIndex >= wayPoint.Count)
                currentPointIndex = 0;
            arrived = false;
        }, 2f);
    }

    protected bool HadSeenPlayer()
    {
        Vector3 toTarget = target.transform.position - transform.position;
        toTarget = new Vector3(toTarget.x, 0, toTarget.z);
        float angle = Vector3.Angle(transform.forward, toTarget);
        return angle <= 45f;
    }

    protected override void HandlerDie()
    {
        
        Debug.Log("enemy die");
    }
    private void HandlerWeaponAttaking(bool onAttacking)
    {
        onWeaponAttacking = onAttacking;
    }
}
