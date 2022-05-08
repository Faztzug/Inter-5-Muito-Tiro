using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGunner : EnemyIA
{
    
    [SerializeField] protected EnemyGun gun;
    protected Animator anim;
    [SerializeField] [Range(0,1)] private float weightIKhand;
    protected GameState pState;
    protected bool reloading;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        pState = player.GetComponent<GameState>();
    }
    protected override void Update() 
    {
        base.Update();
    }

    void OnAnimatorIK()
    {
        if(!IsPlayerAlive()) return;

        Vector3 frente = transform.forward;
        Vector3 direcaoAlvo = transform.forward * 1000f;
        float angulo = Vector3.Angle(frente, direcaoAlvo);

        
        anim.SetLookAtPosition(pState.bodyPartChest.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, pState.bodyPartChest.position);
    
        if(distance < findPlayerDistance)
        {
            anim.SetLookAtWeight(1);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weightIKhand);
        }
        else
        {
            anim.SetLookAtWeight(0);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
        
    }

    protected override void AsyncUpdateIA()
    {
        base.AsyncUpdateIA();
        GoToPlayer();
        ReadyFire();
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
            else 
            {
                pos = transform.position;
                agent.SetDestination(pos);
            }
        }
        else
        {
            Debug.LogError(gameObject.name + " GUNNER AI OUT OF NAV MESH!");
        }
        
    }

    protected void ReadyFire()
    {
        if(!IsPlayerAlive()) return;
        if(reloading == false)
        {
            if(gun.trigger)
            {
                Fire();
                return;
            } 
            else if(gun.loadedAmmo > 0)
            {
                Trigger();
                return;
            } 
        }
        if(gun.loadedAmmo < gun.maxLoadedAmmo)
        {
            gun.Reload();
            reloading = true;
        }
        else
        {
            reloading = false;
        }
    }
    protected void Fire()
    {
        gun.Fire();
    }
    protected void Trigger()
    {
        gun.Trigger();
    }
}
