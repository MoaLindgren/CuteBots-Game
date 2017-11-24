using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    GameObject saveLoad;
    GameObject pausMenu;
    GameObject player;
    bool gameIsPaused;
    InputManager inputManager;

    // Use this for initialization
    void Start()
    {
        saveLoad = GameObject.Find("SaveManager");
        pausMenu = GameObject.Find("PausMenu");
        player = GameObject.Find("Player");
        pausMenu.SetActive(false);
        Time.timeScale = 1f;

        saveLoad.GetComponent<SaveLoad>().Load(player);
       
    }

    void Awake()
    {

        if (saveLoad == null)
        {
            saveLoad = GameObject.Find("SaveLoad");
        }
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


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
