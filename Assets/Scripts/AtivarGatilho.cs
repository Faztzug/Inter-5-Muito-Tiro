using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarGatilho : MonoBehaviour //Na verdade ta invertido com o AtivarCao
{
    [SerializeField] MenuPause menu;
    private Animator anim;
    private GameState state;

    void Start()
    {
        anim = GetComponent<Animator>();
        state = menu.state;
    }


    void Update()
    {
        // if (Input.GetButtonDown("Trigger"))
        // {
        //     Debug.Log("Recarregou");
        //     anim.SetBool("Acionado", true);
        // }
        // else
        // {
        //     anim.SetBool("Acionado", false);
        // }
        anim.SetBool("Acionado", state.gunTrigger);
    }
}
