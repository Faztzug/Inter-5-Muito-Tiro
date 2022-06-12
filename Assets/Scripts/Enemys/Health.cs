using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected AudioSource source;
    [SerializeField] protected AudioClip damageSound;
    protected Animator anim;
    protected EnemyIA enemy;
    public virtual void Start()
    {
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
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
            if(enemy != null)
            {
                enemy.Taunt();
                enemy.UpdateHealth(health, maxHealth);
            } 
            if(source != null) source.PlayOneShot(damageSound);
            else Debug.LogError("NO AUDIO SOURCE FOR DAMAGE");
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
