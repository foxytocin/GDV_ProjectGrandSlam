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
    public bool gameStatePlay;

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
    private Material playerMaterial;
    private Color32 playerColor;
    private Light playerLight;
    private Vector3 lastDirection;
    private AudioManager audioManager;
    public bool resultScreenActive;
    public float remoteBombTimer;
    public float houdiniTimer;

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
        cam = FindObjectOfType<CameraMovement>();
    }

    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
        playerLight = GetComponent<Light>();
        playerColor = playerMaterial.color;
        gameStatePlay = false;
        travelDistanceStart = (int)transform.position.z;
        avaibleBomb = 3;
        speed = 5f;
        bombTimer = 2f;
        Time.timeScale = 1.0f;
        bombPower = 1;
        remoteBombItem = false;
        houdiniItem = false;
        creatingBomb = false;
        target = transform.position;
        myTime = 0f;
        gravity = 0f;
        cam.PlayerPosition(transform.position, playerID);
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
            Debug.Log(houdiniTimer);

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
            Debug.Log(remoteBombTimer);

        }

        else if (remoteBombTimer <= 0f)
        {
            remoteBombItem = false;
            remoteBombTimer = 0f;
        }

        //GameStatePlay
        if (gameStatePlay)
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

                //Debug.Log("nicht Tot");
                if (transform.position != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime)))
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
                else if (transform.position.y < 0.45f)
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
            levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
            gravity += Time.deltaTime * 0.8f;
            transform.position = Vector3.MoveTowards(transform.position, target, gravity * gravity);

            if (transform.position.y == -200)
            {
                gravity = 0f;
                Debug.Log("Player_" + playerID.ToString() + " is Dead");
                cam.PlayerPosition(new Vector3(0f, -2f, 0f), playerID);
                FindObjectOfType<RulesScript>().playerDeath(playerID, transform.position);
                Destroy(gameObject);
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

    // Setzt Bombe mit überprüfung von avaibleBomb und aLife
    private void SetBomb()
    {
        creatingBomb = true;
        int bombXPos = Mathf.RoundToInt(transform.position.x);
        int bombZPos = Mathf.RoundToInt(transform.position.z);

        if(avaibleBomb > 0 && (levelGenerator.AllGameObjects[bombXPos, bombZPos] == null || levelGenerator.AllGameObjects[bombXPos, bombZPos].gameObject.CompareTag("Player")))
        {
            avaibleBomb -= 1;
            levelGenerator.AllGameObjects[bombXPos, bombZPos] = bombSpawner.SpawnBomb(bombXPos, bombZPos, playerID, bombPower, bombTimer, remoteBombItem, playerColor);
        } else {
            creatingBomb = false;
        }
        creatingBomb = false;
    }

    private bool freeWay(Vector3 tmp)
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
                        audioManager.playSound("pickupItem");
                    }
                    return false;
                }
            }
            return false;
    }

    // Player faellt in den Abgund
    public void playerFall()
    {
        switch (playerID)
        {
            case 0: audioManager.playSound("scream1"); break;
            case 1: audioManager.playSound("scream2"); break;
            case 2: audioManager.playSound("scream3"); break;
            case 3: audioManager.playSound("scream4"); break;
            default: break;
        }

        target.y = -200f;
        fall = true;
    }

    // Restart des Levels
    public IEnumerator playerFallRestart()
    {
        levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
        target.y = -50f;
        fall = true;

        while(transform.position.y > target.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.3f);
            yield return null;
        }
        
        Debug.Log("Player_" + playerID.ToString() + " is Dead");
        cam.PlayerPosition(new Vector3(0f, -2f, 0f), playerID);
        gameObject.SetActive(false);
        Destroy(gameObject);
        StopAllCoroutines();
    }

    // Tot trifft ein
    public void dead()
    {
        levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
        Debug.Log("Player_" + playerID.ToString() + " is Dead");
        ghostSpawner.createGhost(transform.position, playerID, playerColor);
        transform.Translate(0f, -2f, 0f);
        cam.PlayerPosition(transform.position, playerID);
        FindObjectOfType<RulesScript>().playerDeath(playerID, transform.position);
        levelGenerator.AllGameObjects[(int)target.x, (int)target.z] = null;
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


    public IEnumerator playerGlowOn()
	{
		float emission = 0f;
		Color baseColor = playerColor;
		playerLight.intensity = 0f;
		playerLight.enabled = true;

		while(emission < 2.3f)
		{
			emission += Time.deltaTime * 0.2f;
			Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
			playerMaterial.SetColor("_EmissionColor", finalColor);
			playerMaterial.EnableKeyword("_EMISSION");
			playerLight.intensity = emission;

			yield return null;
		}
	}

    public IEnumerator playerGlowOff()
	{
		float emission = 2.3f;
		Color baseColor = playerColor;

		while(emission > 0f)
		{
			emission -= Time.deltaTime * 0.3f;
			Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
			playerMaterial.SetColor("_EmissionColor", finalColor);
			playerLight.intensity = emission;

			yield return null;
		}

		playerMaterial.DisableKeyword("_EMISSION");
		playerLight.enabled = false;
		playerLight.intensity = 0f;
	}


    // public IEnumerator fadeToDeath()
    // {
    //     float alpha = playerMaterial.color.a;
    //     Color fadeColor = playerColor;

    //     while(alpha > 0f)
    //     {
    //         Debug.Log("fadeToDead: " +alpha);
    //         alpha -= Time.deltaTime;
    //         fadeColor.a = alpha;
    //         playerMaterial.color = (Color32)fadeColor;
    //         yield return null;
    //     }

    //     Destroy(gameObject);
    // }

}