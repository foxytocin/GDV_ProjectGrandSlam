<<<<<<< HEAD
﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool moveX;
    public bool moveZ;
    public LevelGenerator LevelGenerator;
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
    public List<GameObject> remoteBombList;
    public bool creatingBomb;
    public RemoteBomb RemoteBomb;

    float speedMultiply;

    // Use this for initialization
    void Awake()
    {
        moveX = true;
        moveZ = true;
        playerID = 0;
        life = 3;
        avaibleBomb = 1000;
        speed = 1;
        bombTimer = 3;
        range = 3;
        aLife = true;
        remoteBomb = true;
        speedMultiply = 0.01f;
        creatingBomb = false;

    }

    void Update()
    {
        // Player Steuerung
        if (playerID == 0)
        {
            Vector3 tmp = InputManager.OneMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                speedMulti();
                //wallTest(playerList[0]);
                //Debug.Log("move" + body.name);
                body.transform.Translate(tmp.x * speedMultiply  * Time.deltaTime, 0 , tmp.z * speedMultiply * Time.deltaTime);
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }

        }

        if (playerID == 1)
        {
            Vector3 tmp = InputManager.TwoMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                speedMulti();
                //wallTest(playerList[1]);
                //Debug.Log("move" + body.name);
                body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }

        if (playerID == 2 && (moveX || moveZ))
        {
            Vector3 tmp = InputManager.ThreeMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                speedMulti();
                //wallTest(playerList[2]);
                //Debug.Log("move" + body.name);
                body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }

        if (playerID == 3 && (moveX || moveZ))
        {
            Vector3 tmp = InputManager.FourMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                speedMulti();
                //wallTest(playerList[3]);
                //Debug.Log("move" + body.name);
                body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }


        // Speed Verfall
        if (speedMultiply < 0.01f)
        {
            speedMultiply = 0.01f;
            //Debug.Log("Setting SM: " +speedMultiply);
        }

        // X_Button Abfrage fuer die Player
        if (InputManager.OneXButton() && !creatingBomb)
        {
            SetBomb(0);
        }

        if (InputManager.TwoXButton() && !creatingBomb)
        {
            SetBomb(1);
        }

        if (InputManager.ThreeXButton() && !creatingBomb)
        {
            SetBomb(2);
        }

        if (InputManager.FourXButton() && !creatingBomb)
        {
            SetBomb(3);
        }



        // A_Button Abfrage fuer die Player
        if (InputManager.OneAButton())
        {
            //FindObjectOfType<RemoteBomb>().remoteBomb(0);
            RemoteBombEx(0);
        }

        if (InputManager.TwoAButton())
        {
            //FindObjectOfType<RemoteBomb>().remoteBomb(1);
            RemoteBombEx(1);
        }

        if (InputManager.ThreeAButton())
        {
            //FindObjectOfType<RemoteBomb>().remoteBomb(2);
        }

        if (InputManager.FourAButton())
        {
            //FindObjectOfType<RemoteBomb>().remoteBomb(3);
        }



        // Start_Button Abfrage fuer die Player 
        if (InputManager.OneStartButton())
        {
            //Pause aufufen
        }

        if (InputManager.TwoStartButton())
        {
            //Pause aufufen
        }

        if (InputManager.ThreeStartButton())
        {
            //Pause aufufen
        }

        if (InputManager.FourStartButton())
        {
            //Pause aufufen
        }

    }


    public void setWorld(LevelGenerator LevelGenerator)
    {
        this.LevelGenerator = LevelGenerator;
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
            //Debug.Log("Bombe_Player_" + id.ToString());
            FindObjectOfType<BombSpawner>().SpawnBomb(id);
        } else {
            creatingBomb = false;
        }
    }

    public void RemoteBombEx(int id)
    {
        foreach(GameObject bomb in playerList[id].GetComponent<PlayerScript>().remoteBombList) {
            bomb.GetComponent<BombScript>().bombTimer = 0;
            bomb.GetComponent<BombScript>().remoteBomb = false;
        }
        playerList[id].GetComponent<PlayerScript>().remoteBombList.Clear();
    }


    void speedMulti()
    {
        if (speedMultiply < 5.0f)
        {
            speedMultiply += (speedMultiply / (speedMultiply * 10.0f));
            //Debug.Log("Degreace SM: " +speedMultiply);
        }
    }
}

=======

﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool moveX;
    public bool moveZ;
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
    public Vector3 target;
    public Vector3 lastTmpVector;
    public bool[] himmelsrichtungen;
    float myTime;


    float speedMultiply;

    // Use this for initialization
    void Awake()
    {
        moveX = true;
        moveZ = true;
        playerID = 0;
        life = 3;
        avaibleBomb = 1000;
        speed = 5f;
        bombTimer = 3;
        range = 1;
        aLife = true;
        remoteBomb = false;
        speedMultiply = 0.01f;
        creatingBomb = false;
        target = body.transform.position;
        myTime = 0f;
    }

    void FixedUpdate()
    {
        myTime += Time.deltaTime;
        // Player Steuerung
        if (playerID == 0)
        {
            Vector3 tmp = InputManager.OneMainJoystick();
            //Debug.Log("move: " + tmp.ToString());
            if (tmp != new Vector3(0, 0, 0))
            {
                if (( lastTmpVector == tmp || target == body.transform.position ) && myTime > 0.2f)
                {
                    if (freeWay(tmp))
                    {
                        target += tmp;
                        lastTmpVector = tmp;
                    }
                    myTime = 0f;
                }
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }

        if (playerID == 1)
        {
            Vector3 tmp = InputManager.TwoMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                if ((lastTmpVector == tmp || target == body.transform.position) && myTime > 0.2f)
                {
                    if (freeWay(tmp))
                    {
                        target += tmp;
                        lastTmpVector = tmp;
                    }
                    myTime = 0f;
                }
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }

        if (playerID == 2)
        {
            Vector3 tmp = InputManager.ThreeMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                if ((lastTmpVector == tmp || target == body.transform.position) && myTime > 0.2f)
                {
                    if (freeWay(tmp))
                    {
                        target += tmp;
                        lastTmpVector = tmp;
                    }
                    myTime = 0f;
                }
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }

        if (playerID == 3)
        {
            Vector3 tmp = InputManager.FourMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                if ((lastTmpVector == tmp || target == body.transform.position) && myTime > 0.2f)
                {
                    if (freeWay(tmp))
                    {
                        target += tmp;
                        lastTmpVector = tmp;
                    }
                    myTime = 0f;
                }
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }

        body.transform.position = Vector3.MoveTowards(body.transform.position, target, speed * Time.deltaTime);

        // Speed Verfall
        if (speedMultiply < 0.01f)
        {
            speedMultiply = 0.01f;
            //Debug.Log("Setting SM: " +speedMultiply);
        }

        // X_Button Abfrage fuer die Player
        if (InputManager.OneXButton() && !creatingBomb)
        {
            SetBomb(0);
        }

        if (InputManager.TwoXButton() && !creatingBomb)
        {
            SetBomb(1);
        }

        if (InputManager.ThreeXButton() && !creatingBomb)
        {
            SetBomb(2);
        }

        if (InputManager.FourXButton() && !creatingBomb)
        {
            SetBomb(3);
        }



        // A_Button Abfrage fuer die Player
        if (InputManager.OneAButton())
        {
            // RemoteBombe zünden Player_One
        }

        if (InputManager.TwoAButton())
        {
            // RemoteBombe zünden Player_Two
        }

        if (InputManager.ThreeAButton())
        {
            // RemoteBombe zünden Player_Three
        }

        if (InputManager.FourAButton())
        {
            // RemoteBombe zünden Player_Four
        }



        // Start_Button Abfrage fuer die Player 
        if (InputManager.OneStartButton())
        {
            //Pause aufufen
        }

        if (InputManager.TwoStartButton())
        {
            //Pause aufufen
        }

        if (InputManager.ThreeStartButton())
        {
            //Pause aufufen
        }

        if (InputManager.FourStartButton())
        {
            //Pause aufufen
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


    void speedMulti()
    {
        if (speedMultiply < 5.0f)
        {
            speedMultiply += (speedMultiply / (speedMultiply * 10.0f));
        }
    }

    bool freeWay(Vector3 tmp)
    {
        if(levelGenerator.AllGameObjects[(int)(target.x + tmp.x),(int)(target.z + tmp.z)] == null)
        {
            return true;
        }

        return false;
    }
}


﻿
>>>>>>> master
