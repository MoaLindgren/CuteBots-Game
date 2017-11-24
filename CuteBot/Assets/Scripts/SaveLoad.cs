using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    GameObject player;
    SaveLoad saveLoad;
    bool sceneIsLoaded;

    public void Save()
    {
        player = GameObject.Find("Player");
        IOScript.ioScript.playerPositionX = player.transform.position.x;
        IOScript.ioScript.playerPositionY = player.transform.position.y;
        IOScript.ioScript.playerPositionZ = player.transform.position.z;
    }

    public void Load(GameObject player)
    {
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
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

    }

    public bool SceneIsLoaded
    {
        get
        {
            return sceneIsLoaded;
        }
        set
        {
            sceneIsLoaded = value;
        }
    }
}
