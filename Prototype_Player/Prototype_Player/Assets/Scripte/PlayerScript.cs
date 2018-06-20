using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    GameObject body;
    int playerID;
    int life;
    int avaibleBombs;
    int speed;
    int range;

	// Use this for initialization
	public PlayerScript(int playerID)
    {
        this.playerID = playerID;
        body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Player_" + playerID.ToString();
        Debug.Log(body.name);
        life = 3;
        avaibleBombs = 1;
        speed = 1;
        range = 1;
    }

    void Start()
    {

    }

    public void setBomb()
    {

        Debug.Log("Bombe_Player_"+ playerID.ToString());
    }

    // Update is called once per frame
    public void  move()
    {
        Debug.Log("move");
        body.transform.Translate(InputManager.MainJoystick());
    }
}
