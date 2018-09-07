using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour {

    public GameObject KistenPartsPrefab;
    private CameraScroller cameraScroller;
    private LevelGenerator levelGenerator;
    private ItemSpawner itemSpawner;
    private DayNightSwitch dayNightSwitch;
    public GameObject lightning_prefab;
    private AudioManager audioManager;
    public AudioSource audioSource;
    public AudioClip thunderStrike1;
    public AudioClip thunderStrike2;
    public AudioClip thunderStrike3;
    private bool thunderAndRainisPlaying;
    private float startVolume;
    public Vector3Int thunderPos;

	// Use this for initialization
	void Awake()
    {
        cameraScroller = FindObjectOfType<CameraScroller>();
        dayNightSwitch = FindObjectOfType<DayNightSwitch>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        itemSpawner = FindObjectOfType<ItemSpawner>();
        audioSource = FindObjectOfType<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
        thunderAndRainisPlaying = false;
        startVolume = audioSource.volume;
    }
	
	void FixedUpdate()
    {
        if(Random.value > 0.99f && !dayNightSwitch.isDay)
        {

            thunderPos = new Vector3Int((int)Random.Range(-5f, 35f), 0, cameraScroller.rowPosition + (int)Random.Range(0f, 35f));

            Instantiate(lightning_prefab, thunderPos, Quaternion.identity);
            thunderStrikeSound();
            StartCoroutine(checkWorld(thunderPos));
        }

        if(!dayNightSwitch.isDay && !thunderAndRainisPlaying)
        {
            audioSource.volume = startVolume * audioManager.settingsFXVolume;
            audioSource.PlayDelayed(1f);
            thunderAndRainisPlaying = true;

        } else if(dayNightSwitch.isDay && thunderAndRainisPlaying) {

            thunderAndRainisPlaying = false;
            StartCoroutine(thunderFadeOut());
        }
    }


    private IEnumerator checkWorld(Vector3Int thunderPos)
    {
        int radius = 2;

        for(int z = -radius; z <= radius; z++)
        {
            for(int x = -radius; x <= radius; x++)
            {
                if(thunderPos.x + x > 0 && thunderPos.x + x < levelGenerator.levelBreite  && thunderPos.z + z > 0 && thunderPos.z + z < levelGenerator.levelTiefe)
                {
                    if(levelGenerator.AllGameObjects[thunderPos.x + x, thunderPos.z + z] != null)
                    {
                        GameObject go = levelGenerator.AllGameObjects[thunderPos.x + x, thunderPos.z + z].gameObject;

                        switch(go.tag)
                        {
                            case "Kiste" :
                                levelGenerator.AllGameObjects[thunderPos.x + x, thunderPos.z + z] = null;
                                go.SetActive(false);

                                //Ersetzt die Kiste durch Kiste_destroyed Prefab
                                Instantiate(KistenPartsPrefab, new Vector3(thunderPos.x + x, 0.5f, thunderPos.z + z), Quaternion.identity, transform);

                                //Spawnt Item
                                if(Random.value > 0.7f)
                                    itemSpawner.SpawnItem(thunderPos.x + x, thunderPos.z + z);
                                break;

                            case "Item" :
                                levelGenerator.AllGameObjects[thunderPos.x + x, thunderPos.z + z] = null;
                                audioManager.playSound("break2");
                                go.SetActive(false);
                                break;

                            default :
                                break;
                        }
                    }
                }
            }
        }
        yield return null;    
    }

    private IEnumerator thunderFadeOut()
    {
        float startVolume = audioSource.volume * audioManager.settingsFXVolume;
 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 10f;
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }

    void thunderStrikeSound()
    {
        switch((int)Random.Range(0f, 3f))
        {
            case 0: audioSource.PlayOneShot(thunderStrike1, 0.4f * audioManager.settingsFXVolume); break;
            case 1: audioSource.PlayOneShot(thunderStrike2, 0.6f * audioManager.settingsFXVolume); break;
            case 2: audioSource.PlayOneShot(thunderStrike3, 0.5f * audioManager.settingsFXVolume); break;
        }
    }
}
