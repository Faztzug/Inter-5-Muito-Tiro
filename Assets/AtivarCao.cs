using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarCao : MonoBehaviour //Na verdade ta invertido com o AtivarGatilho
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            //Debug.Log("Recarregou");
            anim.SetBool("Engatilhar", true);
        }
        else
        {
            anim.SetBool("Engatilhar", false);
        }
    }
}
