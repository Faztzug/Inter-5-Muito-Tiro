using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{ 
    public float slowMotionTimeScale;
    public float lerpTimeChange = 0.1f;
    public bool focusActive = false;
    private float startTimeScale;
    private float startFixedDeltaTime;
    [SerializeField] private GameObject PostProcess;
    private Animator anim;

    public Slider Barra;
    public Slider Cooldown;
    private bool stopTimer;

    void Start()
    {
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
        anim = PostProcess.GetComponent<Animator>();

        stopTimer = true;
        Barra.maxValue = 4;
        Cooldown.maxValue = 10;
    }

    void Update()
    {
        if (Input.GetButtonDown("Focus"))
        {
            focusActive = !focusActive;
            Debug.Log(focusActive);
            stopTimer = false;
        }

        if(focusActive) StartSlowMotion();
        else StopSlowMotion();

        if (Barra.value == 0) //Quando chegar em 0 vai parar
        {
            if (Cooldown.value == 0)
            {
                stopTimer = true;
                Barra.value = 4;
                Cooldown.value = 10;
            }
            Cooldown.value -= Time.deltaTime;
            focusActive = false;
            StopSlowMotion();
        }

        if (stopTimer == false) //Conta que desce a barra
        {
            Barra.value -= Time.deltaTime;
        }
    }

    private void StartSlowMotion()
    {
      
        anim.SetBool("Foco", true);
        float currentTime = Time.timeScale;
       


        if (currentTime != slowMotionTimeScale)
        {
            
            if(currentTime > slowMotionTimeScale) currentTime -= lerpTimeChange * Time.unscaledDeltaTime;
            if(currentTime < slowMotionTimeScale) currentTime = slowMotionTimeScale;

        Time.timeScale = currentTime;
        Time.fixedDeltaTime = startFixedDeltaTime * currentTime;
        }

    }

    private void StopSlowMotion()
    {
        
        anim.SetBool("Foco", false);
        float currentTime = Time.timeScale;

        if (currentTime != startTimeScale)
        {
            
            if(currentTime < startTimeScale) currentTime += lerpTimeChange * Time.unscaledDeltaTime;
            if(currentTime > startTimeScale) currentTime = startTimeScale;

        Time.timeScale = currentTime;
        Time.fixedDeltaTime = startFixedDeltaTime * currentTime;
        }
    }
}
