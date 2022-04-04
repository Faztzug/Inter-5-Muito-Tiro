using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{ 
    public int ammount;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) CollectItem(other);
    }

    public void CollectItem(Collider info)
    {
        Destroy(this.gameObject);
    }
}
