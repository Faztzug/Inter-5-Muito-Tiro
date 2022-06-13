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
    [SerializeField] private List<Bullet> bullets;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject Flash;
    [SerializeField] private float bulletTimer = 3f;
    [SerializeField] private Transform shootPosition;
    private Camera cam;
    //protected bool trigger = false;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private float damage = -1f;
    private Movimento moveScript;
    private GameState state;
    public AudioSource source;
    public AudioSource engate;
    public AudioClip fireClip;
    public AudioClip triggerClip;
    public AudioClip reloadClip;
    public AudioClip reloadFailClip;
    [SerializeField] private SlowMotion slowMotion;
    private Animator anim;
    [SerializeField] private Tambor tambor;
    [SerializeField] private float ReloadCoolDown = 0.2f;
    private float reloadTimer = 0;
   
    void Start()
    {
        state = GetComponent<GameState>();
        cam = Camera.main;
        foreach (Bullet bullet in bullets)
        {
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(false);
            bullet.slowMotion = slowMotion;
        }

        if(ammoText == null) ammoText = GameObject.FindGameObjectWithTag("ammoText").GetComponent<TextMeshProUGUI>();
        UpdateAmmoText();
        Cursor.lockState = CursorLockMode.Locked;
        moveScript = GetComponent<Movimento>();
        anim = GetComponentInChildren<Animator>();
        if(tambor == null) tambor = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<Tambor>();
    }

    void Update()
    {
        reloadTimer -= Time.deltaTime;
        if (state.TimeS != SpeedState.Paused)
        {
            if (Input.GetButtonDown("Fire"))
            {
                if (loadedAmmo == 0)
                {
                    source.PlayOneShot(reloadFailClip);
                }
                Fire();
            }
            if (Input.GetButtonDown("Trigger")) Trigger();
            if (Input.GetButtonDown("Reload") && reloadTimer < 0) Reload();
        }
    }

    public void Trigger()
    {
        if(!state.gunTrigger && loadedAmmo > 0)
        {
            state.gunTrigger = true;
            source.PlayOneShot(triggerClip);
            tambor.TriggerRotate();
        }
         
    }
    public void Reload()
    {
        if(state.GodMode) extraAmmo = maxExtraAmmo;
        
        if(loadedAmmo < maxLoadedAmmo && extraAmmo > 0)
        {
            anim.SetTrigger("Reload");
            source.PlayOneShot(reloadClip);
            tambor.ReloadBullet();
            loadedAmmo++;
            extraAmmo--;
            UpdateAmmoText();
            reloadTimer = ReloadCoolDown;
        }
    }

    public void Fire()
    {
        if(state.gunTrigger)
        {
            source.PlayOneShot(fireClip);
            loadedAmmo --;
            UpdateAmmoText();
            state.gunTrigger = false;

            Debug.Log("Atirou");
            anim.SetTrigger("Atirando");
            tambor.FireBullet();


            foreach (Bullet bullet in bullets)
            {
                if(bullet.gameObject.activeSelf == false)
                {
                    ShootingBullet(bullet);
                    return;
                }
            }
           
            Debug.Log("all bullets active");

            var newBullet = Instantiate(bulletPrefab.GetComponent<Bullet>());
            newBullet.transform.SetParent(null);
            bullets.Add(newBullet);
            ShootingBullet(newBullet);
        }
        
    }
    private void ShootingBullet(Bullet bullet)
    {
        SpawnBullet(bullet);

        var target = moveScript.LookAtRayHit;
        if(target != Vector3.zero)
        {
            bullet.transform.LookAt(target);
            Debug.DrawLine(shootPosition.position, target, Color.blue, 10f);
        }
        var flash = Instantiate(Flash, bullet.transform.position, bullet.transform.rotation);
        Destroy(flash.gameObject, 1f);
    }

    private void SpawnBullet(Bullet bullet)
    {
        
        bullet.hit = false;
        bullet.StopAllCoroutines();
        bullet.Respawn();
        bullet.transform.position = shootPosition.position;
        bullet.transform.localRotation = cam.transform.rotation;
        bullet.gameObject.SetActive(true);
        bullet.DisableBullet(bulletTimer);
        bullet.damage = damage;
    }

    public void UpdateAmmoText()
    {
        //ammoText.text = loadedAmmo + " / " + extraAmmo;
        ammoText.text = extraAmmo.ToString();
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
