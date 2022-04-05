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
    private Vector3 move;
    private bool setedVelocity = false;

    void Start()
    {
        speed = moveVector.z;
        gravity = moveVector.y;
        
        setedVelocity = false;
    }

    void Update()
    {
        //Vector3 move = transform.position + ((transform.forward * speed) * Time.deltaTime);
        //move.y += gravity * Time.deltaTime;

        //rgbd.MovePosition(move);

        // Vector3 move = transform.forward * speed;
        // move.y = gravity;
        
        //rgbd.velocity = move;
        //rgbd.AddForce(transform.forward * speed, ForceMode.Impulse);
        //if(rgbd.velocity.z < gravity && rgbd.velocity.x < gravity && rgbd.velocity.y < gravity) rgbd.AddForce(transform.forward * speed, ForceMode.Impulse);

        if(!setedVelocity)
        {
            move = rgbd.velocity + transform.forward * speed;
            setedVelocity = true;
        }
        
        move.y += gravity * Time.deltaTime;
        rgbd.velocity = move;

        //transform.Translate(speed * Time.deltaTime);
        gravity += moveVector.y * Time.deltaTime;
    }

    void FixedUpdate() 
    {
        
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
        rgbd.velocity = Vector3.zero;
        gravity = moveVector.y;
        speed = moveVector.z;
        transform.position = Vector3.zero;
        transform.SetPositionAndRotation(Vector3.zero, new Quaternion(0,0,0,0));
        rgbd.angularVelocity = Vector3.zero;
        setedVelocity = false;
    }
    
    public void DisableBullet(float time)
    {
        StartCoroutine(DisableTimer(time));
    }

    IEnumerator DisableTimer(float time)
    {
        yield return new WaitForSeconds(time);
        setedVelocity = false;

        gameObject.SetActive(false);
    }
}
