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
    public int bombPower;
    public bool alive;
    public bool remoteBombItem;
    public bool houdiniItem;
    public int travelDistance;
    public bool gameStatePlay;

    public AudioSource audioSource;
    public AudioClip GrabItem;
    public AudioClip Player1falls;
    public AudioClip Player2falls;
    public AudioClip Player3falls;
    public AudioClip Player4falls;

    private int travelDistanceStart;

    public List<GameObject> playerList;
    public bool creatingBomb;
    public Vector3 target;
    private Vector3 lastTmpVector;
    private Vector3 tmpVectorPos;
    private Vector3 tmp;
    float myTime;
    private LevelGenerator levelGenerator;
    private BombSpawner bombSpawner;
    private Houdini houdini;
    private RemoteBomb remoteBomb;
    public GhostSpawnerScript ghostSpawner;
    public CameraMovement cam;
    private bool RichtungsAenderung; //true == z; false == x 
    private bool fall = false;
    private float gravity;
    private Color32 playerColor;
    private Vector3 lastDirection;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        bombSpawner = FindObjectOfType<BombSpawner>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        remoteBomb = FindObjectOfType<RemoteBomb>();
        houdini = FindObjectOfType<Houdini>();
    }

    void Start()
    {
        gameStatePlay = false;
        playerColor = GetComponent<Renderer>().material.color;
        travelDistanceStart = (int)transform.position.z;
        travelDistance = 0;
        life = 3;
        avaibleBomb = 3;
        speed = 5f;
        bombTimer = 2f;
        bombPower = 1;
        alive = true;
        remoteBombItem = false;
        houdiniItem = false;
        creatingBomb = false;
        target = transform.position;
        myTime = 0f;
        gravity = 0f;
        levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = this.gameObject;
        cam.PlayerPosition(transform.position, playerID);
        transform.Rotate(0, 90, 0, Space.World);
    }

    void Update()
    {
        
        if(alive && gameStatePlay)
        {
            myTime += Time.deltaTime;

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
            if (freeWay(checkSingleDirection(tmp)))
            {
                //Im Array aktuelle position loeschen wenn das objekt auch wirklich ein Player ist 
                if (levelGenerator.AllGameObjects[(int)target.x, (int)target.z] != null && levelGenerator.AllGameObjects[(int)target.x, (int)target.z].gameObject.CompareTag("Player"))
                    levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;

                //neue position berechenen
                target += tmp;

                //Player wird im Array auf der neuer Position 
                levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = this.gameObject;

                //speichern des benutzten Bewegungsvectors
                lastTmpVector = tmp;
            }

            //Objekt zum target Bewegung
            if (transform.position.y > -1f)
            {
                tmpVectorPos = transform.position;

                calcTravelDistance();

                //Debug.Log("nicht Tot");
                if (transform.position != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime)) && alive)
                {
                    // Player Rollanimation
                    if (tmpVectorPos.x != transform.position.x && RichtungsAenderung)
                    {
                        transform.Rotate(0, 90f, 0, Space.World);
                        RichtungsAenderung = false;
                    }
                    else if (tmpVectorPos.z != transform.position.z && !RichtungsAenderung)
                    {
                        transform.Rotate(0, -90f, 0, Space.World);
                        RichtungsAenderung = true;
                    }

                    if (tmpVectorPos.z < transform.position.z || tmpVectorPos.x < transform.position.x)
                        transform.Rotate(8.5f, 0, 0);
                    else
                        transform.Rotate(-8.5f, 0, 0);

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
        }


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
    }


    //Funktion erlaubt das Druecken von 2 Richtungstasten zur gleichen Zeit
    //Die zuletzt gedrückte Taste bestimmt dann die aktuelle Richtung
    Vector3 checkSingleDirection(Vector3 tmp)
    {
        if(tmp != new Vector3(0f, 0f, 0f))
        {   
            //Ist das Produkt != 0 werden 2 Tasten gedruckt
            //Um zu bestimmen welche Taste zusätzlich gedrueckt wurde wird die aktuelle Richtung mit dem Produkt beider Tasten verglichen
            //Daraus kann errechnet werden welcher der neue Richtungvector ist
            if(tmp.x * tmp.z != 0)
            {
                //Bewegung nach Rechts und Hoch wird gedrueckt
                if(lastDirection.x == 1 && tmp.x * tmp.z == 1) {
                    this.tmp = new Vector3(0, 0, 1);
                    return this.tmp;
                }
                
                //Bewegung nach Rechts und Runter wird gedrueckt
                if(lastDirection.x == 1 && tmp.x * tmp.z == -1) {
                    this.tmp = new Vector3(0, 0, -1);
                    return this.tmp;
                }

                //Bewegung nach Linkt und Hoch wird gedrueckt
                if(lastDirection.x == -1 && tmp.x * tmp.z == -1) {
                    this.tmp = new Vector3(0, 0, 1);
                    return this.tmp;
                }

                //Bewegung nach Links und Runter wird gedrueckt
                if(lastDirection.x == -1 && tmp.x * tmp.z == 1) {
                    this.tmp = new Vector3(0, 0, -1);
                    return this.tmp;
                }

                //Bewegung nach Oben und Rechts wird gedrueckt
                if(lastDirection.z == 1 && tmp.x * tmp.z == 1) {
                    this.tmp = new Vector3(1, 0, 0);
                    return this.tmp;
                }

                //Bewegung nach Oben und Links wird gedrueckt
                if(lastDirection.z == 1 && tmp.x * tmp.z == -1) {
                    this.tmp = new Vector3(-1, 0, 0);
                    return this.tmp;
                }

                //Bewegung nach Unten und Rechts wird gedrueckt
                if(lastDirection.z == -1 && tmp.x * tmp.z == -1) {
                    this.tmp = new Vector3(1, 0, 0);
                    return this.tmp;
                }

                //Bewegung nach Unten und Links wird gedrueckt
                if(lastDirection.z == -1 && tmp.x * tmp.z == 1) {
                    this.tmp = new Vector3(-1, 0, 0);
                    return this.tmp;
                }
            } else {

                lastDirection = tmp;
            }
            return tmp;
        }
        return new Vector3(0, 0, 0);
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


    // bombPower
    public void setPower(int tmp)
    {
        bombPower += tmp;
    }

    public int getPower()
    {
        return bombPower;
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

    // remoteBomb
    public bool getRemoteBomb()
    {
        return remoteBombItem;
    }

    public void setRemoteBombe(bool tmp)
    {
        remoteBombItem = tmp;
    }

    // bombTimer
    public float getbombTimer()
    {
        return bombTimer;
    }

    public void setbombTimer(int tmp)
    {
        bombTimer = tmp;
    }

    // Weiteste zurueck gelegte Strecke wird gespeicher
    void calcTravelDistance() {
        if(transform.position.z > travelDistance + travelDistanceStart)
        {
            travelDistance = (int)transform.position.z - travelDistanceStart;
        }
    }

    // Setzt Bombe mit überprüfung von avaibleBomb und aLife
    void SetBomb()
    {
        creatingBomb = true;
        int bombXPos = Mathf.RoundToInt(transform.position.x);
        int bombZPos = Mathf.RoundToInt(transform.position.z);

        if(avaibleBomb > 0 && (levelGenerator.AllGameObjects[bombXPos, bombZPos] == null || levelGenerator.AllGameObjects[bombXPos, bombZPos].gameObject.CompareTag("Player")))
        {
            setAvaibleBomb(-1);
            levelGenerator.AllGameObjects[bombXPos, bombZPos] = bombSpawner.SpawnBomb(bombXPos, bombZPos, playerID, bombPower, bombTimer, remoteBombItem, playerColor);
        } else {
            creatingBomb = false;
        }
        creatingBomb = false;
    }

    bool freeWay(Vector3 tmp)
    {
        // Pruefen das keine Zwei Tasten für diagonales gehen gedrückt sind 
        // if (tmp == new Vector3(-1, 0, 0) || tmp == new Vector3(1, 0, 0) || tmp == new Vector3(0, 0, -1) || tmp == new Vector3(0, 0, 1))
        // {
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
                        go.GetComponent<PowerUp>().GrabItem(playerID);
                        levelGenerator.AllGameObjects[xPos, zPos] = null;
                        audioSource.PlayOneShot(GrabItem, 0.5f);
                    }
                    return false;
                }
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
}