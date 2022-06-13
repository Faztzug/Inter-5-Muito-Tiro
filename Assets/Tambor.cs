using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tambor : MonoBehaviour
{
    [SerializeField] private MenuPause menu;
    private GameState state;
    [SerializeField] private List<GameObject> bullets;
    private int index;
    private int Index 
    {
        get => index;
        set 
        {
            if(value > maxIndex) index = 0;
            else if(value < 0) index = maxIndex;
            else index = value;
        }
    }
    private int maxIndex = 5;
    [SerializeField] private float rotateSpeed = 20f;
    private float rotateAmmount = 60f;
    private bool isRotating = false;
    private float toRotate = 0f;

    void Start()
    {
        state = menu.state;
        maxIndex = bullets.Count - 1;
    }

    public void TriggerRotate()
    {
        if(toRotate >= 360f) toRotate = 0f;
        toRotate += rotateAmmount;
        isRotating = true;
        
        Index++;
    }

    public void FireBullet()
    {
        bullets[Index].SetActive(false);
    }

    public void ReloadBullet()
    {
        var next = Index;
        next++;
        if(next > maxIndex) next = 0;

        for (int i = next; i < bullets.Count; i++)
        {
            if(bullets[i].activeSelf == false)
            {
                bullets[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < next; i++)
        {
            if(bullets[i].activeSelf == false)
            {
                bullets[i].SetActive(true);
                return;
            }
        }
    }

    void Update()
    {
        if(isRotating)
        {
            var speed = state.TimeS == SpeedState.Slowed ? rotateSpeed * 2f : rotateSpeed;
            var rot = transform.rotation.eulerAngles;
            rot.z += speed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, rot.z);

            if(rot.z >= toRotate) isRotating = false;
        }
    }
}
