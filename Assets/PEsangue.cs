using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEsangue : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticle;

    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            bloodParticle.SetActive(true);
        }
        else
            {
                bloodParticle.SetActive(false);
            }
        
    }

    
}
