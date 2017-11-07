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
    Transform spawnPosition;

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

    void OnTriggerEnter(Collider other)
    {
        //if (Input.GetKey(KeyCode.E))
        //{
        switch (other.tag)
        {
            case "Interactable":
                break;
            case "Climbable":
                break;
        }
        //}
    }
}
