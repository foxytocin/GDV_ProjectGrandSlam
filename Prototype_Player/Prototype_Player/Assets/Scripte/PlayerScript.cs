using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Controller controller;
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
        controller = new Controller(playerID, body);
        body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Player_" + playerID.ToString();
        life = 3;
        avaibleBombs = 1;
        speed = 1;
        range = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
