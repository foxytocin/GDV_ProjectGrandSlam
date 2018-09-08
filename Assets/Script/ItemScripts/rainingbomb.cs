using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RainingBomb : MonoBehaviour {
    ObjectPooler objectPooler;
    public GameObject KistenPartsPrefab;
    private AudioManager audioManager;
    public AudioSource audioSource;
    public AudioClip bombRainSound1;
    public AudioClip bombRainSound2;
    public AudioClip bombRainSound3;
    private LevelGenerator levelGenerator;
    private MapDestroyer mapDestroyer;
    public float fallSpeed = 8.0f;
    public float spinSpeed = 250.0f;

    // Use this for initialization

    void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        audioSource = FindObjectOfType<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
        objectPooler = ObjectPooler.Instance;
        mapDestroyer = FindObjectOfType<MapDestroyer>();
    }

    // Update is called once per frame
    public IEnumerator BombFall(int x, int z) { 

            while (transform.position.y > 0.38)
            {
                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            }

            if (transform.position.y == 0.38)
            {

                    GameObject go;

                    if (levelGenerator.SecondaryGameObjects1[x, z] != null)
                    {
                        if (levelGenerator.SecondaryGameObjects1[x, z].gameObject.CompareTag("Boden") && levelGenerator.AllGameObjects[x, z] == null)
                        {

                        }

                        if (levelGenerator.AllGameObjects[x, z] != null)
                        {
                            go = levelGenerator.AllGameObjects[x, z].gameObject;

                            switch (go.tag)
                            {
                                case "Kiste":
                                    go.SetActive(false);
                                    levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = gameObject;

                                    Instantiate(KistenPartsPrefab, new Vector3(x, 0.5f, z), Quaternion.identity, transform);

                                    break;

                                case "Item":
                                    audioManager.playSound("break2");
                                    go.SetActive(false);
                                    levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = gameObject;

                                    break;

                                case "Player":
                                    objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                                    go.GetComponent<PlayerScript>().dead();
                                    StartCoroutine(mapDestroyer.KillField(x, z));
                                    levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = gameObject;
                                    break;

                                case "Bombe":
                                Destroy(gameObject);

                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    yield return null;
        


    
            }
    }
}
