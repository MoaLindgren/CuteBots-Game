using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{

    //public GameObject player;

    private int offsetY = 7;
    private int offsetX = 5;


    void LateUpdate() //Följer efter spelaren
    {
            transform.position = new Vector3(GameObject.Find("TM8Test").transform.position.x + offsetX, GameObject.Find("TM8Test").transform.position.y + offsetY, transform.position.z);
       
    }



}
