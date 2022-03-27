using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPosition;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) Fire();
    }

    public void Fire()
    {
        Instantiate(bulletPrefab, shootPosition.position, cam.transform.rotation);
        
        Debug.DrawRay(shootPosition.position, cam.transform.forward * 100f, Color.green, 10f);
        Physics.Raycast(shootPosition.position, cam.transform.forward * 100f, 100f);
    }
}
