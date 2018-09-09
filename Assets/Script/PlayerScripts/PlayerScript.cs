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
    public Vector3 target;
    private Vector3 lastTmpVector;
    private Vector3 tmpVectorPos;
    private Vector3 tmp;
    private Vector3 nullVector = new Vector3(0, 0, 0);
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
    private Vector3 lastDirection;
    private AudioManager audioManager;
    public bool resultScreenActive;
    public float remoteBombTimer;
    public float houdiniTimer;
    private RulesScript rulesScript;
    private GameManager gameManager;
    private AudioSource playerAudio;

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
        playerAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
        playerColor = playerMaterial.color;
        avaibleBomb = 3;
        lastTmpVector = new Vector3(1, 0, 0);
        lastDirection = new Vector3(1, 0, 0);
        speed = 5.5f;
        bombTimer = 2f;
        Time.timeScale = 1.0f;
        bombPower = 1;
        remoteBombItem = false;
        houdiniItem = false;
        creatingBomb = false;
        target = transform.position;
        levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = gameObject;
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
                            tmp = InputManager.KeyOneMainJoystick();
                            break;
                        case 1:
                            tmp = InputManager.XBOXOneMainJoystick();
                            break;
                        case 2:
                            tmp = InputManager.PS4OneMainJoystick();
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
                            tmp = InputManager.KeyTwoMainJoystick();
                            break;
                        case 1:
                            tmp = InputManager.XBOXTwoMainJoystick();
                            break;
                        case 2:
                            tmp = InputManager.PS4TwoMainJoystick();
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
                            tmp = InputManager.KeyThreeMainJoystick();
                            break;
                        case 1:
                            tmp = InputManager.XBOXThreeMainJoystick();
                            break;
                        case 2:
                            tmp = InputManager.PS4ThreeMainJoystick();
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
                            tmp = InputManager.KeyFourMainJoystick();
                            break;
                        case 1:
                            tmp = InputManager.XBOXFourMainJoystick();
                            break;
                        case 2:
                            tmp = InputManager.PS4FourMainJoystick();
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

            tmp = checkSingleDirection(tmp);

            if (tmp != nullVector)
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
            tmpVectorPos = transform.position;

            if (transform.position != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime)))
            {
                movingSound(true);

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
            } else {

                movingSound(false);
            }
        }
    }


    private bool soundNotPlaying = true;
    public float rollingSoundDelay = 0.2f;
    private void movingSound(bool moving)
    {
        if(moving && soundNotPlaying)
        {
            playerAudio.volume = 0.2f;
            soundNotPlaying = false;
            playerAudio.Play();

        } else if(!moving && !soundNotPlaying) {

            if(rollingSoundDelay > 0f)
            {
                rollingSoundDelay -= Time.deltaTime;

            } else {

                if(playerAudio.volume > 0f)
                {
                    playerAudio.volume -= Time.deltaTime;

                } else {

                    soundNotPlaying = true;
                    playerAudio.Pause();
                    rollingSoundDelay = 0.2f;
                    playerAudio.volume = 0.2f;
                }

            }

        }
    }


    // Funktion erlaubt das Druecken von 2 Richtungstasten zur gleichen Zeit
    // Die zuletzt gedrückte Taste bestimmt dann die aktuelle Richtung
    Vector3 checkSingleDirection(Vector3 tmp)
    {
        if(tmp != nullVector)
        {   
            // Ist das Produkt != 0 werden 2 Tasten gedruckt
            // Um zu bestimmen welche Taste zusätzlich gedrueckt wurde wird die aktuelle Richtung mit dem Produkt beider Tasten verglichen
            // Daraus kann errechnet werden welcher der neue Richtungvector ist
            int calcDir = (int)tmp.x * (int)tmp.z;

            if(calcDir != 0)
            {
                switch((int)lastDirection.x)
                {
                    case 1:
                    // Bewegung nach Rechts...
                    switch(calcDir)
                    {   
                        case  1: return maskDirection(new Vector3(0, 0, 1));  // und Hoch wird gedrueckt
                        case -1: return maskDirection(new Vector3(0, 0, -1)); // und Runter wird gedrueckt
                    }
                    break;

                    case -1:
                    // Bewegung nach Links...
                    switch(calcDir)
                    {   
                        case  1: return maskDirection(new Vector3(0, 0, -1)); // und Runter wird gedrueckt
                        case -1: return maskDirection(new Vector3(0, 0, 1)); // und Hoch wird gedrueckt
                    }
                    break;
                }

                switch((int)lastDirection.z)
                {
                    case 1:
                    // Bewegung nach Oben...
                    switch(calcDir)
                    {   
                        case  1: return maskDirection(new Vector3(1, 0, 0)); // und Rechts wird gedrueckt
                        case -1: return maskDirection(new Vector3(-1, 0, 0)); // und Links wird gedrueckt
                    }
                    break;

                    case -1:
                    // Bewegung nach Unten...
                    switch(calcDir)
                    {   
                        case  1: return maskDirection(new Vector3(-1, 0, 0)); // und Links wird gedrueckt
                        case -1: return maskDirection(new Vector3(1, 0, 0)); // und Rechts wird gedrueckt
                    }
                    break;
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
        return nullVector;
    }

    private Vector3 maskDirection(Vector3 direction)
    {
        if (freeWay(direction))
        {
            return direction;
        }
        else if (freeWay(lastDirection))
        {
            return lastDirection;
        }
        return nullVector;
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

    private bool freeWay(Vector3 tmp)
    {
        //entweder hat sich der Richungsvector nicht geändert oder das Objekt die selbe Position wie TargetVector
        if ((lastTmpVector == tmp || target == transform.position) && myTime > 0.18f)
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
                        playerFall();
                        myTime = 0f;
                        return true;
                    
                    case "Item":
                        go.GetComponent<PowerUp>().GrabItem(playerID);
                        levelGenerator.AllGameObjects[(int)go.transform.position.x, (int)go.transform.position.z] = null;
                        audioManager.playSound("pickupItem");
                        myTime = 0f;
                        return true;

                    default:
                        break;
                }
            }
        }

        return false;
    }

    // Player faellt in den Abgund
    // playerFall & playerFallCore damit die Coroutine innerhalb des Players und nicht im Aufrufenden Objekt laeuft
    public void playerFall()
    {
        if(gameManager.gameStatePlay)
        {
            StartCoroutine(playerFallCore());
        }
    }

    private IEnumerator playerFallCore()
    {
        rulesScript.playerDeath(playerID, transform.position);
        target.y = -200f;

        switch (playerID)
        {
            case 0: audioManager.playSound("scream1"); break;
            case 1: audioManager.playSound("scream2"); break;
            case 2: audioManager.playSound("scream3"); break;
            case 3: audioManager.playSound("scream4"); break;
            default: break;
        }

        while(transform.position.y > -50 && gameManager.gameStatePlay)
        {
            gravity += Time.deltaTime * 0.8f;
            transform.position = Vector3.MoveTowards(transform.position, target, gravity * gravity);
            yield return null;
        }

        gravity = 0f;
        if(gameObject != null)
        {
            levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
            Destroy(gameObject);
        }
        
    }


    // Tot trifft ein
    public void dead()
    {
        rulesScript.playerDeath(playerID, transform.position);
        levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
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