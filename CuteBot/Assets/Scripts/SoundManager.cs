using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

                       //Ljudeffekter
    public AudioSource musicPlayer;

    
    public AudioClip menuMusic;
    public List <AudioClip> levelMusic;
    public List<AudioClip> mcSfx;
    public List<AudioClip> guardSfx;
    public List<AudioClip> kompisSfx;

    SoundManager SM;

    public GameObject menuCanvas;

    public Slider volumeSlider;
    public static float volume;
    [SerializeField] static SoundManager instance = null; //Scripts kan hämta funktioner från SoundManager

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;         //Använd för ljudeffekter?


    void OnEnable()
    {
      
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;


        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == ("MainMenu"))

        {
            musicPlayer.clip = menuMusic;
            menuCanvas.SetActive(true);
            
        }


        else if (sceneName == ("Scene1"))
        {

            musicPlayer.clip = levelMusic[1];
            menuCanvas.SetActive(false);
        }
        musicPlayer.Play();
        

    }

    void Start()
    {

        if (SM == null)

            SM = this;

        else if (SM != this)
            Destroy(gameObject);
        
      
        DontDestroyOnLoad(gameObject);
        
        musicPlayer.volume = volumeSlider.value;

   

       
       
    }

    /*
    public void sfxShuffle(params AudioClip[] clips)
    {
        int shuffleIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfx.pitch = randomPitch;                                            //Beep boop robot speak
        sfx.clip = clips[shuffleIndex];
        sfx.Play();
    }
    */

    public void VolumeSetting()
    {
        musicPlayer.volume = volumeSlider.value;
    }

 
}
