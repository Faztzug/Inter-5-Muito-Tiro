using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int maxDrops = 2;
    [SerializeField] private GameObject[] itens = new GameObject[2];

    [SerializeField] private float[] dropChance = new float[2]; //0 a 100
    
    public void Drop()
    {
        Debug.Log("Droping Itens");
        Vector3 dropPos = transform.position;
        dropPos.y =+ 5;
        Quaternion dropRot = transform.rotation;
        for (int i = 0; i < itens.Length; i++)
        {
            float rng = Random.Range(0,100);
            if(rng <= dropChance[i])
            {
                var item = Instantiate(itens[i], dropPos, dropRot);
                item.GetComponent<Rigidbody>().AddForce(item.transform.up * 5f, ForceMode.Impulse);
                if(i >= maxDrops) return;
            } 
        }
    }

    void OnDestroy()
    {
        //Drop();
    }
}
