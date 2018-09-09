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
    public float fallSpeed = 5.0f;
    float randomSpeed;

    float rotationY;
    float gravity;
    Vector3 temp;


    


// Use this for initialization

void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        audioSource = FindObjectOfType<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
        objectPooler = ObjectPooler.Instance;
        mapDestroyer = FindObjectOfType<MapDestroyer>();
            randomSpeed = 0.3f;
            temp = new Vector3(transform.position.x, 0.38f, transform.position.z);

            gravity = 0;
        
    }


// Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0.38)
        {
            gravity += Time.deltaTime * 0.5f;
            transform.Translate(0, -((gravity * gravity) + randomSpeed), 0);
        }

        if (transform.position.y < 0.37)
        {
            int x = (int)transform.position.x;
            int z = (int)transform.position.z;

            


                if (levelGenerator.AllGameObjects[x, z] != null)
                        {
                            GameObject go;
                            go = levelGenerator.AllGameObjects[x, z].gameObject;

                            switch (go.tag)
                            {
                                case "Bombe":
                                    transform.position = temp;
                                    levelGenerator.AllGameObjects[x, z] = null;
                                    Destroy(gameObject);
                                    break;
                                    
                                case "Kiste":
                                    go.SetActive(false);
                                    transform.position = temp;
                                    levelGenerator.AllGameObjects[x, z] = gameObject;
                                    Instantiate(KistenPartsPrefab, new Vector3(x, 0.5f, z), Quaternion.identity, transform);
                                    break;

                                case "Item":
                                    audioManager.playSound("break2");
                                    go.SetActive(false);
                                    transform.position = temp;
                                    levelGenerator.AllGameObjects[x, z] = gameObject;
                                    break;

                                case "Player":
                                    objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                                    go.GetComponent<PlayerScript>().dead();
                                    StartCoroutine(mapDestroyer.KillField(x, z));
                                    transform.position = temp;
                                    levelGenerator.AllGameObjects[x, z] = gameObject;
                                    break;


                                default:
                                    break;
                            }
                }
                    

            



        }
        if (transform.position.y == 0.38)
        {
            int x = (int)transform.position.x;
            int z = (int)transform.position.z;




            if (levelGenerator.AllGameObjects[x, z] != null)
            {
                GameObject go;
                go = levelGenerator.AllGameObjects[x, z].gameObject;
            }
    }
}
