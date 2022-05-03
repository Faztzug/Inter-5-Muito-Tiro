using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] private int loadedAmmo = 6;
    [SerializeField] private int maxLoadedAmmo = 6;
    [SerializeField] [Range(0, 72)] private int extraAmmo = 12;
    [SerializeField] private int maxExtraAmmo = 72;
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletTimer = 3f;
    [SerializeField] private Transform shootPosition;
    private Camera cam;
    protected bool trigger = false;
    [SerializeField] private TextMeshProUGUI ammoText;
    private Movimento moveScript;
    private GameState state;
   
    void Start()
    {
       state = GetComponent<GameState>();
        cam = Camera.main;
        foreach (Bullet bullet in bullets)
        {
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(false);
        }

        UpdateAmmoText();
        Cursor.lockState = CursorLockMode.Locked;
        moveScript = GetComponent<Movimento>();
    }

    void Update()
    {
        if (state.TimeS != SpeedState.Paused)
        {
            if (Input.GetButtonDown("Fire")) Fire();
            if (Input.GetButtonDown("Trigger")) Trigger();
            if (Input.GetButtonDown("Reload")) Reload();
        }
    }

    public void Trigger()
    {
        if(!trigger && loadedAmmo > 0)
        {
            trigger = true;
           
            Debug.Log("TRIGGER");
        }
         
    }
    public void Reload()
    {
        if(loadedAmmo < maxLoadedAmmo && extraAmmo > 0)
        {
            loadedAmmo++;
            extraAmmo--;
            Debug.Log("RELOAD");
            UpdateAmmoText();
        }
         
    }

    public void Fire()
    {
        if(trigger)
        {
            loadedAmmo --;
            UpdateAmmoText();
            trigger = false;
        

            //Physics.Raycast(shootPosition.position, cam.transform.forward * 100f, 100f);


            foreach (Bullet bullet in bullets)
            {
                if(bullet.gameObject.activeSelf == false)
                {
                    SpawnBullet(bullet);

                    var target = moveScript.LookAtRayHit;
                    if(target != Vector3.zero)
                    {
                        bullet.transform.LookAt(target);
                        Debug.DrawLine(shootPosition.position, target, Color.blue, 10f);
                        return;
                    }
                    Debug.DrawRay(shootPosition.position, cam.transform.forward * 100f, Color.green, 10f);
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

    public void UpdateAmmoText()
    {
        ammoText.text = loadedAmmo + " / " + extraAmmo;
    }

    public void GainAmmo(int ammount, Item item)
    {
        if(extraAmmo < maxExtraAmmo)
        {
            extraAmmo += ammount;
            UpdateAmmoText();
            item.DestroyItem();

            if(extraAmmo > maxExtraAmmo) extraAmmo = maxExtraAmmo;
        }
    }
}
