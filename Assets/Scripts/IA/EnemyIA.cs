using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))] 
public class EnemyIA : MonoBehaviour
{
    protected Transform player;
    protected Vector3 pos;
    protected Vector3 playerPos;
    protected NavMeshAgent agent;
    protected Rigidbody rgbd;
    [SerializeField] [Range(0,1)] protected float updateRate;

    protected virtual void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rgbd = GetComponent<Rigidbody>();
        StartCoroutine(CourotineAsyncUpdateIA());
    }

    protected virtual void Update() 
    {
        
    }

    protected IEnumerator CourotineAsyncUpdateIA()
    {
        yield return new WaitForSecondsRealtime(updateRate);
        AsyncUpdateIA();

        StartCoroutine(CourotineAsyncUpdateIA());
    }

    protected virtual void AsyncUpdateIA()
    {

    }
}
