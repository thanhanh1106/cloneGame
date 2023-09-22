using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBrain : CharacterBrain
{
    [SerializeField] float followRage;

    public Transform Point;

    protected List<Vector3> wayPoint;
    protected int currentPointIndex;

    protected override CharacterBrain target => GameManager.Instance.Player;

    protected bool arrived = false;
    protected bool following = false;
    protected bool attacking = false;

    protected override void Awake()
    {
        base.Awake();

        // chú ý: để ý Id của con bot không thì bị throw, đảm bảo tính đúng đắn dữ liệu
        wayPoint = GameManager.Instance.EnemiesWayPoints
            .SingleOrDefault(wayPoints => wayPoints.TargetEnemyId.Equals(Id))?.listPoints
            .Select(point => point.position).ToList();

        agent.OnArived += HandlerArrived;
    }

    protected virtual void Update()
    {
        attacking = Vector3.Distance(transform.position, target.transform.position) <= characterAttack.AttackRange;
        following = Vector3.Distance(transform.position,target.transform.position) <= followRage 
            && !attacking && HadSeenPlayer();
        
        if(attacking)
        {
            agent.AgentBody.isStopped = true; // dừng lại không di chuyển nữa
            Attack();
        }

        if (following)
        {
            animator.SetMovement(AnimatorCharacterController.MovementType.Run);
            agent.SetSpeed(1);
            agent.MoveToDestination(target.transform.position);
        }
        if (!arrived && !following && !attacking)
        {
            animator.SetMovement(AnimatorCharacterController.MovementType.Run);
            agent.SetSpeed(0);
            agent.MoveToDestination(wayPoint[currentPointIndex]);
        }

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
}
