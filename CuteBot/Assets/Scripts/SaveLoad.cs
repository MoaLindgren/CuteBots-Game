using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{

    GameObject player;
    SaveLoad saveLoad;

    void Start()
    {
        player = GameObject.Find("Player");

    }

    public void Save()
    {
        player = GameObject.Find("Player");
        IOScript.ioScript.playerPositionX = player.transform.position.x;
        IOScript.ioScript.playerPositionY = player.transform.position.y;
        IOScript.ioScript.playerPositionZ = player.transform.position.z;
        IOScript.ioScript.currentScene = SceneManager.GetActiveScene();
        print(IOScript.ioScript.currentScene.name);
    }

    public void Load()
    {
        print(IOScript.ioScript.currentScene.name);
        SceneManager.LoadScene(IOScript.ioScript.currentScene.name);
        IOScript.ioScript.Load();
        player.transform.position = new Vector3(IOScript.ioScript.playerPositionX, IOScript.ioScript.playerPositionY, IOScript.ioScript.playerPositionZ);

    }
    void Awake()
    {
        if (saveLoad == null)
        {
            DontDestroyOnLoad(this.gameObject);
            saveLoad = this;
        }
        else if (saveLoad != this)
        {
            Destroy(gameObject);
        }
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

    }
}
