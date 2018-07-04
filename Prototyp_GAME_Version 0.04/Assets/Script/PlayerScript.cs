using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public GameObject body;
    public int playerID;
    public int life;
    public int avaibleBomb;
    public float speed;
    public int bombTimer;
    public int range;
    public bool aLife;
    public bool remoteBomb;
    public List<GameObject> playerList;
    public bool creatingBomb;
    Vector3 target;
    Vector3 lastTmpVector;
    float myTime;
    public List<GameObject> remoteBombList;

    void Awake()
    {
        playerID = 0;
        life = 3;
        avaibleBomb = 1000;
        speed = 5f;
        bombTimer = 3;
        range = 2;
        aLife = true;
        remoteBomb = false;
        creatingBomb = false;
        target = body.transform.position;
        myTime = 0f;
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

                if (InputManager.OneAButton())
                {
                    // RemoteBombe zünden Player_One
                }

                if (InputManager.OneStartButton())
                {
                    //Pause aufufen
                }

                break;

            //Player 2
            case 1:

                tmp = InputManager.TwoMainJoystick();

                if (InputManager.TwoXButton() && !creatingBomb)
                    SetBomb(1);

                if (InputManager.TwoAButton())
                {
                    // RemoteBombe zünden Player_Two
                }

                if (InputManager.TwoStartButton())
                {
                    //Pause aufufen
                }

                break;

            //Player 3
            case 2:

                tmp = InputManager.ThreeMainJoystick();

                if (InputManager.ThreeXButton() && !creatingBomb)
                    SetBomb(2);

                if (InputManager.ThreeAButton())
                {
                    // RemoteBombe zünden Player_Three
                }

                if (InputManager.ThreeStartButton())
                {
                    //Pause aufufen
                }

                break;

            //Player 4
            case 3:

                tmp = InputManager.FourMainJoystick();

                if (InputManager.FourXButton() && !creatingBomb)
                    SetBomb(3);

                if (InputManager.FourAButton())
                {
                    // RemoteBombe zünden Player_Four
                }

                if (InputManager.FourStartButton())
                {
                    //Pause aufufen
                }

                break;

            //Player Default (Exeption)
            default:
                Debug.Log("Playerfehler");
                break;
        }

        //Target bewegen
        if (freeWay(tmp))
        {
            target += tmp;
            lastTmpVector = tmp;
        }

        //Objekt zum target Bewegung
        body.transform.position = Vector3.MoveTowards(body.transform.position, target, speed * Time.deltaTime);

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
    public void dead(int id)
    {
        Debug.Log("Player_" + playerID.ToString() + " is Dead");
        playerList[id].GetComponent<PlayerScript>().setLife(-1);
        playerList[id].GetComponent<PlayerScript>().setALife(false);
        playerList[id].SetActive(false);
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
        return aLife;
    }

    public void setALife(bool tmp)
    {
        aLife = tmp;
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
    public int getbombTimer()
    {
        return bombTimer;
    }

    public void setbombTimer(int tmp)
    {
        bombTimer = tmp;
    }



    // Setzt Bombe mit überprüfung von avaibleBomb und aLife
    public void SetBomb(int id)
    {
        if (playerList[id].GetComponent<PlayerScript>().getAvaibleBomb() > 0 && playerList[id].GetComponent<PlayerScript>().getALife())
        {
            creatingBomb = true;
            FindObjectOfType<BombSpawner>().SpawnBomb(id);

        } else {
            creatingBomb = false;
        }
    }


    bool freeWay(Vector3 tmp)
    {
        // Pruefen das keine Zwei Tasten für diagonales gehen gedrückt sind 
        if (tmp == new Vector3(-1, 0, 0) || tmp == new Vector3(1, 0, 0) || tmp == new Vector3(0, 0, -1) || tmp == new Vector3(0, 0, 1))
        {
            //entweder hat sich der Richungsvector nicht geändert oder das Objekt die selbe Position wie TargetVector
            if ((lastTmpVector == tmp || target == body.transform.position) && myTime > 0.2f)
            {
                //Prueft im Array an der naechsten stelle ob dort ein objekt liegt wenn nicht dann return.true
                if (levelGenerator.AllGameObjects[Mathf.RoundToInt(target.x + tmp.x), Mathf.RoundToInt(target.z + tmp.z)] == null)
                {
                    myTime = 0f;
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}