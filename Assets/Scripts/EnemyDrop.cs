using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int maxDrops = 2;
    [SerializeField] private GameObject[] itens = new GameObject[2];

    [SerializeField] private float[] dropChance = new float[2]; //0 a 100
    

    void OnDestroy()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        for (int i = 0; i < itens.Length; i++)
        {
            float rng = Random.Range(0,100);
            if(rng <= dropChance[i])
            {
                Instantiate(itens[i], dropPos, dropRot);
                if(i >= maxDrops) return;
            } 
        }
    }
}
