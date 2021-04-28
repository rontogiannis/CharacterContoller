using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 10f;

    public FixedJoystick joystick;

    Rigidbody rb;
    CharacterController cc;
    Animator anim;

    const float gravitationalConstant = 0.1f;

    float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        verticalSpeed = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = joystick.Horizontal;
        float verticalMovement = joystick.Vertical;

        bool isMoving = false;

        if(horizontalMovement != 0 || verticalMovement != 0)
        {
            isMoving = true;
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Camera.main.transform.forward;

        Vector3 direction = horizontalMovement * right + verticalMovement * forward;
        direction.y = 0;

        if (cc != null && !cc.isGrounded)
        {
            verticalSpeed -= gravitationalConstant * Time.deltaTime;

        }
        else
        {
            verticalSpeed = 0f;
        }

        if(cc == null)
            rb.velocity += direction * speed * Time.deltaTime;
        else
        {
            Vector3 targetVelocity = direction * speed * Time.deltaTime + new Vector3(0f, verticalSpeed, 0f);
            cc.Move(targetVelocity);
        }
            

        if(isMoving)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
