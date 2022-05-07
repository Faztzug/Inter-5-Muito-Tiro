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
        //Debug.Log(hit.gameObject.name);
        if((hit.gameObject.CompareTag("Melle") 
        || hit.gameObject.CompareTag("Cactu")) 
        && time < 0)
        {
            var damage = contactDamage;
            if(hit.gameObject.TryGetComponent<ContactDamage>(out ContactDamage contact))
            {
                damage = contact.Damage;
            }
            Debug.Log("T Ouch!");
            hp.UpdateHealth(damage);
            time = invicibilityTime;
        }
    }
    void OnCollisionEnter(Collision hit)
    {
        if((hit.gameObject.CompareTag("Melle") 
        || hit.gameObject.CompareTag("Cactu")) 
        && time < 0)
        {
            var damage = contactDamage;
            if(hit.gameObject.TryGetComponent<ContactDamage>(out ContactDamage contact))
            {
                damage = contact.Damage;
            }
            Debug.Log("T Ouch!");
            hp.UpdateHealth(damage);
            time = invicibilityTime;
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        if((hit.gameObject.CompareTag("Melle") 
        || hit.gameObject.CompareTag("Cactu")) 
        && time < 0)
        {
            var damage = contactDamage;
            if(hit.gameObject.TryGetComponent<ContactDamage>(out ContactDamage contact))
            {
                damage = contact.Damage;
            }
            Debug.Log("T Ouch!");
            hp.UpdateHealth(damage);
            time = invicibilityTime;
        }
    }
}
