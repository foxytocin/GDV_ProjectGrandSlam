using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRain : MonoBehaviour
{

    ObjectPooler objectPooler;
    public GameObject KistenPartsPrefab;
    private CameraScroller cameraScroller;
    private LevelGenerator levelGenerator;
    private MapDestroyer mapDestroyer;
    private AudioManager audioManager;
    public AudioSource audioSource;
    public AudioClip bombRainSound1;
    public AudioClip bombRainSound2;
    public AudioClip bombRainSound3;
    public bool bombenregen = false;
    private BombSpawner bombSpawner;
    public Color32 bombraincolor;
    public int PlayerID;


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

        if (Random.value > 0.95f && bombenregen == true)
        {
            Vector3Int bombPos = new Vector3Int((int)Random.Range(2f, 32f), 0, cameraScroller.rowPosition + (int)Random.Range(0f, 15f));
            StartCoroutine(checkWorld(bombPos));

        }
    }


    private IEnumerator checkWorld(Vector3Int bombPos)
    {
        int x = bombPos.x;
        int z = bombPos.z;

        GameObject go;

        if (levelGenerator.SecondaryGameObjects1[x, z] != null)
        {
            if (levelGenerator.SecondaryGameObjects1[x, z].gameObject.CompareTag("Boden") && levelGenerator.AllGameObjects[x, z] == null)
            {
                levelGenerator.AllGameObjects[x, z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 1, 3, false, true, bombraincolor);
            }

            if (levelGenerator.AllGameObjects[x, z] != null)
            {
                go = levelGenerator.AllGameObjects[x, z].gameObject;

                switch (go.tag)
                {
                    case "Kiste":
                        go.SetActive(false);

                        //Ersetzt die Kiste durch Kiste_destroyed Prefab
                        Instantiate(KistenPartsPrefab, new Vector3(x, 0.5f, z), Quaternion.identity, transform);
                        levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 1, 3, false, true, bombraincolor);

                        break;

                    case "Item":
                        audioManager.playSound("break2");
                        go.SetActive(false);
                        levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 1, 3, false, true, bombraincolor);

                        break;

                    case "Player":
                        objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                        go.GetComponent<PlayerScript>().dead();
                        StartCoroutine(mapDestroyer.KillField(x, z));
                        levelGenerator.AllGameObjects[bombPos.x, bombPos.z] = bombSpawner.SpawnBomb(bombPos.x, bombPos.z, 5, 1, 3, false, true, bombraincolor);
                        break;

                    case "Wand":

                        break;

                    case "Bombe":

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
        switch ((int)Random.Range(0f, 3f))
        {
            case 0: audioSource.PlayOneShot(bombRainSound1, 0.5f * audioManager.settingsFXVolume); break;
            case 1: audioSource.PlayOneShot(bombRainSound2, 0.5f * audioManager.settingsFXVolume); break;
            case 2: audioSource.PlayOneShot(bombRainSound3, 0.5f * audioManager.settingsFXVolume); break;
        }
    }
}
