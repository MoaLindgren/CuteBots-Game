using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    float gravity;

    [SerializeField]
    float pushForce = 2.0f;

    [SerializeField]
    float climbSpeed;

    GameObject target;

    bool canClimb = false;
    bool canDrag = false;

    GameManager GM;
    CharacterController controller;

    private int maxFallDistance = -10;

    Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(movementSpeed <=4 && !canDrag)
        {
            movementSpeed = 6;
        }

        if (transform.position.y <= maxFallDistance)
        {
            SceneManager.LoadScene("Scene1");
        }

        if (canClimb)
        {
            if (Input.GetKey(KeyCode.W))
            {

                moveDirection = new Vector3(0, Input.GetAxis("Vertical"), 0);
                moveDirection *= climbSpeed;
            }

        }


        if (controller.isGrounded)
        {
            moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            moveDirection = transform.TransformDirection(moveDirection);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= movementSpeed + 4;
            }
            else
            {
                moveDirection *= movementSpeed;
            }
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpHeight;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {

        Rigidbody body = hit.collider.attachedRigidbody;
        Vector3 direction = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        if (body == null || body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDirection * pushForce;


    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Climbable")
        {
            canClimb = true;

        }
        if (other.tag == "Draggable")
        {
            float speed = 4.0f;
            float step = 2f;

            step = Time.deltaTime * speed;

            if (Input.GetKey(KeyCode.E))
            {
                movementSpeed = 4.0f;
                canDrag = true;
                other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, transform.position, step);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Climbable")
        {
            canClimb = false;
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        }
        if (other.tag == "Draggable")
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                canDrag = false;
                movementSpeed = 6;

            }
        }
    }



}
