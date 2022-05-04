using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDummy : MonoBehaviour
{
    private Transform player;
    private Vector3 pos;
    private Vector3 playerPos;
    private NavMeshAgent agent;
    private Rigidbody rgbd;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rgbd = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        pos = transform.position;
        pos.y = 0;
        playerPos = player.position;
        playerPos.y = 0;
        var distance = Vector3.Distance(pos, playerPos);

        playerPos = player.position;
        if(distance > 10 && distance < 100)
        {
            agent.SetDestination(playerPos);
        }
        else if(distance > 5)
        {
            agent.SetDestination(playerPos);
        }
        else 
        {
            pos = transform.position;
            agent.SetDestination(pos);
            rgbd.velocity = Vector3.zero;
            rgbd.angularVelocity = Vector3.zero;
        }
    }
}
