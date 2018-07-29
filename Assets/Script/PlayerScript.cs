using System.Collections.Generic;
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
    public bool remoteBomb;

    public List<GameObject> playerList;
    public bool creatingBomb;
    public Vector3 target;
    Vector3 lastTmpVector;
    float myTime;
    public List<GameObject> remoteBombList;
    public LevelGenerator levelGenerator;
    public GhostSpawnerScript ghostSpawner;
    public CameraMovement camera;
    bool RichtungsAenderung; //true == z; false == x 
    bool fall = false;
    float gravity;



    void Start()
    {
        life = 3;
        avaibleBomb = 3;
        speed = 5f;
        bombTimer = 2f;
        range = 1;
        alive = true;
        remoteBomb = false;
        creatingBomb = false;
        target = transform.position;
        myTime = 0f;
        gravity = 0f;
        levelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = playerList[playerID];
        camera.playerPosition(transform.position, playerID);
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
                    SetBomb(0);

                // RemoteBombe zünden Player_One
                if (InputManager.OneAButton())
                    FindObjectOfType<RemoteBomb>().remoteBomb(0);

                //Pause aufrufen
                if (InputManager.OneStartButton())
                    return;
                
                break;

            //Player 2
            case 1:

                tmp = InputManager.TwoMainJoystick();

                if (InputManager.TwoXButton() && !creatingBomb)
                    SetBomb(1);

                // RemoteBombe zünden Player_Two
                if (InputManager.TwoAButton())
                    FindObjectOfType<RemoteBomb>().remoteBomb(1);

                //Pause aufrufen
                if (InputManager.TwoStartButton())
                    return;

                break;

            //Player 3
            case 2:

                tmp = InputManager.ThreeMainJoystick();

                if (InputManager.ThreeXButton() && !creatingBomb)
                    SetBomb(2);

                // RemoteBombe zünden Player_Three
                if (InputManager.ThreeAButton())
                    FindObjectOfType<RemoteBomb>().remoteBomb(2);

                //Pause aufrufen
                if (InputManager.ThreeStartButton())
                    return;

                break;

            //Player 4
            case 3:

                tmp = InputManager.FourMainJoystick();

                if (InputManager.FourXButton() && !creatingBomb)
                    SetBomb(3);

                // RemoteBombe zünden Player_Four
                if (InputManager.FourAButton())
                    FindObjectOfType<RemoteBomb>().remoteBomb(3);

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
            //Debug.Log("nicht Tot");
            if (transform.position != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime)) && alive)
            {
                if (tmpVectorPos.x != transform.position.x && RichtungsAenderung)
                {
                    transform.Rotate(0, 90, 0, Space.World);
                    RichtungsAenderung = false;
                }
                else if (tmpVectorPos.z != transform.position.z && !RichtungsAenderung)
                {
                    transform.Rotate(0, -90, 0,Space.World);
                    RichtungsAenderung = true;
                }

                if (tmpVectorPos.z < transform.position.z || tmpVectorPos.x < transform.position.x)
                    transform.Rotate(7, 0, 0);
                else if (tmpVectorPos.z > transform.position.z || tmpVectorPos.x > transform.position.x)
                    transform.Rotate(-7, 0, 0);

                    camera.playerPosition(transform.position, playerID);
            }
            else if (transform.position.y < 0.45f && !alive)
            {
                transform.position.Set(transform.position.x, -1, transform.position.z);
                camera.playerPosition(transform.position, playerID);
            }
        }
        

        if(fall)
        {
            gravity += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, gravity * gravity);

            if (transform.position.y == -50)
            {
                gravity = 0f;
                setLife(-1);
                setALife(false);
                this.gameObject.SetActive(false);
            }
        }
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
        return remoteBomb;
    }

    public void setRemoteBombe(bool tmp)
    {
        remoteBomb = tmp;
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



    // Setzt Bombe mit überprüfung von avaibleBomb und aLife
    void SetBomb(int id)
    {
        if (playerList[id].GetComponent<PlayerScript>().getAvaibleBomb() > 0 && playerList[id].GetComponent<PlayerScript>().getALife())
        {
            creatingBomb = true;
            FindObjectOfType<BombSpawner>().SpawnBomb(transform.position, id);

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
                //Prueft im Array an der naechsten stelle ob dort ein objekt liegt wenn nicht dann return.true
                if (levelGenerator.AllGameObjects[Mathf.RoundToInt(target.x + tmp.x), Mathf.RoundToInt(target.z + tmp.z)] == null)
                {
                    myTime = 0f;
                    return true;
                }
                else
                {
                    if (levelGenerator.AllGameObjects[(int)(target.x + tmp.x), (int)(target.z + tmp.z)].gameObject.CompareTag("FreeFall"))
                    {
                        playerFall();
                    }

                    if (levelGenerator.AllGameObjects[(int)(target.x + tmp.x), (int)(target.z + tmp.z)].gameObject.CompareTag("KillField"))
                    {
                        dead();
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
        target.y = -50f;
        fall = true;
        alive = false;
    }
                    
}