using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private MenuPause menu;
    private GameState state;
    [SerializeField] private TMP_Dropdown graphics;
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Slider volume;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sensibility;
    [SerializeField] private Slider accelaration;

    void Start()
    {
        state = menu.state;
        LoadMouseSliders();
    }

    public void LoadMouseSliders()
    {
        sensibility.value = state.MouseSensibility;
        accelaration.value = accelaration.maxValue - state.MouseAccelaration;

        //Debug.Log("accel " + state.MouseAccelaration);
        //Debug.Log("value " + accelaration.value);
    }

    
}
