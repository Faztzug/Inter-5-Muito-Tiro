using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))] 
public class EnemyIA : MonoBehaviour
{
    [SerializeField] [Range(0,1)] protected float[] updateRateRNG = new float[2];
    [HideInInspector] public Transform player;
    protected Vector3 pos;
    protected Vector3 playerPos;
    protected NavMeshAgent agent;
    protected Rigidbody rgbd;
    [SerializeField] protected float findPlayerDistance = 100f;
    [SerializeField] protected float minPlayerDistance = 10f;
    [Range(0,1)] protected float updateRate;
    protected float distance;
    [SerializeField] float FocusGainOnDeath = 3f;

    protected virtual void Start() 
    {
        updateRate = Random.Range(updateRateRNG[0], updateRateRNG[1]);
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
        distance = Mathf.Infinity;
        StartCoroutine(CourotineAsyncUpdateIA());
    }

    protected virtual void Update() 
    {
        
    }

    protected IEnumerator CourotineAsyncUpdateIA()
    {
        updateRate = Random.Range(updateRateRNG[0], updateRateRNG[1]);

        yield return new WaitForSeconds(updateRate);

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
    public virtual void GivePlayerFocusOnDeath()
    {
        if(!IsPlayerAlive()) return;
        if(player.TryGetComponent<SlowMotion>(out SlowMotion slowMotion))
        {
            slowMotion.GainFocusPoints(FocusGainOnDeath);
        }
    }
}
