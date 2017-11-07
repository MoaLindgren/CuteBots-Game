using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
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
    Transform spawnPosition;

    [SerializeField]
    float climbSpeed;

    GameObject target;

    bool canClimb = false;

    GameManager GM;
    ItemManager IM;

    private int maxFallDistance = -10;

    Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        IM = new ItemManager();
    }

    void Update()
    {

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

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
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
    }


    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Climbable")
        {
            canClimb = false;
            
        }
    }


}
