using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRain : MonoBehaviour {

    ObjectPooler objectPooler;
    public GameObject KistenPartsPrefab;
    private CameraScroller cameraScroller;
    private LevelGenerator levelGenerator;
    private ItemSpawner itemSpawner;
    private MapDestroyer mapDestroyer;
    public GameObject bomb_Prefab;
    private AudioManager audioManager;
    public AudioSource audioSource;
    public AudioClip bombRainSound1;
    public AudioClip bombRainSound2;
    public AudioClip bombRainSound3;
    private float startVolume;
    public bool bombenregen = false;
    private BombSpawner bombSpawner;
    private Color32 bombraincolor;


    void Awake()
    {
        bombraincolor = new Color32(0, 0, 0, 0);
        bombSpawner = FindObjectOfType<BombSpawner>();
        objectPooler = ObjectPooler.Instance;
        cameraScroller = FindObjectOfType<CameraScroller>();
        mapDestroyer = FindObjectOfType<MapDestroyer>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        itemSpawner = FindObjectOfType<ItemSpawner>();
        audioSource = FindObjectOfType<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
        startVolume = audioSource.volume;
    }

    void FixedUpdate()
    {

        if (Random.value > 0.5f && bombenregen == true)
        {


            Vector3Int bombPos = new Vector3Int((int)Random.Range(0f, 10f), 0, cameraScroller.rowPosition + (int)Random.Range(0f, 10f));

            BombRainSound();
            StartCoroutine(checkWorld(bombPos));
            levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 1000, 1, -1, false, bombraincolor);
        }
    }



    private IEnumerator checkWorld(Vector3Int bombPos)
    {
        int x = bombPos.x;
        int z = bombPos.z;

        GameObject go;

                if (x > 0 && x < levelGenerator.levelBreite && z > 0 && z < levelGenerator.levelTiefe)
                {
                    if (levelGenerator.AllGameObjects[x, z] != null)
                    {
                        go = levelGenerator.AllGameObjects[x, z].gameObject;

                        switch (go.tag)
                        {
                            case "Kiste":
                                audioManager.playSound("destroyed_box");
                                levelGenerator.AllGameObjects[x, z] = null;
                                go.SetActive(false);

                                //Ersetzt die Kiste durch Kiste_destroyed Prefab
                                Instantiate(KistenPartsPrefab, new Vector3(x, 0.5f, z), Quaternion.identity, transform);

                                break;

                            case "Item":
                                audioManager.playSound("break2");
                                levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = null;
                                go.SetActive(false);
                                
                                break;

                            case "Player":
                                objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                                go.GetComponent<PlayerScript>().dead();
                                levelGenerator.AllGameObjects[x, z] = null;
                                StartCoroutine(mapDestroyer.KillField(x, z));
                                break;

                            default:
                                break;
                        }
                    }
                }
        yield return null;
    }


    void BombRainSound()
    {
        switch ((int)Random.Range(0f, 4f))
        {
            case 1: audioSource.PlayOneShot(bombRainSound1, 0.5f * audioManager.settingsFXVolume); break;
            case 2: audioSource.PlayOneShot(bombRainSound2, 0.5f * audioManager.settingsFXVolume); break;
            case 3: audioSource.PlayOneShot(bombRainSound3, 0.5f * audioManager.settingsFXVolume); break;
        }
    }
}
