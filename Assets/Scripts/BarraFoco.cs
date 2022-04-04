using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraFoco : MonoBehaviour
{
    public Slider Barra;
    public Slider Cooldown;
    private bool stopTimer;

    void Start()
    {
        stopTimer = true;
        Barra.maxValue = 4;
        Cooldown.maxValue = 10;
    }

    void Update()
    {
        if (Input.GetButtonDown("Focus")) //Qnd apertar comeca a conta
        {
            stopTimer = !stopTimer;
        }

        if (stopTimer == false) //Conta que desce a barra
        {
            Barra.value -= Time.unscaledDeltaTime;
        }
    }
}
