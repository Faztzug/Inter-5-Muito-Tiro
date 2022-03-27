using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 speed;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(this.gameObject);
        Debug.Log("bye bye lemon");
    }
}
