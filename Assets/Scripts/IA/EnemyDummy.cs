using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDummy : EnemyIA
{
    protected override void Update() 
    {
        base.Update();
        
        //RunAway();
    }

    protected override void AsyncUpdateIA()
    {
        base.AsyncUpdateIA();

        RunAway();
    }

    private void RunAway()
    {
        pos = transform.position;
        pos.y = 0;
        playerPos = player.position;
        playerPos.y = 0;
        var distance = Vector3.Distance(pos, playerPos);

        if(agent.isOnNavMesh)
        {
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
        else
        {
            Debug.LogError(gameObject.name + " OUT OF NAV MESH!");
        }
        

        
    }
}
