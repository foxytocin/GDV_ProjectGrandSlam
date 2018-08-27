using UnityEngine.Audio;
using System.Collections;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public float settingsFXVolume;

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
	}

    private void Start()
    {
        playSound("Music");
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
            if (s.name != "Music")
                s.source.volume = s.groundVolume * settingsVolume;

            Debug.Log(s.name + ":" + s.volume);
        }
        
        playSound("woosh_2");
        Debug.Log("played Sound");
        while (waitingB)
        {
            waiting *= Time.deltaTime + 0.2f;

            if (waiting > 6f)
                waitingB = false;
            yield return null;
        }
        Debug.Log("Stopped Sound");
        stopSound("woosh_2");
    }

    public void setMusicVolume(float settingsVolume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Music");
        s.source.volume = s.groundVolume * settingsVolume;

    }
}
