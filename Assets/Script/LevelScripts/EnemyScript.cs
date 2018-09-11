using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{


    public Vector3 target;
    private Vector3 lastTmpVector;
    private Vector3 tmpVectorPos;
    private Vector3 actualDirection;

    private Vector3[] dirs;

    private LevelGenerator levelGenerator;
    public GhostSpawnerScript ghostSpawner;
    private bool RichtungsAenderung; //true == z; false == x
    private float gravity;
    private Material playerMaterial;
    public Color32 playerColor;
    private Vector3 lastDirection;
    private AudioManager audioManager;
    public bool resultScreenActive;
    private RulesScript rulesScript;
    private GameManager gameManager;
    private EnemySpawner enemySpawner;
    private AudioSource playerAudio;
    private CameraScroller cameraScroller;
    private bool enemyWalking;

    float myTime;
    public float speed;

    void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        ghostSpawner = FindObjectOfType<GhostSpawnerScript>();
        audioManager = FindObjectOfType<AudioManager>();
        rulesScript = FindObjectOfType<RulesScript>();
        gameManager = FindObjectOfType<GameManager>();
        playerAudio = GetComponent<AudioSource>();
        cameraScroller = FindObjectOfType<CameraScroller>();
    }

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        enemySpawner = GetComponent<EnemySpawner>();
        playerMaterial = GetComponent<Renderer>().material;
        playerColor = playerMaterial.color;
        lastTmpVector = new Vector3(1f, 0f, 0f);
        lastDirection = new Vector3(1f, 0f, 0f);
        speed = Random.Range(1f, 4f);
        Time.timeScale = 1.0f;
        target = transform.position;
        levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = gameObject;
        myTime = 0f;
        gravity = 0f;
        transform.Rotate(0, 90, 0, Space.World);
        resultScreenActive = false;
        enemyWalking = true;

        dirs = new Vector3[4];
        dirs[0] = new Vector3(1f, 0f, 0f);
        dirs[1] = new Vector3(-1f, 0f, 0f);
        dirs[2] = new Vector3(0f, 0f, 1f);
        dirs[3] = new Vector3(0f, 0f, -1f);

        actualDirection = dirs[(int)Random.Range(0f, 4f)];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStatePlay)
        {

            if(transform.position != target)
            {
                levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = null;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            } else {
                
                levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = null;
                actualDirection = MoveEnemy(actualDirection);
                target += actualDirection;
                levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = gameObject;
            }
        //     //Debug.Log("tmp:" + tmp);
        //     //tmp = checkSingleDirection(tmp);
        //    if (tmp != Vector3.zero)
        //     { 
        //         //Im Array aktuelle position loeschen wenn das objekt auch wirklich ein Player ist 
        //         if (levelGenerator.AllGameObjects[(int)target.x, (int)target.z] != null && levelGenerator.AllGameObjects[(int)target.x, (int)target.z].gameObject.CompareTag("Enemy"))
        //             levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;

        //         //neue position berechenen
        //         target += tmp;

        //         //Player wird im Array auf der neuer Position 
        //         levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = this.gameObject;

        //         //speichern des benutzten Bewegungsvectors
        //         lastTmpVector = tmp;
        //     }

        //     //Objekt zum target Bewegung
        //     tmpVectorPos = transform.position;

        //     if (transform.position != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime)))
        //     {
        //         //movingSound(true);

        //         // Player Rollanimation
        //         if (tmpVectorPos.x != transform.position.x && RichtungsAenderung)
        //         {
        //             transform.Rotate(0, 90f, 0, Space.World);
        //             RichtungsAenderung = false;
        //         }
        //         else if (tmpVectorPos.z != transform.position.z && !RichtungsAenderung)
        //         {
        //             transform.Rotate(0, -90f, 0, Space.World);
        //             RichtungsAenderung = true;
        //         }

        //         if (tmpVectorPos.z < transform.position.z || tmpVectorPos.x < transform.position.x)
        //             transform.Rotate(8.5f, 0, 0);
        //         else
        //             transform.Rotate(-8.5f, 0, 0);
        //     }
        //     // else
        //     // {
        //     //     movingSound(false);
        //     // }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            other.GetComponent<PlayerScript>().dead();
        }
    }

    private Vector3 MoveEnemy(Vector3 tmp)
    {
        if (freeWay(tmp))
        {
            return tmp;
        }
        else {

            return getNewDirection(checkNeighbors(tmp));
        }
    }

    private Vector3 getNewDirection(List<Vector3> list)
    {
        if(list.Count !=  0)
        {
            int newDirIndex = (int)Random.Range(0f, list.Count);
            return list[newDirIndex];       
        } else {
            return Vector3.zero;
        }
         
    }

    private List<Vector3> checkNeighbors(Vector3 currentDirection)
    {
        List<Vector3> availablePaths = new List<Vector3>();

        availablePaths.Clear();

        for(int i = 0; i < 4; i++)
        {
            if (currentDirection != dirs[i])
            {
                if(freeWay(dirs[i]))
                {
                    availablePaths.Add(dirs[i]);
                }
            }
        }        
        return availablePaths;
    }

    private bool freeWay(Vector3 tmp)
    {
        int xPos = (int)(target.x + tmp.x);
        int zPos = (int)(target.z + tmp.z);

        //Prueft im Array an der naechsten stelle ob dort ein objekt liegt wenn nicht dann return.true
        if (levelGenerator.AllGameObjects[xPos, zPos] == null && zPos < cameraScroller.rowPosition + levelGenerator.tiefeLevelStartBasis)
        {
            // Zu 5% wechselt der Enemy seine Richtung zufällig
            if(Random.value >= 0.95f)
            {
                return false;
            }

            if (levelGenerator.SecondaryGameObjects1[xPos, zPos] != null)
            {
                if (levelGenerator.SecondaryGameObjects1[xPos, zPos].gameObject.CompareTag("KillField"))
                {
                    dead();
                }
            }
            return true;
        }
        else {

            if(levelGenerator.AllGameObjects[xPos, zPos] != null)
            {
                GameObject go = levelGenerator.AllGameObjects[xPos, zPos].gameObject;
                switch (go.tag)
                {
                    case "FreeFall":
                        playerFall();
                        return false;

                    case "Item":
                        levelGenerator.AllGameObjects[(int)go.transform.position.x, (int)go.transform.position.z] = null;
                        audioManager.playSound("break2");
                        go.SetActive(false);
                        return true;

                    case "Enemy":
                        //Debug.Log("Enemy hat sich selber gefunden, geht zurück!");
                        return true;

                    case "Player":
                        return true;
                        
                    default:
                        break;
                }
            }
            return false;
        }
    }

    public void playerFall()
    {
        enemyWalking = false;

        if (gameManager.gameStatePlay)
        {
            StartCoroutine(playerFallCore());
        }
    }

    private IEnumerator playerFallCore()
    {
        target.y = -200f;
        gravity = 0f;

        while (gameObject != null && transform.position.y > -50 && gameManager.gameStatePlay)
        {
            gravity += Time.deltaTime * 0.8f;
            transform.position = Vector3.MoveTowards(transform.position, target, gravity * gravity);
            yield return null;
        }

        gravity = 0f;
        if (gameObject != null)
        {
            levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
            Destroy(gameObject);
        }

    }

    // Tot trifft ein
    public void dead()
    {
        enemyWalking = false;
        levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
        Destroy(gameObject);
    }

    private bool soundNotPlaying = true;
    public float rollingSoundDelay = 0.2f;
    private void movingSound(bool moving)
    {
        if (moving && soundNotPlaying)
        {
            playerAudio.volume = 0.2f;
            soundNotPlaying = false;
            playerAudio.Play();

        }
        else if (!moving && !soundNotPlaying)
        {

            if (rollingSoundDelay > 0f)
            {
                rollingSoundDelay -= Time.deltaTime;

            }
            else
            {

                if (playerAudio.volume > 0f)
                {
                    playerAudio.volume -= Time.deltaTime;

                }
                else
                {
                    soundNotPlaying = true;
                    playerAudio.Pause();
                    rollingSoundDelay = 0.2f;
                    playerAudio.volume = 0.2f;
                }

            }

        }
    }
}
