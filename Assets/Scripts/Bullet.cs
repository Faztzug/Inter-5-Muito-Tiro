using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 moveVector;
    private float speed;
    private float gravity;
    [SerializeField] private Rigidbody rgbd;
    [SerializeField] private float damage;

    void Start()
    {
        speed = moveVector.z;
        gravity = moveVector.y;
    }

    void Update()
    {
        Vector3 move = transform.position + ((transform.forward * speed) * Time.deltaTime);
        move.y += gravity * Time.deltaTime;
        
        rgbd.MovePosition(move);
        //transform.Translate(speed * Time.deltaTime);
        gravity += moveVector.y * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        BulletHit(collisionInfo);
        
    }
    public void BulletHit(Collision collisionInfo)
    {
        gameObject.SetActive(false);

        if(collisionInfo.gameObject.GetComponent<Health>())
        {
            collisionInfo.gameObject.GetComponent<Health>().UpdateHealth(damage);
        }

        Debug.Log("bye bye lemon");
    }

    public void Respawn()
    {
        gravity = moveVector.y;
        speed = moveVector.z;
        transform.position = Vector3.zero;
        transform.SetPositionAndRotation(Vector3.zero, new Quaternion(0,0,0,0));
        rgbd.angularVelocity = Vector3.zero;
        rgbd.velocity = Vector3.zero;
    }
    
    public void DisableBullet(float time)
    {
        StartCoroutine(DisableTimer(time));
    }

    IEnumerator DisableTimer(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
