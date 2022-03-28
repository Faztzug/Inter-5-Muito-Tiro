using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    public void UpdateHealth(float value)
    {
        health += value;

        if(health > maxHealth) health = maxHealth;
        if(health <= 0) Destroy(this.gameObject);

        Debug.Log(name+" Health: " + value);
    }
}
