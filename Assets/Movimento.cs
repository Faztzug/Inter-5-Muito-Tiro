using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    CharacterController controller;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 vertical = Input.GetAxis("Vertical") * transform.forward;
        Vector3 horizontal = Input.GetAxis("Horizontal") * transform.right;

        if (vertical.magnitude > 0.1 || horizontal.magnitude > 0.1)
        {
            Vector3 direcaoCamera = Camera.main.transform.forward;
            transform.forward = direcaoCamera;
        }

        controller.SimpleMove(vertical * 3);
        controller.SimpleMove(horizontal * 3);
    }
}