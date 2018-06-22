using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject body;
    public int playerID;
    public int life;
    public int avaibleBomb;
    public float speed;
    public int range;
    public bool aLife;
    public List<GameObject> playerList;

    // Use this for initialization
    void Awake()
    {
        playerID = 0;
        life = 3;
        avaibleBomb = 100;
        speed = 1;
        range = 1;
        aLife = true;

    }

    void Update()
    {
        if (playerID == 0)
        {
            //Debug.Log("move" + body.name);
            body.transform.Translate(InputManager.OneMainJoystick());
        }

        if (playerID == 1)
        {
            //Debug.Log("move" + body.name);
            body.transform.Translate(InputManager.TwoMainJoystick());
        }

        if (playerID == 2)
        {
            //Debug.Log("move" + body.name);
            body.transform.Translate(InputManager.ThreeMainJoystick());
        }

        if (playerID == 3)
        {
            //Debug.Log("move" + body.name);
            body.transform.Translate(InputManager.FourMainJoystick());
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


    // Uebergabe der PlayerID
    public void setPlayerID(int id)
    {
        playerID = id;
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
            //placeBomb(body.trasform.position, playerID, range)
            playerList[id].GetComponent<PlayerScript>().setAvaibleBomb(-1);
        }
    }
}
