using UnityEngine.Audio;
using System.Collections;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Music[] music;


    public float settingsFXVolume;
    public float settingsMusicVolume;

    public bool musicAtStart;

    int randomInGameMusic;

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

        randomInGameMusic = 0;
}

    private void Start()
    {
        if(!musicAtStart)
            setMusicVolume(0);

        playSound("menumusic");
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
                s.source.volume -= 0.2f * (Time.deltaTime + 0.3f);
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
        music[randomInGameMusic].source.volume = 0;
        music[randomInGameMusic].source.Play();

        while(music[randomInGameMusic].source.volume < music[randomInGameMusic].groundVolume * settingsMusicVolume)
        {
            music[randomInGameMusic].source.volume += 0.2f * (Time.deltaTime + 0.3f);
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
                music[randomInGameMusic].source.volume -= 0.2f * (Time.deltaTime + 0.3f);
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
}
