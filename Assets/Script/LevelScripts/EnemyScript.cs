using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 target;
    private Vector3 actualDirection;
    private Vector3[] dirs;
    private LevelGenerator levelGenerator;
    public GhostSpawnerScript ghostSpawner;
    private float gravity;
    private AudioManager audioManager;
    private GameManager gameManager;
    private CameraScroller cameraScroller;
    private MenuDemoMode MenuDemoMode;
    public float speed;

    void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        ghostSpawner = FindObjectOfType<GhostSpawnerScript>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        cameraScroller = FindObjectOfType<CameraScroller>();
        MenuDemoMode = FindObjectOfType<MenuDemoMode>();
    }

    // Use this for initialization
    void Start()
    {
        // Stattet die Gegener mit zufaelliger Farbe und Geschwindigkeit aus
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.75f, 0.75f, 0.5f, 1f);
        speed = Random.Range(1f, 4f);
        target = transform.position;
        levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = gameObject;
        gravity = 0f;
        transform.Rotate(0, 90, 0, Space.World);

        // Definiert moegliche Richtungsvektoren in welche der Gegener sich bewegen kann
        dirs = new Vector3[4];
        dirs[0] = new Vector3(1f, 0f, 0f);
        dirs[1] = new Vector3(-1f, 0f, 0f);
        dirs[2] = new Vector3(0f, 0f, 1f);
        dirs[3] = new Vector3(0f, 0f, -1f);

        // Setzt zufaellig die erste Richtung in welches sich der Gegener bwegen soll
        actualDirection = dirs[(int)Random.Range(0f, 4f)];
    }

    void Update()
    {
        // Lauft nur wenn der das Spiel wirklich gespielt wird
        if (gameManager.gameStatePlay || MenuDemoMode.demoRunning)
        {
            if (transform.position != target)
            {
                //Im Array aktuelle position loeschen wenn das objekt auch wirklich ein Enemy ist 
                if (levelGenerator.AllGameObjects[(int)target.x, (int)target.z] != null &&
                    levelGenerator.AllGameObjects[(int)target.x, (int)target.z].gameObject.CompareTag("Enemy"))
                    levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;

                levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = gameObject;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            }
            else
            {

                // Prueft ob der Weg weiterhin frei ist / Ermittelt neue Richtung
                levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
                actualDirection = MoveEnemy(actualDirection);
                target += actualDirection;
                levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = gameObject;
            }
        }
    }

    // Kollidiert der Player mit einem Gegener wird er getoetet
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            other.GetComponent<PlayerScript>().dead();
        }
    }

    // Prueft ob der Weg weiterhin frei ist.
    // Falls nicht wird eine neue Richtung ermittelt
    private Vector3 MoveEnemy(Vector3 tmp)
    {
        if (freeWay(tmp))
        {
            return tmp;
        }
        else
        {
            return getNewDirection(checkNeighbors(tmp));
        }
    }

    // Aus den moeglichen freien Wegen wird zufaellig einer ausgewaehlt und als neuer Richtugnsvektor returned
    private Vector3 getNewDirection(List<Vector3> list)
    {
        if (list.Count != 0)
        {
            int newDirIndex = (int)Random.Range(0f, list.Count);
            return list[newDirIndex];
        }
        else
        {
            return Vector3.zero;
        }

    }

    // Ermittelt moegliche freie Wege wenn der Gegner gegen eine Hindernis gelaufen ist
    private List<Vector3> checkNeighbors(Vector3 currentDirection)
    {
        List<Vector3> availablePaths = new List<Vector3>();

        availablePaths.Clear();

        for (int i = 0; i < 4; i++)
        {
            if (currentDirection != dirs[i])
            {
                if (freeWay(dirs[i]))
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
            if (Random.value >= 0.95f)
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
        else
        {

            if (levelGenerator.AllGameObjects[xPos, zPos] != null)
            {
                GameObject go = levelGenerator.AllGameObjects[xPos, zPos].gameObject;
                switch (go.tag)
                {
                    case "FreeFall":
                        playerFall();
                        return true;

                    case "Item":
                        levelGenerator.AllGameObjects[(int)go.transform.position.x, (int)go.transform.position.z] = null;
                        audioManager.playSound("break2");
                        go.SetActive(false);
                        return true;

                    case "Enemy":
                        return false;

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
        levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
        Destroy(gameObject);
    }

}