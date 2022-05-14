using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 moveVector;
    private float speed;
    private float gravity;
    [SerializeField] private Rigidbody rgbd;
    public float damage;
    private Vector3 move;
    private bool setedVelocity = false;
    [HideInInspector] public SlowMotion slowMotion;

    void Start()
    {
        speed = moveVector.z;
        gravity = moveVector.y;
        
        setedVelocity = false;
    }

    void Update()
    {
        if(!setedVelocity)
        {
            move = rgbd.velocity + transform.forward * speed;
            setedVelocity = true;
        }
        
        move.y += gravity * Time.deltaTime;
        rgbd.velocity = move;

        gravity += moveVector.y * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        BulletHit(collisionInfo.gameObject);
        
    }
    void OnTriggerEnter(Collider other)
    {
        BulletHit(other.gameObject);
    }
    public void BulletHit(GameObject collision)
    {
        gameObject.SetActive(false);

        if(collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().UpdateHealth(damage);
            if(slowMotion != null) slowMotion.GainFocusPoints(Mathf.Abs(damage));
            //Debug.Log(collisionInfo.gameObject.name + " took " + damage + " of damage!");
        }
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
