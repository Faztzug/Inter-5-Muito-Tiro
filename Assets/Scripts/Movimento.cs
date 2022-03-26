using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [Header("Character Values")]
    [SerializeField]  private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private Transform lookAtObj;
    private CharacterController controller;
    private Camera cam;
    private Animator anim;

    [Header("Gravity Values")]
    [SerializeField] private float gravity = 1f;
    private float gravityAcceleration;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        lookAtObj = cam.transform.GetChild(0);
    }

    private void Update()
    {
        Movement();
        Animations();
    }

    private void Movement()
    {
        MoveRotation();

        MoveInput();
    }

    private void Animations()
    {
        
    }

    void OnAnimatorIK()
    {
        //limitando a rotacao da cabeca
        Vector3 frente = transform.forward;
        Vector3 direcaoAlvo = lookAtObj.transform.position - transform.position;
        float angulo = Vector3.Angle(frente, direcaoAlvo);

        anim.SetLookAtPosition(lookAtObj.position);

        if (angulo < 70 )
        {
            anim.SetLookAtWeight(1);
        }
        else
        {
            anim.SetLookAtWeight(1);
        }
    }

    private void MoveRotation()
    {
        var camRotation = cam.transform.rotation;
        var objRotation = transform.rotation;
        Vector3 setRotation = new Vector3(objRotation.eulerAngles.x, camRotation.eulerAngles.y, objRotation.eulerAngles.z);
        transform.eulerAngles = setRotation;
    }

    private void MoveInput()
    {
        Vector3 vertical = Input.GetAxis("Vertical") * transform.forward;
        Vector3 horizontal = Input.GetAxis("Horizontal") * cam.transform.right;

        if(controller.isGrounded)
        {
            gravityAcceleration = 0f;
            if(Input.GetButtonDown("Jump")) gravityAcceleration = jumpForce;
            else gravityAcceleration = -gravity * 10f * Time.deltaTime;
        }
        else
        {
            gravityAcceleration -= gravity * Time.deltaTime;
        }

        Vector3 movement = (vertical + horizontal) * Time.deltaTime;
        movement.y = gravityAcceleration * Time.deltaTime;

        controller.Move(movement * speed);

        anim.SetFloat("Velocidade", Mathf.Abs(vertical.magnitude));
    }
}