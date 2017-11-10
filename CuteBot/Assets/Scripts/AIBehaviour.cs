using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    public Transform target;
    public GameObject player;
    bool detectedPlayer;
    float movement;
    float speed = 2.0f;
    Transform[] navigation;
    NavMeshAgent agent;

    void Start()
    {
        navigation = new Transform[2];
        agent = GetComponent<NavMeshAgent>();
    }


    void OnTriggerEnter(Collider range)
    {
        movement = Time.deltaTime * speed;

        if (range.gameObject.tag == "Player")
        {
            detectedPlayer = true;
        }

    }

    void Update()
    {

        if (detectedPlayer)
        {
            Vector3 relativePosition = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = rotation;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movement);

        }
    }

    void OnTriggerExit()
    {
        movement = Time.deltaTime / speed;
        transform.position = transform.position; //Gå tillbaka till navMesh positionen
    }
}
