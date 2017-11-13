using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    bool canClimb = false;
    bool canDrag = false;
    public bool isDetectable;

    private int maxFallDistance = -10;

    [SerializeField]
    float climbSpeed;

    [SerializeField]
    float gravity;

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float pushForce = 2.0f;

    GameManager GM;

    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    public bool IsDetectable
    {
        get { return isDetectable; }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        isDetectable = true;
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

        // Om vi kan klättra rör vi oss uppåt.
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = new Vector3(0, Input.GetAxis("Vertical"), 0);
                moveDirection *= climbSpeed;
            }

        }

        //Rörelsekontroller för spelaren
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

    //Så spelaren kan flytta object genom att putta dem.
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

    void OnTriggerEnter(Collider other) //När spelaren går in i collidern
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
                other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, transform.position, step); //Får objektet att följa efter spelaren sålänge E hålls in
            }
        }
    }

    void OnTriggerExit(Collider other) //När spelaren lämnar collidern återställs tidigare värden
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
