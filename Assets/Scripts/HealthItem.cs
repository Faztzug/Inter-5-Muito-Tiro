using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public override void CollectItem(Collider info)
    {
        base.CollectItem(info);
        if(info.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth hp))
        {
            var porcent = ammount / 10f;
            var value = hp.maxHealth * porcent;
            Debug.Log("HP ammount: " + ammount);
            Debug.Log("HP porcent: " + porcent);
            Debug.Log("HP+: " + value);
            hp.UpdateHealth(value,this);
        }
    }
}
