using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIBehaviour : MonoBehaviour
{
    GameObject player;
    GameObject GM;
    bool detectedPlayer;
    float movement;
    float speed = 5.0f;
    float offsetX = 4.0f;
    public Transform[] nodes;
    int destinationPoints = 0;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GM = GameObject.Find("GameManager");
        player = GameObject.Find("Player");

    }

    void GotoNextPoint()
    {
        if (nodes.Length == 0)
        {
            return;
        }

        agent.destination = nodes[destinationPoints].position;

        destinationPoints = (destinationPoints + 1) % nodes.Length;

    }


    void OnTriggerEnter(Collider other)
    {
        movement = Time.deltaTime * speed;

        if (other.gameObject.tag == "Player")
        {
            detectedPlayer = true;
        }


    }

    void Update()
    {

        if (detectedPlayer && player.GetComponent<PlayerManager>().IsDetectable && !GM.GetComponent<InputManager>().GameIsPaused)
        {
            Vector3 relativePosition = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = rotation;

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movement);

        }

        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

    }


    void OnTriggerExit()
    {
        detectedPlayer = false;
        movement = Time.deltaTime / speed;
        //Gå tillbaka till navMesh positionen
    }
}
