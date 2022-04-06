using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouch : MonoBehaviour
{
    [SerializeField] private float contactDamage;
    private Health hp;
    [SerializeField] float invicibilityTime = 0.1f;
    private float time;

    void Start()
    {
        hp = GetComponent<Health>();
        time = invicibilityTime;
    }

    void Update()
    {
        time -= Time.unscaledDeltaTime;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name);
        if(hit.gameObject.CompareTag("Enemy") && time < 0)
        {
            Debug.Log("T ouch!");
            hp.UpdateHealth(contactDamage);
            time = invicibilityTime;
        }
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log(collisionInfo.gameObject.name);
        if(collisionInfo.gameObject.CompareTag("Player"))
        {
        }
    }
}
