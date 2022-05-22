using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    protected Animator anim;
    protected EnemyIA enemy;
    public virtual void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        if(TryGetComponent<EnemyIA>(out EnemyIA enemy))
        {
            this.enemy = enemy;
        }
    }

    public virtual void UpdateHealth(float value = 0, Item item = null)
    {
        health += value;

        if(health > maxHealth) health = maxHealth;
        if(health <= 0) DestroyCharacter();

        if(value < 0)
        {
            if(anim != null) anim.SetTrigger("Damage");
            if(enemy != null) enemy.Taunt();
        }

        //Debug.Log(name+" Health: " + value);
    }

    public virtual void DestroyCharacter()
    {
        if(TryGetComponent<EnemyDrop>(out EnemyDrop drop))
        {
            drop.Drop(); 
        }
        if(TryGetComponent<EnemyIA>(out EnemyIA enemy))
        {
            enemy.EnemyDeath();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
