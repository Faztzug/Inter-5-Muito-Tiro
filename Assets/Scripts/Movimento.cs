using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [Header("Character Values")]
    [SerializeField]  private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private CharacterController controller;
    private Camera cam;

    [Header("Gravity Values")]
    [SerializeField] private float gravity = 1f;
    private float gravityAcceleration;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 vertical = Input.GetAxis("Vertical") * cam.transform.forward;
        Vector3 horizontal = Input.GetAxis("Horizontal") * cam.transform.right;

        // if (vertical.magnitude > 0.1 || horizontal.magnitude > 0.1)
        // {
        //     Vector3 direcaoCamera = Camera.main.transform.forward;
        //     transform.forward = direcaoCamera;
        // }
        if(controller.isGrounded)
        {
            if(Input.GetButtonDown("Jump")) gravityAcceleration = jumpForce;
            else gravityAcceleration = 0f;
        }
        else
        {
            gravityAcceleration -= gravity * Time.deltaTime;
        }

        

        Vector3 movement = (vertical + horizontal) * Time.deltaTime;
        movement.y = gravityAcceleration * Time.deltaTime;


        controller.Move(movement * speed);

        // controller.SimpleMove(vertical * speed);
        // controller.SimpleMove(horizontal * speed);
    }
}