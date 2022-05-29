using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum SpeedState
{
    Running,
    Slowed,
    Paused
}

public class GameState : MonoBehaviour
{
    public const float kDefaultMouseSpeedY = 2;
    public const float kDefaultMouseSpeedX = 200;
    public const float kDefaultMouseAccelarationY = 0.1f;
    public const float kDefaultMouseAccelarationX = 0.2f;

    [Header("Save Data")]
    [Range(0.1f, 2f)] [SerializeField] private float mouseSensibility;
    public float MouseSensibility 
    { 
        get => mouseSensibility; 
        set 
        {
            mouseSensibility = value;
            SetMouseSpeed();
        } 
        
    }
    [Range(0.1f, 5f)] [SerializeField] private float mouseAccelaration;
    public float MouseAccelaration 
    { 
        get => mouseAccelaration; 
        set 
        {
            mouseAccelaration = value;
            SetMouseSpeed();
        } 
    }

    [Header("Game Data")]
    [SerializeField] private CinemachineFreeLook freeLook;
    public Transform bodyPartHead;
    public Transform bodyPartChest;
    public Transform bodyPartPelvis;
    public Transform bodyPartLeftLeg;
    public Transform bodyPartLeftArm;
    public Transform bodyPartRightLeg;
    public Transform bodyPartRightArm;
    public List<Transform> bodyPartsList; 
    public SpeedState TimeS {get; set;} = SpeedState.Running;
    public EnemyBullets enemyBullets;
    public bool playerDead = false;
    public bool GodMode = false;
    public bool gunTrigger;

    void Start()
    {
        enemyBullets = FindObjectOfType<EnemyBullets>();
        
        bodyPartsList.Add(bodyPartHead);
        bodyPartsList.Add(bodyPartChest);
        bodyPartsList.Add(bodyPartPelvis);
        bodyPartsList.Add(bodyPartLeftLeg);
        bodyPartsList.Add(bodyPartLeftArm);
        bodyPartsList.Add(bodyPartRightLeg);
        bodyPartsList.Add(bodyPartRightArm);
    }

    public Transform RandomBodyPart()
    {
        var rng = Random.Range(0, bodyPartsList.Count);
        return bodyPartsList[rng];
    }
    private void SetMouseSpeed()
    {
        freeLook.m_YAxis.m_MaxSpeed = kDefaultMouseSpeedY * mouseSensibility;
        freeLook.m_XAxis.m_MaxSpeed = kDefaultMouseSpeedX * mouseSensibility;
        
        freeLook.m_YAxis.m_AccelTime = kDefaultMouseAccelarationY * mouseAccelaration;
        freeLook.m_XAxis.m_AccelTime = kDefaultMouseAccelarationX * mouseAccelaration;
    }

    void Update()
    {
        if(TimeS == SpeedState.Paused)
        {
            if (Input.GetButtonDown("Cheat"))
            {
                GodMode = !GodMode;
                Debug.LogWarning("GOD MODE: " + GodMode);
            }
        }
        //SetMouseSpeed();
    }


}
