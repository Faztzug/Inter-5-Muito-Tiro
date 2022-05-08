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
            hp.UpdateHealth(ammount,this);
        }
    }
}
