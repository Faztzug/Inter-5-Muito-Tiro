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
    [SerializeField] protected float shootingDistance = 100f;
    [SerializeField] protected float findPlayerDistance = 100f;
    [SerializeField] protected float minPlayerDistance = 10f;
    [Range(0,1)] protected float updateRate;
    protected float distance;
    [SerializeField] float FocusGainOnDeath = 3f;
    [HideInInspector] public GameState state;
    protected Animator anim;
    [HideInInspector] public bool alive = true;
    [SerializeField] protected int damageTauntAsync = 3;
    protected int tauntTimerAsync;

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
        state = player.GetComponent<GameState>();
        agent = GetComponent<NavMeshAgent>();
        rgbd = GetComponent<Rigidbody>();
        rgbd.maxAngularVelocity = 0;
        distance = Mathf.Infinity;
        tauntTimerAsync = damageTauntAsync;
        anim = GetComponent<Animator>();
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
        if(tauntTimerAsync > 0)
        {
            tauntTimerAsync--;
            return;
        }
    }
    protected bool IsPlayerAlive()
    {
        if(player != null && player.gameObject.activeSelf && state.playerDead == false) return true;
        else return false;
    }
    public virtual void EnemyDeath()
    {
        GivePlayerFocusOnDeath();
        anim.SetTrigger("Die");

        if(agent.isOnNavMesh) agent.SetDestination(transform.position);

        alive = false;

        foreach (var collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
        foreach (var script in GetComponentsInChildren<MonoBehaviour>())
        {
            if(script == this) continue;
            script.enabled = false;
        }
        this.StopAllCoroutines();
    }
    public virtual void GivePlayerFocusOnDeath()
    {
        if(!IsPlayerAlive()) return;
        if(player.TryGetComponent<SlowMotion>(out SlowMotion slowMotion))
        {
            slowMotion.GainFocusPoints(FocusGainOnDeath);
        }
    }
    public virtual void Taunt()
    {
        tauntTimerAsync = damageTauntAsync;
    }
    public virtual void UpdateHealth(float health, float maxHealth)
    {

    }
}
