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
    //private bool stopTimer;
    [SerializeField] private float maxFocusBar = 10f;
    [SerializeField] private float currentFocusBar;
    [SerializeField] private float regenFocusBar;
    private GameState state;
    [SerializeField] private float gainFocusMultplier = 0.5f;

    void Start()
    {
        state = GetComponent<GameState>();
        Time.timeScale = 1f;
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
        anim = PostProcess.GetComponent<Animator>();

        //stopTimer = true;
        currentFocusBar = maxFocusBar;
        Barra.maxValue = maxFocusBar;
    }

    void Update()
    {
        if (state.TimeS != SpeedState.Paused)
        {
            if (Input.GetButtonDown("Focus"))
            {
                focusActive = !focusActive;
                //stopTimer = !stopTimer;
                if(focusActive) state.TimeS = SpeedState.Slowed;
                else state.TimeS = SpeedState.Running;
            }

            UpdateBar();

            if (focusActive && state.playerDead == false) StartSlowMotion();
            else StopSlowMotion();
        }
    }
    private void UpdateBar()
    {
        if(state.GodMode) currentFocusBar += regenFocusBar * 10f * Time.deltaTime;

        //if(focusActive == false) currentFocusBar += regenFocusBar * Time.deltaTime; //regen focus
        if (focusActive) currentFocusBar -= Time.unscaledDeltaTime;
        
        if(currentFocusBar > maxFocusBar) currentFocusBar = maxFocusBar;
        if(currentFocusBar <= 0)
        {
            focusActive = false;
            state.TimeS = SpeedState.Running;
        }

        Barra.value = currentFocusBar;
    }

    public void GainFocusPoints(float value)
    {
        currentFocusBar += value * gainFocusMultplier;
        if(currentFocusBar > maxFocusBar) currentFocusBar = maxFocusBar;
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
