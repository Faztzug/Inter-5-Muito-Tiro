using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))] 
public class EnemyIA : MonoBehaviour
{
    [SerializeField] [Range(0,1)] protected float[] updateRateRNG = new float[2];
    [HideInInspector] public Transform player;
    protected Vector3 pos;
    protected Vector3 playerPos;
    protected NavMeshAgent agent;
    protected Rigidbody rgbd;
    public float shootingDistance = 100f;
    [SerializeField] protected float findPlayerDistance = 100f;
    [SerializeField] protected float minPlayerDistance = 10f;
    [Range(0,1)] protected float updateRate;
    [HideInInspector] public float distance;
    [SerializeField] float FocusGainOnDeath = 3f;
    [HideInInspector] public GameState state;
    protected Animator anim;
    [HideInInspector] public bool alive = true;
    [SerializeField] protected int damageTauntAsync = 3;
    protected int tauntTimerAsync;
    protected Outline outline;
    protected float outlineMaxThickness;

    protected virtual void Awake() 
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
    }

    protected virtual void Start() 
    {
        updateRate = Random.Range(updateRateRNG[0], updateRateRNG[1]);
        state = player.GetComponent<GameState>();
        agent = GetComponent<NavMeshAgent>();
        if(agent == null) agent = transform.parent.GetComponent<NavMeshAgent>();
        rgbd = GetComponentInChildren<Rigidbody>();
        rgbd.maxAngularVelocity = 0;
        distance = Mathf.Infinity;
        tauntTimerAsync = damageTauntAsync * 3;
        anim = GetComponentInChildren<Animator>();
        outline = GetComponentInChildren<Outline>();
        if(outline != null)
        {
            outlineMaxThickness = outline.OutlineWidth;
            outline.OutlineWidth = 0f;
        } 
        StartCoroutine(CourotineAsyncUpdateIA());
    }

    protected virtual void Update() 
    {
        if(outline != null)
        {
            if(state.TimeS == SpeedState.Slowed && outline.OutlineWidth < outlineMaxThickness)
            {
                //Debug.Log("Increase Outline");
                outline.OutlineWidth += Time.unscaledDeltaTime;
            }
            else if(state.TimeS == SpeedState.Running && outline.OutlineWidth > 0)
            {
                //Debug.Log("MINUS Outline");
                outline.OutlineWidth -= Time.unscaledDeltaTime;
            }
        } 
    }

    protected IEnumerator CourotineAsyncUpdateIA()
    {
        updateRate = Random.Range(updateRateRNG[0], updateRateRNG[1]);

        yield return new WaitForSeconds(updateRate);

        rgbd.velocity = Vector3.zero;

        if(tauntTimerAsync >= 0)
        {
            tauntTimerAsync--;
            //Debug.Log("Current Taunt = " + tauntTimerAsync + name);
        }
        else  AsyncUpdateIA();

        StartCoroutine(CourotineAsyncUpdateIA());
    }

    protected virtual void AsyncUpdateIA()
    {
        
    }
    public bool IsPlayerAlive()
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
