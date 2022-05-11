using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEsangue : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private float timeToDestroy = 3f;

    public void Bleed(Transform bullet)
    {
        var blood = Instantiate(bloodParticle, bullet.position, bullet.rotation, null);
        Destroy(blood, timeToDestroy);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log(this.gameObject.name + " Bleed");
            Bleed(other.transform);
        }
    }

    
}
