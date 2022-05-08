using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelle : EnemyIA
{
    [SerializeField] private GameObject BonkBox;
    [SerializeField] private float attackCooldown;
    protected override void Update() 
    {
        base.Update();
    }

    protected override void AsyncUpdateIA()
    {
        base.AsyncUpdateIA();
        GoToPlayer();
    }

    private void GoToPlayer()
    {
        if(IsPlayerAlive())
        {
            playerPos = player.position;
            playerPos.y = 0;
        }

        pos = transform.position;
        pos.y = 0;

        distance = Vector3.Distance(pos, playerPos);

        if(agent.isOnNavMesh)
        {
            if(distance > minPlayerDistance && distance < findPlayerDistance && IsPlayerAlive())
            {
                playerPos = player.position;
                agent.SetDestination(playerPos);
            }
            else if(distance <= minPlayerDistance && IsPlayerAlive())
            {
                StartCoroutine(AttackPlayer());
            }
            else 
            {
                pos = transform.position;
                agent.SetDestination(pos);
            }
        }
        else
        {
            Debug.LogError(gameObject.name + " MELLE AI OUT OF NAV MESH!");
        }
        
    }
    private IEnumerator AttackPlayer()
    {
        BonkBox.SetActive(true);

        yield return new WaitForSeconds(attackCooldown);
        
        BonkBox.SetActive(false);
    }
}
