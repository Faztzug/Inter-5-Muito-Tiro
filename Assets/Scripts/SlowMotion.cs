using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{ 
    public float slowMotionTimeScale;
    public float lerpTimeChange = 0.1f;
    public bool focusActive = false;
    private float startTimeScale;
    private float startFixedDeltaTime;

    void Start()
    {
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetButtonDown("Focus"))
        {
            focusActive = !focusActive;
            Debug.Log(focusActive);
        }

        if(focusActive) StartSlowMotion();
        else StopSlowMotion();
    }

    private void StartSlowMotion()
    {
        float currentTime = Time.timeScale;
        
        if(currentTime != slowMotionTimeScale)
        {
            
            if(currentTime > slowMotionTimeScale) currentTime -= lerpTimeChange * Time.unscaledDeltaTime;
            if(currentTime < slowMotionTimeScale) currentTime = slowMotionTimeScale;

        Time.timeScale = currentTime;
        Time.fixedDeltaTime = startFixedDeltaTime * currentTime;
        }

    }

    private void StopSlowMotion()
    {
        float currentTime = Time.timeScale;
        
        if(currentTime != startTimeScale)
        {
            
            if(currentTime < startTimeScale) currentTime += lerpTimeChange * Time.unscaledDeltaTime;
            if(currentTime > startTimeScale) currentTime = startTimeScale;

        Time.timeScale = currentTime;
        Time.fixedDeltaTime = startFixedDeltaTime * currentTime;
        }
    }
}
