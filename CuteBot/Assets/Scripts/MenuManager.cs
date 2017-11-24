using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    GameObject saveLoad;

    void Start()
    {
        saveLoad = GameObject.Find("SaveLoad");
    }

    void Awake()
    {
        if (saveLoad == null)
        {
            saveLoad = GameObject.Find("SaveLoad");
        }

    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void Enlarge(Text text)
    {
        if (text.fontSize == 30)
        {
            text.fontSize = 40;
        }
        else
        {
            text.fontSize = 30;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();

    }

    public void OnValueChanged()
    {
        AudioListener.volume = 0.5f;
    }
}
