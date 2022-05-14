using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    public virtual void Start()
    {
        health = maxHealth;
    }

    public virtual void UpdateHealth(float value = 0, Item item = null)
    {
        health += value;

        if(health > maxHealth) health = maxHealth;
        if(health <= 0) DestroyCharacter();

        Debug.Log(name+" Health: " + value);
    }

    public virtual void DestroyCharacter()
    {
        if(TryGetComponent<EnemyDrop>(out EnemyDrop drop))
        {
            drop.Drop(); 
        }
        if(TryGetComponent<EnemyIA>(out EnemyIA enemy))
        {
            enemy.GivePlayerFocusOnDeath();
        }
        Destroy(this.gameObject);
    }
}
