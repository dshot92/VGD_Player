using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    public float speed = 6.0F;
    public float jumpSpeed = 100.0F;
    public float gravity = 2.0F;
    
    private Vector3 moveDirection = Vector3.zero;
    public float rotationSpeed = 5F;

    public float Hdumping = 0.3F;
    public float Vdumping = 0.05F;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //anim.SetFloat("speed", moveDirection.magnitude);
            moveDirection = transform.TransformDirection(moveDirection);
            //anim.SetFloat("direction", transform.TransformDirection(moveDirection).magnitude);
            moveDirection *= speed;

            anim.SetFloat("direction", Input.GetAxis("Horizontal") * rotationSpeed, Hdumping, Time.deltaTime);
            anim.SetFloat("speed", Input.GetAxis("Vertical") * rotationSpeed, Vdumping, Time.deltaTime);

            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("Jumping");
                    anim.SetBool("jump", true);
                    moveDirection.y = jumpSpeed;
                }else
                    anim.SetBool("jump", false);
            }
           
        

            if (Input.GetKey(KeyCode.LeftShift)) {
                anim.SetBool("running", true);
            } else {
                anim.SetBool("running", false);
            }
        }
        controller.transform.RotateAround(transform.position,Vector3.up,Input.GetAxis("Horizontal"));
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
