using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarGatilho : MonoBehaviour //Na verdade ta invertido com o AtivarCao
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Trigger"))
        {
            Debug.Log("Recarregou");
            anim.SetBool("Acionado", true);
        }
        else
        {
            anim.SetBool("Acionado", false);
        }
    }
}
