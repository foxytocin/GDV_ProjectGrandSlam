using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // PlayerStats
    public int playerID;
    public int life;
    public int avaibleBomb;
    public float speed;
    public float bombTimer;
    public int range;
    public bool alive;
    public bool remoteBombItem;
    public bool houdiniItem;
    public int travelDistance;

    public AudioSource audioSource;
    public AudioClip Player1falls;
    public AudioClip Player2falls;
    public AudioClip Player3falls;
    public AudioClip Player4falls;

    private int travelDistanceStart;

    public List<GameObject> playerList;
    public bool creatingBomb;
    public Vector3 target;
    Vector3 lastTmpVector;
    float myTime;
    private LevelGenerator levelGenerator;
    private BombSpawner bombSpawner;
    private Houdini houdini;
    private RemoteBomb remoteBomb;
    public GhostSpawnerScript ghostSpawner;
    public CameraMovement cam;
    //private PowerUp powerUp;
    bool RichtungsAenderung; //true == z; false == x 
    bool fall = false;
    float gravity;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        bombSpawner = FindObjectOfType<BombSpawner>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        remoteBomb = FindObjectOfType<RemoteBomb>();
        houdini = FindObjectOfType<Houdini>();
        //powerUp = FindObjectOfType<PowerUp>();
    }

    void Start()
    {
        travelDistanceStart = (int)transform.position.z;
        travelDistance = 0;
        life = 3;
        avaibleBomb = 3;
        speed = 5f;
        bombTimer = 2f;
        range = 1;
        alive = true;
        remoteBombItem = false;
        houdiniItem = false;
        creatingBomb = false;
        target = transform.position;
        myTime = 0f;
        gravity = 0f;
        levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = playerList[playerID];
        cam.PlayerPosition(transform.position, playerID);
        transform.Rotate(0, 90, 0, Space.World);
    }

    void Update()
    {
        myTime += Time.deltaTime;
        Vector3 tmp = new Vector3();

        // Player Steuerung
        switch (playerID)
        {
            //Player 1
            case 0:

                tmp = InputManager.OneMainJoystick();

                if (InputManager.OneXButton() && !creatingBomb)
                    SetBomb();

                // RemoteBombe zünden Player_One
                if (InputManager.OneAButton())
                    remoteBomb.remoteBomb(0);

                //Pause aufrufen
                if (InputManager.OneStartButton())
                    return;
                
                break;

            //Player 2
            case 1:

                tmp = InputManager.TwoMainJoystick();

                if (InputManager.TwoXButton() && !creatingBomb)
                    SetBomb();

                // RemoteBombe zünden Player_Two
                if (InputManager.TwoAButton())
                    remoteBomb.remoteBomb(1);

                //Pause aufrufen
                if (InputManager.TwoStartButton())
                    return;

                break;

            //Player 3
            case 2:

                tmp = InputManager.ThreeMainJoystick();

                if (InputManager.ThreeXButton() && !creatingBomb)
                    SetBomb();

                // RemoteBombe zünden Player_Three
                if (InputManager.ThreeAButton())
                    remoteBomb.remoteBomb(2);

                //Pause aufrufen
                if (InputManager.ThreeStartButton())
                    return;

                break;

            //Player 4
            case 3:

                tmp = InputManager.FourMainJoystick();

                if (InputManager.FourXButton() && !creatingBomb)
                    SetBomb();

                // RemoteBombe zünden Player_Four
                if (InputManager.FourAButton())
                    remoteBomb.remoteBomb(3);

                //Pause aufrufen
                if (InputManager.FourStartButton())
                    return;

                break;

            //Player Default (Exception)
            default:
                Debug.Log("Playerfehler");
                break;
        }

        //Target bewegen
        if (freeWay(tmp) && alive)
        {
            //Im Array aktuelle position loeschen wenn das objekt auch wirklich ein Player ist 
            if (levelGenerator.AllGameObjects[(int)target.x, (int)target.z] != null && levelGenerator.AllGameObjects[(int)target.x, (int)target.z].gameObject.CompareTag("Player"))
                levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;

            //neue position berechenen
            target += tmp;

            //Player wird im Array auf der neuer Position 
            levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = playerList[this.playerID];

            //speichern des benutzten Bewegungsvectors
            lastTmpVector = tmp;
        }

        //Objekt zum target Bewegung
        if (transform.position.y > -1f)
        {
            Vector3 tmpVectorPos = transform.position;

            calcTravelDistance();

            //Debug.Log("nicht Tot");
            if (transform.position != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime)) && alive)
            {
                // Player Rollanimation
                if (tmpVectorPos.x != transform.position.x && RichtungsAenderung)
                {
                    transform.Rotate(0, 90, 0, Space.World);
                    RichtungsAenderung = false;
                }
                else if (tmpVectorPos.z != transform.position.z && !RichtungsAenderung)
                {
                    transform.Rotate(0, -90, 0, Space.World);
                    RichtungsAenderung = true;
                }

                if (tmpVectorPos.z < transform.position.z || tmpVectorPos.x < transform.position.x)
                    transform.Rotate(8, 0, 0);
                else if (tmpVectorPos.z > transform.position.z || tmpVectorPos.x > transform.position.x)
                    transform.Rotate(-8, 0, 0);

                    cam.PlayerPosition(transform.position, playerID);
            }
            else if (transform.position.y < 0.45f && !alive)
            {
                transform.position.Set(transform.position.x, -1, transform.position.z);
                cam.PlayerPosition(transform.position, playerID);
            }
        }

        // ROTATION DER BODENPLATTE ZUR ROTATION DES PLAYERS ADDIEREN, DAMIT DIESER WACKELT WENN ER AUF EINER WACKENDEN BODENPLATTE STEHT
        //transform.localEulerAngles = levelGenerator.SecondaryGameObjects1[(int)transform.position.x, (int)transform.position.z].gameObject.transform.localEulerAngles;

        if(fall)
        {
            gravity += Time.deltaTime * 0.8f;
            transform.position = Vector3.MoveTowards(transform.position, target, gravity * gravity);

            if (transform.position.y == -200)
            {
                gravity = 0f;
                setLife(-1);
                setALife(false);
                this.gameObject.SetActive(false);
            }
        }

        
        //Aktiviert EMISSION und LIGHT beim Player um anzuzugen das der Player das Houdini-Item hat
        // if(houdini)
        // {
        //     float emission = 0.2f + Mathf.PingPong(Time.time * 2f, 2f - 0.2f);
        //     Color baseColor = GetComponent<Renderer>().material.color;
        //     Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
        //     GetComponent<Renderer>().material.SetColor("_EmissionColor", finalColor);
        //     GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        //     GetComponent<Light>().enabled = true;
        //     GetComponent<Light>().intensity = emission;
        // } else {
        //     GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        //     GetComponent<Light>().enabled = false;
        // }
    }


    public void setWorld(LevelGenerator LevelGenerator)
    {
        this.levelGenerator = LevelGenerator;
    }

    // Uebergabe der PlayerID
    public void setPlayerID(int id)
    {
        playerID = id;
    }

    public int getPlayerID()
    {
        return playerID;
    }


    // Tot trifft ein
    public void dead()
    {
        Debug.Log("Player_" + playerID.ToString() + " is Dead");
        transform.Translate(0f, -2f, 0f);
        cam.PlayerPosition(transform.position, playerID);
        setLife(-1);
        setALife(false);
        this.gameObject.SetActive(false);

        ghostSpawner.GetComponent<GhostSpawnerScript>().createGhost(transform.position, playerID);
    }


    // PlayerList uebergabe
    public void setPlayerList(List<GameObject> playerList)
    {
        this.playerList = playerList;
    }

    public List<GameObject> getPlayerList()
    {
        return playerList;
    }


    // Speed
    public void setSpeed()
    {
        speed++;
    }

    public float getSpeed()
    {
        return speed;
    }


    // Range
    public void setRange(int tmp)
    {
        range += tmp;
    }

    public int getRange()
    {
        return range;
    }


    // avaibleBombs
    public void setAvaibleBomb(int wert)
    {
        avaibleBomb += wert;
    }

    public int getAvaibleBomb()
    {
        return avaibleBomb;
    }


    // Lifes
    public void setLife(int wert)
    {
        life += wert;
    }

    public int getLife()
    {
        return life;
    }


    // aLife
    public bool getALife()
    {
        return alive;
    }

    public void setALife(bool tmp)
    {
        alive = tmp;
    }

    //remoteBomb
    public bool getRemoteBomb()
    {
        return remoteBombItem;
    }

    public void setRemoteBombe(bool tmp)
    {
        remoteBombItem = tmp;
    }

    //bombTimer
    public float getbombTimer()
    {
        return bombTimer;
    }

    public void setbombTimer(int tmp)
    {
        bombTimer = tmp;
    }

    //Weiteste zurueck gelegte Strecke wird gespeicher
    void calcTravelDistance() {
        if(transform.position.z > travelDistance + travelDistanceStart)
        {
            travelDistance = (int)transform.position.z - travelDistanceStart;
        }
    }

    // Setzt Bombe mit überprüfung von avaibleBomb und aLife
    void SetBomb()
    {
        if (avaibleBomb > 0 && alive)
        {
            creatingBomb = true;
            bombSpawner.SpawnBomb(transform.position, playerID);

        } else
        {
            creatingBomb = false;
        }
    }

    bool freeWay(Vector3 tmp)
    {
        // Pruefen das keine Zwei Tasten für diagonales gehen gedrückt sind 
        if (tmp == new Vector3(-1, 0, 0) || tmp == new Vector3(1, 0, 0) || tmp == new Vector3(0, 0, -1) || tmp == new Vector3(0, 0, 1))
        {
            //entweder hat sich der Richungsvector nicht geändert oder das Objekt die selbe Position wie TargetVector
            if ((lastTmpVector == tmp || target == transform.position) && myTime > 0.2f)
            {
                int xPos = (int)(target.x + tmp.x);
                int zPos = (int)(target.z + tmp.z);

                //Prueft im Array an der naechsten stelle ob dort ein objekt liegt wenn nicht dann return.true
                if (levelGenerator.AllGameObjects[xPos, zPos] == null)
                {
                    myTime = 0f;

                    //Hat der Player das Houdini-Item, werden automatisch alle Kisten um ihn herum zerstört
                    if(houdiniItem)
                    {
                        houdini.callHoudini(xPos, zPos);
                    }

                    //Debug.Log("Player at: " +levelGenerator.SecondaryGameObjects1[(int)(target.x + tmp.x), (int)(target.z + tmp.z)].gameObject.tag);
                    if (levelGenerator.SecondaryGameObjects1[xPos, zPos].gameObject.CompareTag("KillField"))
                    {
                        dead();
                    }

                    return true;
                }
                else
                {
                    GameObject go = levelGenerator.AllGameObjects[xPos, zPos].gameObject;

                    if (go.CompareTag("FreeFall"))
                    {
                        playerFall();
                    }

                    //Item Kollision
                    if (go.CompareTag("Item"))
                    {
                        go.GetComponent<PowerUp>().PlayerItem(playerID);
                        levelGenerator.AllGameObjects[xPos, zPos] = null;
                        
                        //FindObjectOfType<PowerUp>().PlayerItem(playerID);
                        //Debug.Log("Item picked up");
                    }
                    return false;
                }
            }
            return false;
        }
        return false;
    }

    public void playerFall()
    {
        switch (playerID)
        {
            case 0: audioSource.PlayOneShot(Player1falls, 0.5f); break;
            case 1: audioSource.PlayOneShot(Player2falls, 0.5f); break;
            case 2: audioSource.PlayOneShot(Player3falls, 0.5f); break;
            case 3: audioSource.PlayOneShot(Player4falls, 0.5f); break;
            default: break;
        }

        target.y = -200f;
        fall = true;
        alive = false;
    }
    

    // public IEnumerator playerFall()
    // {
    //     gravity = 0f;

    //     switch (playerID)
    //     {
    //         case 0: audioSource.PlayOneShot(Player1falls, 0.5f); break;
    //         case 1: audioSource.PlayOneShot(Player2falls, 0.5f); break;
    //         case 2: audioSource.PlayOneShot(Player3falls, 0.5f); break;
    //         case 3: audioSource.PlayOneShot(Player4falls, 0.5f); break;
    //         default: break;
    //     }

    //     alive = false;
    //     target.y = -200f;

    //     while(transform.position.y > target.y)
    //     {
    //         gravity += Time.deltaTime * 0.8f;
    //         transform.position = Vector3.MoveTowards(transform.position, target, gravity * gravity);
    //         yield return null;
    //     }

    //     setLife(-1);
    //     setALife(false);
    //     this.gameObject.SetActive(false);
    // }
    
}