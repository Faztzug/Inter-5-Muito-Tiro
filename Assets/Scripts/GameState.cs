using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedState
{
    Running,
    Slowed,
    Paused
}

public class GameState : MonoBehaviour
{
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
    public bool GodMode = false;

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
    }


}
