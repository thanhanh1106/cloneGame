using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    Vector3 moveDirection;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        moveDirection.x = joystick.Horizontal;
        moveDirection.z = joystick.Vertical;
        moveDirection = moveDirection * moveSpeed*Time.deltaTime;

        agent.Move(moveDirection);
    }
}
