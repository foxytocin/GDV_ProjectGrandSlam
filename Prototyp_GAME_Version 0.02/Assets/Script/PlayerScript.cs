using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public WorldScript world;
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
    public List<bool> himmelsRichtungen;

    float speedMultiply;

    
    // Use this for initialization
    void Awake()
    {
        playerID = 0;
        life = 3;
        avaibleBomb = 1000;
        speed = 1;
        bombTimer = 3;
        range = 1;
        aLife = true;
        remoteBomb = false;
        speedMultiply = 0.01f;
        himmelsRichtungen.Add(true);//Norden
        himmelsRichtungen.Add(true);//Osten
        himmelsRichtungen.Add(true);//Süden
        himmelsRichtungen.Add(true);//Westen
    }
    /*
    void OnTriggerEnter(Collider collision)
    {
        Vector3 collisionVector = new Vector3(Mathf.Round(collision.transform.position.x - playerList[playerID].transform.position.x), collision.transform.position.y - playerList[playerID].transform.position.y, Mathf.Round(collision.transform.position.z - playerList[playerID].transform.position.z));
        Debug.Log(collisionVector.ToString());

        if (collisionVector.x == 1f)
        {
            himmelsRichtungen[1] = false;
            body.transform.Translate(-0.1f, 0, 0);
        }

        if (collisionVector.x == -1f)
        {
            himmelsRichtungen[3] = false;
            body.transform.Translate(0.1f, 0, 0);
        }

        if (collisionVector.z == 1f)
        {
            himmelsRichtungen[0] = false;
            body.transform.Translate(0, 0, -0.1f);
        }

        if (collisionVector.z == -1f)
        {
            himmelsRichtungen[2] = false;
            body.transform.Translate(0, 0, 0.1f);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Vector3 collisionVector = new Vector3(Mathf.Round(collision.transform.position.x - playerList[playerID].transform.position.x), collision.transform.position.y - playerList[playerID].transform.position.y, Mathf.Round(collision.transform.position.z - playerList[playerID].transform.position.z));
        Debug.Log(collisionVector.ToString());

        if (collisionVector.x < 0f)
            himmelsRichtungen[3] = true;
        else
            himmelsRichtungen[1] = true;


        if (collisionVector.z < 0f)
            himmelsRichtungen[2] = true;
        else
            himmelsRichtungen[0] = true;
    }

    */

    void Update()
    {
        // Player Steuerung
        
            Vector3 tmp;
            switch (playerID)
            {
                case 0:
                
                    tmp = InputManager.OneMainJoystick();

                    if ((!himmelsRichtungen[0] && tmp.z > 0) || (!himmelsRichtungen[2] && tmp.z < 0))
                    {
                        tmp.z = 0f;
                    }

                    if ((!himmelsRichtungen[1] && tmp.x > 0) || (!himmelsRichtungen[3] && tmp.x < 0))
                    {
                        tmp.x = 0f;
                    }

                if (tmp != new Vector3(0, 0, 0))
                    {
                    Debug.Log("Move" + tmp.ToString());
                        speedMulti();
                        wallTest(playerList[0]);
                        //Debug.Log("move" + body.name);
                        body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
                    }

                    else if (speedMultiply > 0.1f)
                    {
                        speedMultiply -= 0.1f;
                        //Debug.Log("Increase SM: " +speedMultiply);
                    }
                    break;

                case 1:

                    tmp = InputManager.TwoMainJoystick();
                    if (tmp != new Vector3(0, 0, 0))
                    {
                        speedMulti();
                        wallTest(playerList[1]);
                        //Debug.Log("move" + body.name);
                        body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
                    }
                    else if (speedMultiply > 0.1f)
                    {
                        speedMultiply -= 0.1f;
                        //Debug.Log("Increase SM: " +speedMultiply);
                    }
                    break;

                case 2:

                    tmp = InputManager.ThreeMainJoystick();
                    if (tmp != new Vector3(0, 0, 0))
                    {
                        speedMulti();
                        wallTest(playerList[2]);
                        //Debug.Log("move" + body.name);
                        body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
                    }
                    else if (speedMultiply > 0.1f)
                    {
                        speedMultiply -= 0.1f;
                        //Debug.Log("Increase SM: " +speedMultiply);
                    }
                    break;

                case 3:
                    tmp = InputManager.FourMainJoystick();
                    if (tmp != new Vector3(0, 0, 0))
                    {
                        speedMulti();
                        wallTest(playerList[3]);
                        //Debug.Log("move" + body.name);
                        body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
                    }
                    else if (speedMultiply > 0.1f)
                    {
                        speedMultiply -= 0.1f;
                        //Debug.Log("Increase SM: " +speedMultiply);
                    }
                    break;

                default:
                    Debug.Log("Steurungsfehler!");
                    break;
            }


       
        // Speed Verfall
        if (speedMultiply < 0.01f)
        {
            speedMultiply = 0.01f;
            //Debug.Log("Setting SM: " +speedMultiply);
        }



        // X_Button Abfrage fuer die Player
        if (InputManager.OneXButton())
        {
            SetBomb(0);
        }

        if (InputManager.TwoXButton())
        {
            SetBomb(1);
        }

        if (InputManager.ThreeXButton())
        {
            SetBomb(2);
        }

        if (InputManager.FourXButton())
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

   
    public void setWorld(WorldScript world)
    {
        this.world = world;
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
            //Debug.Log("Bombe_Player_" + id.ToString());
            FindObjectOfType<BombSpawner>().SpawnBomb(id);
        }
    }


    void speedMulti()
    {
        if (speedMultiply < 5.0f)
        {
            speedMultiply += (speedMultiply / (speedMultiply * 10.0f));
            //Debug.Log("Degreace SM: " +speedMultiply);
        }
    }

    void wallTest(GameObject player)
    {

        int xPos = (int)Mathf.Round(player.transform.position.x);
        int zPos = (int)Mathf.Round(player.transform.position.z);

        if (world.WorldArray[xPos, zPos] != null)
        {
            //Debug.Log("Object an aktueller Stelle: " + world.WorldArray[xPos, zPos]);
            if (world.WorldArray[xPos, zPos].name == "Item_SpeedBoost")
            {
                Destroy(world.WorldArray[xPos, zPos]);
                speedMultiply = 8f;
            }
            if (world.WorldArray[xPos, zPos].name == "Item_BombPowerUp")
            {
                Destroy(world.WorldArray[xPos, zPos]);
                player.GetComponent<PlayerScript>().setRange(1);
            }
        }
        else
        {
            world.WorldArray[xPos, zPos] = player;
            //Debug.Log("Object an aktueller Stelle: Freier Weg");
        }
    }

    
}



