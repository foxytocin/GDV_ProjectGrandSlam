using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRain : MonoBehaviour {

    ObjectPooler objectPooler;
    public GameObject KistenPartsPrefab;
    private CameraScroller cameraScroller;
    private LevelGenerator levelGenerator;
    //private ItemSpawner itemSpawner;
    private MapDestroyer mapDestroyer;
    public GameObject bomb_Prefab;
    private AudioManager audioManager;
    public AudioSource audioSource;
    public AudioClip bombRainSound1;
    public AudioClip bombRainSound2;
    public AudioClip bombRainSound3;
    //private float startVolume;
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
        audioSource = FindObjectOfType<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {

        if (Random.value > 0.98f && bombenregen == true)
        {


            Vector3Int bombPos = new Vector3Int((int)Random.Range(2f, 32f), 0, cameraScroller.rowPosition + (int)Random.Range(0f, 20f));


            BombRainSound();
            StartCoroutine(checkWorld(bombPos));
            
        }
    }



    private IEnumerator checkWorld(Vector3Int bombPos)
    {
        int x = bombPos.x;
        int z = bombPos.z;

        GameObject go;

                if (x > 0 && x < levelGenerator.levelBreite && z > 0 && z < levelGenerator.levelTiefe)
                {
                    if (levelGenerator.AllGameObjects[x, z] == null)
                    {
                        levelGenerator.AllGameObjects[x, z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 2, 3, false, bombraincolor);
                    }
                    if (levelGenerator.AllGameObjects[x, z] != null)
                    {
                        go = levelGenerator.AllGameObjects[x, z].gameObject;

                        switch (go.tag)
                        {
                            case "Kiste":
                                audioManager.playSound("destroyed_box");
                                go.SetActive(false);

                                //Ersetzt die Kiste durch Kiste_destroyed Prefab
                                Instantiate(KistenPartsPrefab, new Vector3(x, 0.5f, z), Quaternion.identity, transform);
                                levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 2, 3, false, bombraincolor);

                                break;

                            case "Item":
                                audioManager.playSound("break2");
                                go.SetActive(false);
                                levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 2, 3, false, bombraincolor);

                                break;

                            case "Player":
                                objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                                go.GetComponent<PlayerScript>().dead();
                                StartCoroutine(mapDestroyer.KillField(x, z));
                                levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 2, 3, false, bombraincolor);
                            break;

                            case "Wand":

                            break;

                            case "bombe":

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
