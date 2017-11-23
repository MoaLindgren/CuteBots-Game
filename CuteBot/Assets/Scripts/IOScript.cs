using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class IOScript : MonoBehaviour
{
    public static IOScript ioScript;

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    void Awake()
    {
        if (ioScript == null)
        {
            DontDestroyOnLoad(this.gameObject);
            ioScript = this;
        }
        else if (ioScript != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData playerData = new PlayerData();
        playerData.playerPositionX = playerPositionX;
        playerData.playerPositionY = playerPositionY;
        playerData.playerPositionZ = playerPositionZ;

        bf.Serialize(file, playerData);
        file.Close();

    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            playerPositionX = playerData.playerPositionX;
            playerPositionY = playerData.playerPositionY;
            playerPositionZ = playerData.playerPositionZ;



        }
    }

    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }

    }
}

    class PlayerData
    {
        public float playerPositionX;
        public float playerPositionY;
        public float playerPositionZ;
    }
