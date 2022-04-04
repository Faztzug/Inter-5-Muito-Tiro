using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{ 
    [SerializeField] protected int ammount;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) CollectItem(other);
    }

    public virtual void CollectItem(Collider info)
    {
        
    }

    public virtual void DestroyItem()
    {
        this.gameObject.SetActive(false);
    }
}
