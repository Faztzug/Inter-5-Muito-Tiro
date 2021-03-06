using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private EnemyGunner enemy;
    public int loadedAmmo = 6;
    public int maxLoadedAmmo = 6;
    [SerializeField] [Range(0, 7200)] private int extraAmmo = 12;
    [SerializeField] private int maxExtraAmmo = 720;
    [SerializeField] private List<Bullet> enemyBullets;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletTimer = 3f;
    [SerializeField] private Transform shootPosition;
    public bool trigger = false;
    private GameState state;
    [SerializeField] [Range(0,180)] protected float rng;
    [SerializeField] private float damage = -1f;

    public AudioSource source;
    public AudioClip fireClip;
    public AudioClip triggerClip;
    public AudioClip triggerFailClip;
    public AudioClip reloadClip;
    public AudioClip reloadFullClip;

    void Start()
    {
        //loadedAmmo = maxLoadedAmmo;
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        state = enemy.state;
        enemyBullets = state.enemyBullets.bullets;
    }

    public void Trigger()
    {
        if(!trigger && loadedAmmo > 0)
        {
            source.PlayOneShot(triggerClip);
            trigger = true;
        }
         
    }
    public void Reload()
    {
        if(loadedAmmo < maxLoadedAmmo && extraAmmo > 0)
        {
            loadedAmmo++;
            extraAmmo--;
            source.PlayOneShot(reloadClip);
            if(loadedAmmo == maxLoadedAmmo) source.PlayOneShot(reloadFullClip);
        }
         
    }

    public void Fire()
    {
        if(trigger)
        {
            source.PlayOneShot(fireClip);
            loadedAmmo --;
            trigger = false;


            foreach (Bullet bullet in enemyBullets)
            {
                if(bullet.gameObject.activeSelf == false)
                {
                    ShootingBullet(bullet);
                    return;
                }
            }
            
            //Debug.Log("all enemy bullets active");

            var newBullet = Instantiate(bulletPrefab.GetComponent<Bullet>());
            newBullet.transform.SetParent(null);
            enemyBullets.Add(newBullet);
            ShootingBullet(newBullet);
        }
        
    }
    private void ShootingBullet(Bullet bullet)
    {
        SpawnBullet(bullet);

        var target = state.RandomBodyPart().position;
        if(target != Vector3.zero)
        {
            bullet.transform.LookAt(target);
            var rand = Random.Range(rng * -1f, rng);
            var brot = bullet.transform.rotation;
            //Debug.Log(bullet.transform.rotation);
            bullet.transform.Rotate(new Vector3(rand,rand,rand));
            //Debug.Log(bullet.transform.rotation);
            //Debug.Log(rand);
            Debug.DrawRay(bullet.transform.position, bullet.transform.forward * 15f, Color.cyan, 2f);
            return;
        }
    }

    private void SpawnBullet(Bullet bullet)
    {
        bullet.hit = false;
        bullet.StopAllCoroutines();
        bullet.Respawn();
        bullet.transform.position = shootPosition.position;
        bullet.transform.localRotation = shootPosition.rotation;
        bullet.gameObject.SetActive(true);
        bullet.DisableBullet(bulletTimer);
        bullet.damage = damage;
    }
}
