using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    GameObject pausMenu;
    GameObject player;
    bool gameIsPaused;

    // Use this for initialization
    void Start()
    {

        pausMenu = GameObject.Find("PausMenu");
        player = GameObject.Find("Player");
        pausMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausMenu.SetActive(!pausMenu.activeSelf);
            if (pausMenu.activeInHierarchy == true)
            {
                Time.timeScale = 0f; //Fryser tiden
                gameIsPaused = true;
            }
            else if (pausMenu.activeInHierarchy == false)
            {
                Time.timeScale = 1f; //Ofryser tiden
                gameIsPaused = false;
            }
        }

    }

    public void UnPauseMenu()
    {
        pausMenu.SetActive(!pausMenu.activeSelf);
        Time.timeScale = 1f; //Ofryser tiden
    }

    public bool GameIsPaused
    {
        get { return gameIsPaused; }
    }

    public void Save()
    {
        IOScript.ioScript.playerPositionX = player.transform.position.x;
        IOScript.ioScript.playerPositionY = player.transform.position.y;
        IOScript.ioScript.playerPositionZ = player.transform.position.z;
    }

    public void Load()
    {
        IOScript.ioScript.Load();
        player.transform.position = new Vector3(IOScript.ioScript.playerPositionX, IOScript.ioScript.playerPositionY, IOScript.ioScript.playerPositionZ);

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
