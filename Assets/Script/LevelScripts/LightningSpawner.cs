using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour {

    private CameraScroller cameraScroller;
    private DayNightSwitch dayNightSwitch;
    public GameObject lightning_prefab;
    public AudioSource audioSource;
    public AudioClip thunderStrike1;
    public AudioClip thunderStrike2;
    public AudioClip thunderStrike3;
    private bool thunderAndRainisPlaying;
    private float startVolume;

	// Use this for initialization
	void Awake()
    {
        cameraScroller = FindObjectOfType<CameraScroller>();
        dayNightSwitch = FindObjectOfType<DayNightSwitch>();
        audioSource = FindObjectOfType<AudioSource>();
        thunderAndRainisPlaying = false;
        startVolume = audioSource.volume;
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if(Random.value > 0.99f && !dayNightSwitch.isDay)
        {
            Instantiate(lightning_prefab, new Vector3(Random.Range(-5f, 35f), 0, cameraScroller.rowPosition + Random.Range(0f, 35f)), Quaternion.identity);
            thunderStrike();
        }

        if(!dayNightSwitch.isDay && !thunderAndRainisPlaying)
        {
            audioSource.volume = startVolume;
            audioSource.PlayDelayed(1f);
            thunderAndRainisPlaying = true;

        } else if(dayNightSwitch.isDay && thunderAndRainisPlaying) {

            thunderAndRainisPlaying = false;
            StartCoroutine(thunderFadeOut());
        }
    }

    private IEnumerator thunderFadeOut()
    {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 10f;
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;

        StopAllCoroutines();
    }

    void thunderStrike()
    {
        switch((int)Random.Range(0f, 4f))
        {
            case 1: audioSource.PlayOneShot(thunderStrike1, 0.6f); break;
            case 2: audioSource.PlayOneShot(thunderStrike1, 0.6f); break;
            case 3: audioSource.PlayOneShot(thunderStrike1, 0.6f); break;
        }
    }
}
