using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public override void CollectItem(Collider info)
    {
        base.CollectItem(info);
        info.GetComponent<PlayerHealth>().UpdateHealth(ammount,this);
    }
}
