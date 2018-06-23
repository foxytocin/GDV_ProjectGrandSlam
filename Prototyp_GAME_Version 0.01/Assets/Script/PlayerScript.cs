using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool moveX;
    public bool moveZ;
    public WorldScript world;
    public GameObject body;
    public GameObject bombe_Prefab;
    public int playerID;
    public int life;
    public int avaibleBomb;
    public float speed;
    public int range;
    public bool aLife;
    public List<GameObject> playerList;

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
        range = 1;
        aLife = true;
        speedMultiply = 0.01f;

    }

    void Update()
    {

        if (playerID == 0)
        {
            Vector3 tmp = InputManager.OneMainJoystick();
            if (tmp != new Vector3(0, 0, 0))
            {
                speedMulti();
                wallTest(playerList[0]);
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
                wallTest(playerList[1]);
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
                wallTest(playerList[2]);
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
                wallTest(playerList[3]);
                //Debug.Log("move" + body.name);
                body.transform.Translate(tmp.x * speedMultiply * Time.deltaTime, 0, tmp.z * speedMultiply * Time.deltaTime);
            }
            else if (speedMultiply > 0.1f)
            {
                speedMultiply -= 0.1f;
                //Debug.Log("Increase SM: " +speedMultiply);
            }
        }

        if (speedMultiply < 0.01f)
        {
            speedMultiply = 0.01f;
            //Debug.Log("Setting SM: " +speedMultiply);
        }

        if (InputManager.OneXButton())
        {
            setBomb(0);
        }

        if (InputManager.TwoXButton())
        {
            setBomb(1);
        }

        if (InputManager.ThreeXButton())
        {
            setBomb(2);
        }

        if (InputManager.FourXButton())
        {
            setBomb(3);
        }

        if (Input.GetKeyDown("space"))
        {
            dead(1);
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
        Debug.Log("player_" + playerID.ToString() + " is Dead");
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
    public void setRange()
    {
        range++;
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


    // Setzt Bombe mit überprüfung von avaibleBomb und aLife
    public void setBomb(int id)
    {
        if (playerList[id].GetComponent<PlayerScript>().getAvaibleBomb() > 0 && playerList[id].GetComponent<PlayerScript>().getALife())
        {
            Debug.Log("Bombe_Player_" + id.ToString());
            createBomb(playerList[id]);
            playerList[id].GetComponent<PlayerScript>().setAvaibleBomb(-1);
        }
    }


    void createBomb(GameObject player)
    {
        if (world.WorldArray[(int)player.transform.position.x, (int)player.transform.position.z] == null)
        {
                GameObject bombeInstanz;
                bombeInstanz = Instantiate(bombe_Prefab, new Vector3(Mathf.Round(player.transform.position.x), -0.1f, Mathf.Round(player.transform.position.z)), Quaternion.identity);

                BombeScript thisBombeScript = bombeInstanz.GetComponent<BombeScript>();

                thisBombeScript.bombTimer = 3; //WERT MUSS DURCH ITEM ERHÖHT WERDEN
                thisBombeScript.name = "Bombe"; //BOMBENNAME == PLAYERID. Bombe wird nämlich nur über name gefunden.
                thisBombeScript.bombOwnerPlayerID = player.GetComponent<PlayerScript>().getPlayerID();
                thisBombeScript.playerList = player.GetComponent<PlayerScript>().getPlayerList();

                world.WorldArray[(int)player.transform.position.x, (int)player.transform.position.z] = bombeInstanz;
                //angle += angle;
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
       
        if (world.WorldArray[(int)Mathf.Round(player.transform.position.x), (int)Mathf.Round(player.transform.position.z)] != null)
        {
            //Debug.Log("Object an aktueller Stelle: " + World.WorldArray[(int)xPosition, (int)zPosition]);
            if (world.WorldArray[(int)Mathf.Round(player.transform.position.x), (int)Mathf.Round(player.transform.position.z)].name == "Item_SpeedBoost")
            {
                Destroy(world.WorldArray[(int)Mathf.Round(player.transform.position.x), (int)Mathf.Round(player.transform.position.z)]);
                speedMultiply = 10f;
            }
            if (world.WorldArray[(int)Mathf.Round(player.transform.position.x), (int)Mathf.Round(player.transform.position.z)].name == "Item_SpeedLow")
            {
                Destroy(world.WorldArray[(int)Mathf.Round(player.transform.position.x), (int)Mathf.Round(player.transform.position.z)]);
                speedMultiply = 0.5f;
            }
        }
        else
        {
            //Debug.Log("Object an aktueller Stelle: Freier Weg");
        }
    }

}

