using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // PlayerStats
    public int playerID;
    public int avaibleBomb;
    public float speed;
    public float bombTimer;
    public int bombPower;
    public bool remoteBombItem;
    public bool houdiniItem;
    public bool creatingBomb;
    public Vector3Int target;
    private Vector3 lastTmpVector;
    private Vector3 tmpVectorPos;
    private Vector3Int nullVector;
    private Vector3Int tmp;
    private Vector3Int lastDirection;
    float myTime;
    private LevelGenerator levelGenerator;
    private BombSpawner bombSpawner;
    private Houdini houdini;
    private RemoteBomb remoteBomb;
    public GhostSpawnerScript ghostSpawner;
    private bool RichtungsAenderung; //true == z; false == x
    private float gravity;
    private Material playerMaterial;
    public Color32 playerColor;
    private AudioManager audioManager;
    public bool resultScreenActive;
    public float remoteBombTimer;
    public float houdiniTimer;
    private RulesScript rulesScript;
    private GameManager gameManager;

    void Awake()
    {
        bombSpawner = FindObjectOfType<BombSpawner>();
        remoteBombTimer = 0f;
        houdiniTimer = 0f;
        levelGenerator = FindObjectOfType<LevelGenerator>();
        remoteBomb = FindObjectOfType<RemoteBomb>();
        houdini = FindObjectOfType<Houdini>();
        ghostSpawner = FindObjectOfType<GhostSpawnerScript>();
        audioManager = FindObjectOfType<AudioManager>();
        rulesScript = FindObjectOfType<RulesScript>();
        gameManager = FindObjectOfType<GameManager>();
        nullVector = new Vector3Int(0, 0, 0);
    }

    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
        playerColor = playerMaterial.color;
        avaibleBomb = 3;
        lastTmpVector = new Vector3Int(1, 0, 0);
        speed = 5.5f;
        bombTimer = 2f;
        Time.timeScale = 1.0f;
        bombPower = 1;
        remoteBombItem = false;
        houdiniItem = false;
        creatingBomb = false;
        target = Vector3Int.FloorToInt(transform.position);
        myTime = 0f;
        gravity = 0f;
        transform.Rotate(0, 90, 0, Space.World);
        resultScreenActive = false;
    }

    void Update()
    {
        //I T E M  T I M E R
        //HoudiniTimer
        if (houdiniTimer > 0f)
        {
            houdiniItem = true;
            houdiniTimer -= Time.deltaTime;
        }
        else if (houdiniTimer <= 0f)
        {
            houdiniItem = false;
            houdiniTimer = 0f;
        }

        //remoteBombTimer
        if (remoteBombTimer > 0f)
        {
            remoteBombItem = true;
            remoteBombTimer -= Time.deltaTime;
        }
        else if (remoteBombTimer <= 0f)
        {
            remoteBombItem = false;
            remoteBombTimer = 0f;
        }

        //GameStatePlay
        if (gameManager.gameStatePlay)
        {
            myTime += Time.deltaTime;

            // Player Steuerung
            switch (playerID)
            {
                //Player 1
                case 0:
                    switch (gameManager.controller)
                    {
                        case 0:
                            tmp = Vector3Int.FloorToInt(InputManager.KeyOneMainJoystick());
                            break;
                        case 1:
                            tmp = Vector3Int.FloorToInt(InputManager.XBOXOneMainJoystick());
                            break;
                        case 2:
                            tmp = Vector3Int.FloorToInt(InputManager.PS4OneMainJoystick());
                            break;
                        default:
                            break;
                    }


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

                    switch (gameManager.controller)
                    {
                        case 0:
                            tmp = Vector3Int.FloorToInt(InputManager.KeyTwoMainJoystick());
                            break;
                        case 1:
                            tmp = Vector3Int.FloorToInt(InputManager.XBOXTwoMainJoystick());
                            break;
                        case 2:
                            tmp = Vector3Int.FloorToInt(InputManager.PS4TwoMainJoystick());
                            break;
                        default:
                            break;
                    }

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

                    switch (gameManager.controller)
                    {
                        case 0:
                            tmp = Vector3Int.FloorToInt(InputManager.KeyThreeMainJoystick());
                            break;
                        case 1:
                            tmp = Vector3Int.FloorToInt(InputManager.XBOXThreeMainJoystick());
                            break;
                        case 2:
                            tmp = Vector3Int.FloorToInt(InputManager.PS4ThreeMainJoystick());
                            break;
                        default:
                            break;
                    }

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

                    switch (gameManager.controller)
                    {
                        case 0:
                            tmp = Vector3Int.FloorToInt(InputManager.KeyFourMainJoystick());
                            break;
                        case 1:
                            tmp = Vector3Int.FloorToInt(InputManager.XBOXFourMainJoystick());
                            break;
                        case 2:
                            tmp = Vector3Int.FloorToInt(InputManager.PS4FourMainJoystick());
                            break;
                        default:
                            break;
                    }

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

            if (tmp != nullVector)
            {
                tmp = checkSingleDirection(tmp);
                
                //Im Array aktuelle position loeschen wenn das objekt auch wirklich ein Player ist 
                if (levelGenerator.AllGameObjects[target.x, target.z] != null && levelGenerator.AllGameObjects[target.x, target.z].gameObject.CompareTag("Player"))
                    levelGenerator.AllGameObjects[target.x, target.z] = null;

                //neue position berechenen
                target += tmp;

                //Player wird im Array auf der neuer Position 
                levelGenerator.AllGameObjects[target.x, target.z] = gameObject;

                //speichern des benutzten Bewegungsvectors
                lastTmpVector = tmp;
            }

            //Objekt zum target Bewegung
            tmpVectorPos = transform.position;

            if (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                
                //transform.Rotate(Vector3.RotateTowards(transform.position, target, 0.01f, 0.01f));
                
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
            }

        }
    }

    //Funktion erlaubt das Druecken von 2 Richtungstasten zur gleichen Zeit
    //Die zuletzt gedrückte Taste bestimmt dann die aktuelle Richtung
    Vector3Int checkSingleDirection(Vector3Int tmp)
    {
        //Ist das Produkt != 0 werden 2 Tasten gedruckt
        //Um zu bestimmen welche Taste zusätzlich gedrueckt wurde wird die aktuelle Richtung mit dem Produkt beider Tasten verglichen
        //Daraus kann errechnet werden welcher der neue Richtungvector ist
        int buttonCombi = tmp.x * tmp.z;
        
        if(buttonCombi != 0)
        {
            //Bewegung nach Rechts und Hoch wird gedrueckt
            if(lastDirection.x == 1 && buttonCombi == 1) {
                this.tmp = new Vector3Int(0, 0, 1);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if(freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }
        
            //Bewegung nach Rechts und Runter wird gedrueckt
            if(lastDirection.x == 1 && buttonCombi == -1) {
                this.tmp = new Vector3Int(0, 0, -1);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if (freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }

            //Bewegung nach Linkt und Hoch wird gedrueckt
            if(lastDirection.x == -1 && buttonCombi == -1) {
                this.tmp = new Vector3Int(0, 0, 1);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if (freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }

            //Bewegung nach Links und Runter wird gedrueckt
            if(lastDirection.x == -1 && buttonCombi == 1) {
                this.tmp = new Vector3Int(0, 0, -1);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if (freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }

            //Bewegung nach Oben und Rechts wird gedrueckt
            if(lastDirection.z == 1 && buttonCombi == 1) {
                this.tmp = new Vector3Int(1, 0, 0);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if (freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }

            //Bewegung nach Oben und Links wird gedrueckt
            if(lastDirection.z == 1 && buttonCombi == -1) {
                this.tmp = new Vector3Int(-1, 0, 0);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if (freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }

            //Bewegung nach Unten und Rechts wird gedrueckt
            if(lastDirection.z == -1 && buttonCombi == -1) {
                this.tmp = new Vector3Int(1, 0, 0);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if (freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }

            //Bewegung nach Unten und Links wird gedrueckt
            if(lastDirection.z == -1 && buttonCombi == 1) {
                this.tmp = new Vector3Int(-1, 0, 0);
                if (freeWay(this.tmp))
                {
                    return this.tmp;
                }
                else if (freeWay(lastDirection))
                {
                    return lastDirection;
                }
                return nullVector;
            }
            
        } else {

            if(freeWay(tmp))
            {
                lastDirection = tmp;
                return tmp;
            }

            lastDirection = tmp;
            return nullVector;
        }
        return tmp;
    }

    // Setzt Bombe mit überprüfung von avaibleBomb und aLife
    private void SetBomb()
    {
        creatingBomb = true;
        int bombXPos = Mathf.RoundToInt(transform.position.x);
        int bombZPos = Mathf.RoundToInt(transform.position.z);

        if(avaibleBomb > 0 && houdiniItem == false && (levelGenerator.AllGameObjects[bombXPos, bombZPos] == null ||  houdiniItem == false && levelGenerator.AllGameObjects[bombXPos, bombZPos].gameObject.CompareTag("Player")))
        {
            avaibleBomb -= 1;
            levelGenerator.AllGameObjects[bombXPos, bombZPos] = bombSpawner.SpawnBomb(bombXPos, bombZPos, playerID, bombPower, bombTimer, remoteBombItem, false, playerColor);
        } else {
            creatingBomb = false;
        }
        creatingBomb = false;
    }

    private bool freeWay(Vector3Int tmp)
    {
        //entweder hat sich der Richungsvector nicht geändert oder das Objekt die selbe Position wie TargetVector
        if ((lastTmpVector == tmp || target == transform.position) && myTime > 0.18f)
        {
            int xPos = target.x + tmp.x;
            int zPos = target.z + tmp.z;

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
                if(levelGenerator.SecondaryGameObjects1[xPos, zPos] != null)
                {
                    if(levelGenerator.SecondaryGameObjects1[xPos, zPos].gameObject.CompareTag("KillField"))
                    {
                        dead();
                    }
                }

                return true;
            }
            else
            {
                GameObject go = levelGenerator.AllGameObjects[xPos, zPos].gameObject;

                switch(go.tag)
                {
                    case "FreeFall":
                        StartCoroutine(playerFall());
                        myTime = 0f;
                        return true;
                    
                    case "Item":
                        go.GetComponent<PowerUp>().GrabItem(playerID);
                        levelGenerator.AllGameObjects[zPos, xPos] = null;
                        audioManager.playSound("pickupItem");
                        myTime = 0f;
                        return true;

                    // case "Kiste":
                    //     Debug.Log("Gegen eine Kiste gelaufen");
                    //     myTime = 0f;
                    //     return false;

                    // case "Wand":
                    //     Debug.Log("Gegen eine Wand gelaufen");
                    //     myTime = 0f;
                    //     return false;

                    default:
                        break;
                }
            }
        }
        return false;
    }

    // Player faellt in den Abgund
    public IEnumerator playerFall()
    {
        levelGenerator.AllGameObjects[target.x, target.z] = null;
        rulesScript.playerDeath(playerID, transform.position);
        target.y = -200;

        switch (playerID)
        {
            case 0: audioManager.playSound("scream1"); break;
            case 1: audioManager.playSound("scream2"); break;
            case 2: audioManager.playSound("scream3"); break;
            case 3: audioManager.playSound("scream4"); break;
            default: break;
        }

        while(transform.position.y > -200)
        {
            gravity += Time.deltaTime * 0.8f;
            transform.position = Vector3.MoveTowards(transform.position, target, gravity * gravity);
            yield return null;
        }

        gravity = 0f;
        Destroy(gameObject);
    }


    // Tot trifft ein
    public void dead()
    {
        rulesScript.playerDeath(playerID, transform.position);
        levelGenerator.AllGameObjects[target.x, target.z] = null;
        ghostSpawner.createGhost(transform.position, playerID, playerColor);
        Destroy(gameObject);
    }

    public void winAnimationStart()
    {
        resultScreenActive = true;
        //StartCoroutine(winAnimation());
    }

    public IEnumerator winAnimation()
    {
        bool richtung = true; // true = aufwärts, false = abwärts;

        while(resultScreenActive)
        {
            if (transform.position.y <= 1.5f && richtung)
            {
                transform.Translate(0, 0.2f * (Time.deltaTime + 0.6f), 0);
            }
            else
            {
                richtung = false;
            }

            if (transform.position.y > 0.6f && !richtung)
            {
                transform.Translate(0, -0.2f * (Time.deltaTime + 0.6f), 0);
            }
            else
            {
                richtung = true;
            }
            yield return null;
        }
    }

}