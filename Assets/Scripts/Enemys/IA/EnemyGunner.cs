using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyGunner : EnemyIA
{
    [Header("Gunner")]
    [SerializeField] protected EnemyGun gun;
    [Range(0,1)] public float weightIKhand;
    [SerializeField] private bool rotateTowardsPlayer = true;
    protected GameState pState;
    protected bool reloading;
    [SerializeField] protected Transform playerLookAt;

    protected override void Start()
    {
        base.Start();
        //anim = GetComponentInChildren<Animator>();
        pState = player.GetComponent<GameState>();
    }
    protected override void Update() 
    {
        base.Update();
        if(rotateTowardsPlayer && alive && IsPlayerAlive())
        {
            if(playerLookAt != null) 
            {
                playerLookAt.position = pState.bodyPartChest.position;
            }
            else
            {
                transform.LookAt(player);
            }
        } 
    }

    protected virtual void OnAnimatorIK()
    {
        if(!IsPlayerAlive()) return;

        Vector3 frente = transform.forward;
        Vector3 direcaoAlvo = transform.forward * 1000f;
        float angulo = Vector3.Angle(frente, direcaoAlvo);

        
        anim.SetLookAtPosition(pState.bodyPartChest.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, pState.bodyPartChest.position);
    
        if(distance < shootingDistance && alive)
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
        GunnerBehavior();
    }
    protected virtual void GunnerBehavior()
    {
        GoToPlayer();
        ReadyFire();
    }

    protected void GoToPlayer()
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
            Debug.Log("GUNNER GOING TO PLAYER");
            if(distance > minPlayerDistance && distance < findPlayerDistance && IsPlayerAlive())
            {
                playerPos = player.position;
                agent.SetDestination(playerPos);
                anim.SetFloat("Movement", 1);
            }
            else 
            {
                pos = transform.position;
                agent.SetDestination(pos);
                anim.SetFloat("Movement", 0);
            }
        }
        else
        {
            Debug.LogError(gameObject.name + " GUNNER AI OUT OF NAV MESH!");
        }
        
    }

    protected virtual void ReadyFire()
    {
        //Debug.Log("Ready Fire?");
        //Debug.Log(!IsPlayerAlive() || distance > shootingDistance);
        if(!IsPlayerAlive() || distance > shootingDistance) return;
        
        if(reloading == false)
        {
            //Debug.Log("Ready Fire Valid");
            if(gun.trigger)
            {
                Fire();
                anim.SetTrigger("Atirando");
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
            //Debug.Log("Reloading");
            gun.Reload();
            reloading = true;
        }
        else
        {
            //Debug.Log("Reloading Done");
            reloading = false;
        }
    }
    protected void Fire()
    {
        //Debug.Log("Fire");
        gun.Fire();
    }
    protected void Trigger()
    {
        //Debug.Log("Trigger");
        gun.Trigger();
    }
}
