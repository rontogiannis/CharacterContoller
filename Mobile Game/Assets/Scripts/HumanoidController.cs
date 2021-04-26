using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 10f;

    public FixedJoystick joystick;

    Rigidbody rb;
    Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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

        rb.velocity += direction * speed * Time.deltaTime;

        if(isMoving)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }
}

/*
void Update()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
    movementDirection.Normalize();
    transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

    if (movementDirection != Vector3.zero)
    {
        
    }
}
*/
