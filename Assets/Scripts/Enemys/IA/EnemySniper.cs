using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySniper : EnemyGunner
{
    protected override void GunnerBehavior()
    {
        if(IsPlayerAlive())
        {
            playerPos = player.position;
            playerPos.y = 0;
        }

        pos = transform.position;
        pos.y = 0;

        distance = Vector3.Distance(pos, playerPos);
        
        ReadyFire();
    }
}
