using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager2 : MonoBehaviour
{

    public AudioSource sfx;
    public AudioSource music;
    public Slider volumeSlider;
    public static float volume = 0;
    [SerializeField] static SoundManager2 instance = null; //Scripts kan hämta funktioner från SoundManager

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;         //Använd för ljudeffekter?


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }

    public void sfxShuffle(params AudioClip[] clips)
    {
        int shuffleIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfx.pitch = randomPitch;                                            //Beep boop robot speak
        sfx.clip = clips[shuffleIndex];
        sfx.Play();
    }

    public void OnValueChanged()
    {
        AudioListener.volume = 0.5f;
        DontDestroyOnLoad(this);
    }

    //Används för ljudmenyn för att sänka ljudet i scener, ska funka för hela spelet dock.........

    public void LoadGame()
    {
        SceneManager.LoadScene("q");

    }


}
