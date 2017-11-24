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

    GameObject currentStation, currentDragable, station;

    public GameObject model;

    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    Animator anim;

    bool pulling = false;

    public bool IsDetectable
    {
        get { return isDetectable; }
        set { isDetectable = value; }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        station = GameObject.Find("Station");
        isDetectable = true;
    }

    void Update()
    {

        /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        anim.SetFloat("Speed", Mathf.Abs(vertical));
        */

        //if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        //{
        //    //start transition
        //}


        if (movementSpeed <= 4 && !canDrag)
        {
            movementSpeed = 6;
        }

        if (transform.position.y <= maxFallDistance)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            if (Input.GetKey(KeyCode.E) && canDrag && currentDragable != null)
            {
                float speed = 4.0f;
                float step = 2f;

                if (!pulling)
                {
                    anim.SetBool("isPulling", true);
                    pulling = true;
                }

                step = Time.deltaTime * speed;
                movementSpeed = 4.0f;
                currentDragable.transform.position = Vector3.MoveTowards(currentDragable.transform.position, transform.position, step); //Får objektet att följa efter spelaren sålänge E hålls in
            }

            if (Input.GetKey(KeyCode.Mouse0) && currentStation != null && !pulling)
            {
                isDetectable = false;

                if (currentStation.gameObject.GetComponent<ItemManager>().StationHealth > 0)
                {
                    currentStation.GetComponent<ItemManager>().StationHealth -= Time.deltaTime;
                    print(currentStation.GetComponent<ItemManager>().StationHealth);
                    anim.SetBool("isHammering", true);

                }
                if (currentStation.GetComponent<ItemManager>().StationHealth <= 0)
                {
                    station.GetComponent<ItemManager>().TriggerCollider.enabled = false;
                    currentStation = null;
                    IsDetectable = true;
                    anim.SetBool("isHammering", false);
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                anim.SetBool("isPulling", false);
                pulling = false;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
                anim.SetBool("isHammering", false);

            moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && !pulling)
            {
                anim.SetBool("isRunning", true);
            }
            else if (!pulling)
            {
                anim.SetBool("isRunning", false);
            }
            moveDirection = transform.TransformDirection(moveDirection);
            if (moveDirection != Vector3.zero)
            {
                model.transform.rotation = Quaternion.LookRotation(moveDirection);
            }
            //runAnim.SetBool("running", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {

                moveDirection *= movementSpeed + 4;
            }
            else
            {
                moveDirection *= movementSpeed;
            }
            if (Input.GetButton("Jump") && !pulling)
            {
                moveDirection.y = jumpHeight;

            }
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
            canDrag = true;
            currentDragable = other.gameObject;

        }
        if (other.tag == "Safezone")
        {

            if (other.gameObject.GetComponent<ItemManager>().StationHealth > 0)
            {
                currentStation = other.gameObject;
            }

        }
        if (other.tag == "Death Zone")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            canDrag = false;
            currentDragable = null;
            movementSpeed = 6;

            anim.SetBool("isPulling", false);

        }
        if (other.tag == "Safezone")
        {
            anim.SetBool("isHammering", false);
            currentStation = null;
            isDetectable = true;
        }
    }



}
