using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [Header("Character Values")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] [Range(0,1)] private float weightIKhand;
    private Transform lookAtObj;
    public Vector3 LookAtRayHit{get; private set;}
    private CharacterController controller;
    private Camera cam;
    private Animator anim;

    [Header("Gravity Values")]
    [SerializeField] private float gravity = 1f;
    private float gravityAcceleration;
    private GameState state;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip passosClip;
    [HideInInspector] public ReticulaFeedback reticula;


    private void Start()
    {
        state = GetComponent<GameState>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        lookAtObj = cam.transform.GetChild(0);
    }

    private void Update()
    {
        if (state.TimeS != SpeedState.Paused)
        {
            Movement();
            Animations();
            LookAtRayHit = GetRayCastMiddle();
        }
    }

    private void Movement()
    {
        MoveRotation();

        MoveInput();
    }

    private void Animations()
    {
        
    }
    private Vector3 GetRayCastMiddle()
    {
        var layer = 1 << 3;
        layer = ~layer;
        
        RaycastHit rayHit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, 500f, layer))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 500f, Color.red);
            if(rayHit.rigidbody != null && rayHit.rigidbody.gameObject.CompareTag("Enemy") && reticula != null)
            {
                reticula.EnemyState();
            }
            else if(reticula != null)
            {
                reticula.NeutralState();
            }
            return rayHit.point;
        }
        else
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 500f, Color.gray);
            if(reticula != null)
            {
                reticula.NeutralState();
            }
            return Vector3.zero;
        }
    }

    void OnAnimatorIK()
    {
        //limitando a rotacao da cabeca
        Vector3 frente = transform.forward;
        Vector3 direcaoAlvo = lookAtObj.transform.position - transform.position;
        float angulo = Vector3.Angle(frente, direcaoAlvo);

        if(LookAtRayHit != Vector3.zero)
        {
            anim.SetLookAtPosition(LookAtRayHit);
            anim.SetIKPosition(AvatarIKGoal.RightHand, LookAtRayHit);
        }
        else
        {
            anim.SetLookAtPosition(lookAtObj.position);
            anim.SetIKPosition(AvatarIKGoal.RightHand, lookAtObj.position);
        }
        

        if (angulo < 70 )
        {
            anim.SetLookAtWeight(1);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weightIKhand);
        }
        else
        {
            anim.SetLookAtWeight(1);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weightIKhand);
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
        if(Input.GetAxis("Vertical") < 0) vertical = Input.GetAxis("Vertical") * transform.forward / 2;
        Vector3 horizontal = Input.GetAxis("Horizontal") * cam.transform.right * 0.9f;

        if(controller.isGrounded)
        {
            gravityAcceleration = 0f;
            anim.SetBool("isJumping", false);

            if (Input.GetButtonDown("Jump"))
            {
                gravityAcceleration = jumpForce;
                anim.SetBool("isJumping", true);
            }
            else gravityAcceleration = -gravity * 10f * Time.deltaTime;

        }
        else
        {
            gravityAcceleration -= gravity * Time.deltaTime;
        }

        Vector3 movement = (vertical + horizontal) * Time.deltaTime;
        if(Input.GetButton("Sprint")) movement = movement * runSpeed;
        else movement = movement * speed;

        movement.y = gravityAcceleration * Time.deltaTime * speed;
        
        controller.Move(movement);


        anim.SetFloat("Velocidade", Mathf.Abs(vertical.magnitude));

        var velocitylAbs = Mathf.Abs(movement.x) + Mathf.Abs(movement.z);

        if((velocitylAbs > 0) && controller.isGrounded)
        {
            if(audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(passosClip);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}