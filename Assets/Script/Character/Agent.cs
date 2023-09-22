using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotateSpeed;

    [Header("Debug")]
    [SerializeField] bool showPath = true;

    private NavMeshAgent agent;
    public NavMeshAgent AgentBody => this.TryGetComponent(ref agent);

    NavMeshPath agentPath;

    public Action OnArived;

    float moveSpeed;

    public void Initialized()
    {
        agentPath = new NavMeshPath();
        SetSpeed(0);
    }

    public void SetSpeed(float lerpValue)
    {
        lerpValue = Mathf.Clamp01(lerpValue);
        moveSpeed = Mathf.Lerp(minSpeed, maxSpeed, lerpValue);
    }

    public void SetDestination(Vector3 destination)
    {
        AgentBody.isStopped = false;// đảm bảo character không bị khựng lại trước khi đặt 1 destination mới
        AgentBody.SetDestination(destination);
        NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, agentPath);
        if (Arived(destination))
            OnArived?.Invoke();
    }

    public void MoveToDestination(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        direction.y = transform.forward.y;
        MoveInDir(direction);
        // thằng này để vẽ debug đường đi 
        NavMesh.CalculatePath(transform.position, destination,NavMesh.AllAreas,agentPath);

        if(Arived(destination))
            OnArived?.Invoke();
    }

    public void MoveInDir(Vector3 dir)
    {
        dir.Normalize();
        RotateInDir(dir);
        // do character được xoay theo hướng di chuyển từ trước đó rồi nên cho nhân vật di chuyển theo hướng
        // mặt để tránh bị đi lùi
        AgentBody.Move(transform.forward.normalized*moveSpeed*Time.deltaTime); // chuẩn hóa trước để đảm bảo tốc độ
    }

    public void RotateInDir(Vector3 dir)
    {
        Quaternion targetQuanternion = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQuanternion, rotateSpeed*Time.deltaTime);
    }

    bool Arived(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination) <= AgentBody.radius;
    }

    private void OnDrawGizmos()
    {
        if(showPath && agentPath != null)
        {
            for(int index = 1; index < agentPath.corners.Length; index++)
            {
                Gizmos.DrawCube(agentPath.corners[index - 1], Vector3.one * 0.2f);
                Gizmos.DrawLine(agentPath.corners[index - 1], agentPath.corners[index]);
            }
        }
    }
}
