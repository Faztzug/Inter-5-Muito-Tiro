using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelle : EnemyIA
{
    [SerializeField] private GameObject BonkBox;
    [SerializeField] private float attackCooldown;
    protected bool atacking = false;

    protected override void Update() 
    {
        base.Update();
        anim = GetComponent<Animator>();
    }

    protected override void AsyncUpdateIA()
    {
        base.AsyncUpdateIA();
        if(!atacking) GoToPlayer();
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
                //anim.SetFloat("Movement", 1);

            }
            else if(distance <= minPlayerDistance && IsPlayerAlive())
            {
                StartCoroutine(AttackPlayer());
            }
            else 
            {
                pos = transform.position;
                agent.SetDestination(pos);
                //anim.SetFloat("Movement", 0);
            }
        }
        else
        {
            Debug.LogError(gameObject.name + " MELLE AI OUT OF NAV MESH!");
        }

        if (agent.velocity != Vector3.zero)
        {
            anim.SetFloat("Movement", 1);
        }
        else
        {
            anim.SetFloat("Movement", 0);
        }
        
    }
    private IEnumerator AttackPlayer()
    {
        if(!atacking) anim.SetTrigger("Melle");
        atacking = true;

        yield return new WaitForSeconds(attackCooldown);
        //BonkBox.SetActive(true);

        
        //yield return new WaitForSeconds(attackCooldown);
        //BonkBox.SetActive(false);
        atacking = false;
    }
}
