using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public GameObject player;

    private int offsetY = 5;
    private int offsetX = 7;


    void LateUpdate()
    {
        transform.position = new Vector3(GameObject.Find("Caiza").transform.position.x + offsetX, GameObject.Find("Caiza").transform.position.y + offsetY, transform.position.z);
    }

}
