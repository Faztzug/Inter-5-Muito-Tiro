using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKGunner : MonoBehaviour
{
    private float weightIKhand;
    private Animator anim;
    private EnemyGunner enemy;
    private GameState state;
    private float distance => enemy.distance;
    private float shootingDistance => enemy.shootingDistance;
    private bool alive => enemy.alive;
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponentInParent<EnemyGunner>();
        state = enemy.player.GetComponent<GameState>();
        weightIKhand = enemy.weightIKhand;
    }

    void OnAnimatorIK()
    {
        if(!enemy.IsPlayerAlive() || anim == null) return;

        Vector3 frente = transform.forward;
        Vector3 direcaoAlvo = transform.forward * 1000f;
        float angulo = Vector3.Angle(frente, direcaoAlvo);

        //Debug.Log("anim " + anim);
        //Debug.Log("chest? " + state?.bodyPartChest?.position == null);
        anim.SetLookAtPosition(state.bodyPartChest.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, state.bodyPartChest.position);
    
        if(distance < shootingDistance && alive)
        {
            anim.SetLookAtWeight(1);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weightIKhand);
        }
        else
        {
            anim.SetLookAtWeight(0);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
    }
}
