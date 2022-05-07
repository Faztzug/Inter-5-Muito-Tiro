using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))] 
public class EnemyIA : MonoBehaviour
{
    [SerializeField] protected Transform player;
    protected Vector3 pos;
    protected Vector3 playerPos;
    protected NavMeshAgent agent;
    protected Rigidbody rgbd;
    [SerializeField] protected float findPlayerDistance = 100f;
    [SerializeField] protected float minPlayerDistance = 10f;
    [SerializeField] [Range(0,1)] protected float updateRate;

    protected virtual void Start() 
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            if(p.GetComponent<PlayerHealth>() == true)
            {
                player = p.transform;
                break;
            }
        }
        agent = GetComponent<NavMeshAgent>();
        rgbd = GetComponent<Rigidbody>();
        rgbd.maxAngularVelocity = 0;
        StartCoroutine(CourotineAsyncUpdateIA());
    }

    protected virtual void Update() 
    {
        
    }

    protected IEnumerator CourotineAsyncUpdateIA()
    {
        yield return new WaitForSecondsRealtime(updateRate);

        rgbd.velocity = Vector3.zero;
        
        AsyncUpdateIA();

        StartCoroutine(CourotineAsyncUpdateIA());
    }

    protected virtual void AsyncUpdateIA()
    {

    }
    protected bool IsPlayerAlive()
    {
        if(player != null && player.gameObject.activeSelf) return true;
        else return false;
    }
}
