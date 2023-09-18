using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] List<Transform> transfromMovePoints = new List<Transform>();
    [SerializeField] Transform target;
    [SerializeField] float distanceFollow;
    [SerializeField] float distanceAttack;
    [SerializeField] float restTime;

    Vector3 groundContact;
    Vector3[] movementPoints;
    int currentPointIndex;
    Vector3 currentPoint;
    bool isFollowing;
    bool isAttacking;
    bool isMoving;
    bool isStandRest;
    bool move;
    Vector3 currentPositionOnGround;// điểm trên mặt đất mà con bot đang đứng
    float currentRestingTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            groundContact = hit.point;
        }

        foreach (Transform point in transfromMovePoints)
        {
            point.position = new Vector3(point.position.x, groundContact.y, point.position.z);
        }

        movementPoints = transfromMovePoints.Select(transform => transform.position).ToArray();
        currentPointIndex = 0;
        currentPoint = movementPoints[currentPointIndex];
        move = true;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= distanceFollow)
            isFollowing = true;
        else
            isFollowing = false;

        if (Vector3.Distance(transform.position, target.position) <= distanceAttack)
            isAttacking = true;
        else
            isAttacking = false;

        if (!isFollowing && !isAttacking && !isMoving && !isStandRest && move)
        {
            agent.SetDestination(currentPoint);
            isMoving = true;
            move = false;
        }

        if(transform.position == new Vector3(currentPoint.x, transform.position.y, currentPoint.z) && !isFollowing && !isAttacking)
        {
            isStandRest = true;
            isMoving = false;
        }
        if (isFollowing)
        {
            agent.SetDestination(target.position);
        }

        if (isStandRest)
        {
            currentRestingTime += Time.deltaTime;
            if (currentRestingTime >= restTime)
            {
                currentPointIndex++;
                if (currentPointIndex >= movementPoints.Length) currentPointIndex = 0;

                currentPoint = movementPoints[currentPointIndex];
                isStandRest = false;
                currentRestingTime = 0;
                move = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, distanceFollow);
    }

}
