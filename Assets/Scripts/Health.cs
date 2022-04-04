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

    public virtual void UpdateHealth(float value = 0)
    {
        health += value;

        if(health > maxHealth) health = maxHealth;
        if(health <= 0) Destroy(this.gameObject);

        Debug.Log(name+" Health: " + value);
    }
}
