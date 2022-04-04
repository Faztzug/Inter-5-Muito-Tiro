using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Slider bar;

    public override void Start()
    {
        base.Start();
        bar.maxValue = maxHealth;
        UpdateHealth();
    }

    public override void UpdateHealth(float value = 0, Item item = null)
    {
        if(health < maxHealth && item != null) item.DestroyItem();
         
        base.UpdateHealth(value);
        bar.value = health;
    }
}
