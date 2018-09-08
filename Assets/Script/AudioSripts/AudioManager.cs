using UnityEngine.Audio;
using System.Collections;
using Random=UnityEngine.Random;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Music[] music;

    public float settingsFXVolume;
    public float settingsMusicVolume;
    public bool musicAtStart;
    public int randomInGameMusic;


    // Use this for initialization
    void Awake ()
    {
	    foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Music m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;

            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }

        settingsMusicVolume = 0.5f;
        settingsFXVolume = 1f;
    }

    private void Start()
    {
        if(!musicAtStart)
            setMusicVolume(0);

        playSound("menumusic");
    }

    public void playNextSong(){
        music[randomInGameMusic].source.Stop();
        randomInGameMusic = Random.Range(0, music.Length);
        music[randomInGameMusic].source.Play();
        //AudioClip song = music[randomInGameMusic].clip;
        //Invoke("PlayNextSong", music[randomInGameMusic].source.Length;
    }    

    public void stopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void playSound(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
       s.source.Play();
    }

    public void startSetFXVolume(float settingsVolume)
    {
        StartCoroutine(setFXVolume(settingsVolume));
    }
    public IEnumerator setFXVolume(float settingsVolume)
    {
        bool waitingB = true;
        float waiting = 0f;

        settingsFXVolume = settingsVolume;

        foreach (Sound s in sounds)
        {
            if (s.name != "menumusic")
                s.source.volume = s.groundVolume * settingsVolume;

        }
        
        playSound("explosion_medium");

        while (waitingB)
        {
            waiting *= Time.deltaTime + 0.2f;

            if (waiting > 6f)
                waitingB = false;
            yield return null;
        }

        stopSound("explosion_medium");
    }

    public void setMusicVolume(float settingsVolume)
    {
        settingsMusicVolume = settingsVolume;
        Sound s = Array.Find(sounds, sound => sound.name == "menumusic");
        s.source.volume = s.groundVolume * settingsVolume;

        foreach (Music m in music)
        {
            m.source.volume = m.groundVolume * settingsVolume;
        }

    }

    public void stopMenuMusic()
    {

        StartCoroutine(stopMenuMusicCore()); 

    }

    public IEnumerator stopMenuMusicCore()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "menumusic");
        bool terminus = true;

        while (terminus)
        {
            if (s.source.volume > 0)
            {
                s.source.volume -= 0.2f * (Time.deltaTime + 0.1f);
            }
            else
            {
                //Debug.LogWarning("Hier und nicht weiter");
                s.source.Stop();
                s.source.volume = s.groundVolume * settingsMusicVolume;
                terminus = false;
            }
            yield return null;

        }

    }

    public void startInGameMusic()
    {
        StartCoroutine(startInGameMusicCore());
    }

    public IEnumerator startInGameMusicCore()
    {
        music[randomInGameMusic].source.pitch = 1.0f;
        music[randomInGameMusic].source.volume = 0;
        music[randomInGameMusic].source.Play();

        while(music[randomInGameMusic].source.volume < music[randomInGameMusic].groundVolume * settingsMusicVolume)
        {
            music[randomInGameMusic].source.volume += 0.1f * (Time.deltaTime + 0.1f);
            yield return null;
        }
    }

    public void stopInGameMusic()
    {

        StartCoroutine(stopInGameMusicCore());

    }

    public IEnumerator stopInGameMusicCore()
    {
        bool terminus = true;
        while (terminus)
        {
            if (music[randomInGameMusic].source.volume > 0)
            {
                music[randomInGameMusic].source.volume -= 0.1f * (Time.deltaTime + 0.1f);
            }
            else
            {
                music[randomInGameMusic].source.Stop();
                music[randomInGameMusic].source.volume = music[randomInGameMusic].groundVolume * settingsMusicVolume;
                terminus = false;
            }

            yield return null;

        }

    }


    // Pitched die Musik runter wenn das Spiel auf Pause gestellt wird
    // + 0.1f bei Time.deltaTime da die Zeit beim Pausieren auf 0 gestellt wird und es Null-Multiplikation kommen wuerde
    public IEnumerator pitchDown()
    {
        StopCoroutine(pitchUp());
        while(music[randomInGameMusic].source.pitch > 0.3f)
        {
            music[randomInGameMusic].source.pitch -= 0.3f * (Time.deltaTime + 0.1f);
            //Debug.Log(music[randomInGameMusic].source.pitch);
            yield return null;
        }

        music[randomInGameMusic].source.Pause();
    }

    // Pitched die Musik wieder auf Normalgeschwindigkeit wenn das Spiel resumed wird
    public IEnumerator pitchUp()
    {
        StopCoroutine(pitchDown());
        music[randomInGameMusic].source.pitch = 0.3f;
        music[randomInGameMusic].source.volume = music[randomInGameMusic].groundVolume * settingsMusicVolume;
        music[randomInGameMusic].source.Play();

        while(music[randomInGameMusic].source.pitch < 1.0f)
        {
            music[randomInGameMusic].source.pitch += Time.deltaTime;
            //Debug.Log(music[randomInGameMusic].source.pitch);
            yield return null;
        }

        music[randomInGameMusic].source.pitch = 1.0f;
    }
}
