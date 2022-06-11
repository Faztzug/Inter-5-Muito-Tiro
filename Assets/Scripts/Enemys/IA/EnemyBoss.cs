using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyBoss : EnemyGunner
{
    [Header("BOSS")]
    [SerializeField] protected Transform goTo;
    [SerializeField] protected Transform[] patroll;
    protected int PatrollIndex;
    [SerializeField] protected float bossEndTimer = 5f;
    [SerializeField] protected string nextScene;
    [SerializeField] protected Slider healthBar;

    protected override void Start()
    {
        base.Start();
    }
    protected override void GunnerBehavior()
    {
        ReadyFire();
        if(goTo != null) GoToTarget();
        if(goTo == null) PatrollMove();
    }
    protected override void ReadyFire()
    {
        distance = Vector3.Distance(playerPos, transform.position);
        base.ReadyFire();
    }
    protected override void OnAnimatorIK()
    {
        Debug.Log("anim IK");
        //Debug.Log("PlayerHealth alive? " + IsPlayerAlive());
        Debug.Log("anim is null? " + anim == null);
        Debug.Log("target chest is null? " + pState?.bodyPartChest?.position == null);
        base.OnAnimatorIK();
    }

    protected virtual void GoToTarget()
    {
        pos = transform.position;
        pos.y = 0;

        if(agent.isOnNavMesh)
        {
            if(IsPlayerAlive())
            {
                agent.SetDestination(goTo.position);
                anim.SetFloat("Movement", 1);
            }
            else 
            {
                anim.SetFloat("Movement", 0);
            }
        }
        else
        {
            Debug.LogError(gameObject.name + " BOSS AI OUT OF NAV MESH!");
        }

        var targetPos = goTo.position;
        targetPos.y = 0;
        if(Vector3.Distance(pos, targetPos) < 0.5)
        {
            goTo = null;
        }
    }
    protected virtual void PatrollMove()
    {
        if(patroll.Length <= 0 || Vector3.Distance(transform.position, patroll[PatrollIndex].position) > 1f) return;

        PatrollIndex++;

        if(PatrollIndex > patroll.Length-1) PatrollIndex = 0;

        
        agent.SetDestination(patroll[PatrollIndex].position);
    }
    public override void EnemyDeath()
    {
        base.EnemyDeath();
        StartCoroutine(BossEnd());
    }
    protected IEnumerator BossEnd()
    {
        Debug.Log("Boss End " + bossEndTimer);
        yield return new WaitForSeconds(bossEndTimer);
        
        Debug.Log("Load Next Scene");
        SceneManager.LoadScene(nextScene);
    }
    public override void UpdateHealth(float health, float maxHealth)
    {
        base.UpdateHealth(health, maxHealth);
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
}
