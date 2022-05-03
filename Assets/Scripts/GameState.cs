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
    public SpeedState TimeS {get; set;} = SpeedState.Running;


}
