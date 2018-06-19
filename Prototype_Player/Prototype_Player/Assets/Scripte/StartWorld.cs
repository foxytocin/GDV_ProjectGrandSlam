using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWorld: MonoBehaviour {

    List<PlayerScript> playerList;
    int playerID;
	// Use this for initialization
	void Start () {
        playerID = 0;
        playerList = new List<PlayerScript>();
        playerID++;
        PlayerScript playerOne = new PlayerScript(playerID);
        playerList.Add(playerOne);
        /*playerID++;
        Player playerTwo = new Player(playerID);
        playerList.Add(playerTwo);
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
