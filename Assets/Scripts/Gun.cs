using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletTimer = 3f;
    [SerializeField] private Transform shootPosition;
    private Camera cam;
    [SerializeField] private bool trigger = false;

    void Start()
    {
        cam = Camera.main;
        foreach (Bullet bullet in bullets)
        {
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire")) Fire();
        if(Input.GetButtonDown("Trigger")) Trigger();
    }

    public void Trigger()
    {
        if(!trigger)
        {
            trigger = true;
            Debug.Log("TRIGGER");
        }
         
    }

    public void Fire()
    {
        if(trigger)
        {
            trigger = false;
            Debug.DrawRay(shootPosition.position, cam.transform.forward * 100f, Color.green, 10f);
            Physics.Raycast(shootPosition.position, cam.transform.forward * 100f, 100f);
            

            foreach (Bullet bullet in bullets)
            {
                if(bullet.gameObject.activeSelf == false)
                {
                    SpawnBullet(bullet);

                    return;
                }
            }

            Debug.Log("all bullets active");
            SpawnBullet(bullets[0]);
            
            //Instantiate(bulletPrefab, shootPosition.position, cam.transform.rotation);

            
        }
        
    }

    private void SpawnBullet(Bullet bullet)
    {
        bullet.StopAllCoroutines();
        bullet.Respawn();
        bullet.transform.position = shootPosition.position;
        bullet.transform.localRotation = cam.transform.rotation;
        bullet.gameObject.SetActive(true);
        bullet.DisableBullet(bulletTimer);
    }
}
