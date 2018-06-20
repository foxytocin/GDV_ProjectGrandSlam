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

        //Player One
        PlayerScript playerOne = new PlayerScript(playerID);
        playerList.Add(playerOne);
        Debug.Log("Player_1 erstellt!");
        playerID++;

        // Player Two
        PlayerScript playerTwo = new PlayerScript(playerID);
        playerList.Add(playerTwo);
        Debug.Log("Player_2 erstellt!");
        playerID++;

    }
	
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i <= playerID; i++)
        playerList[i].move();

        if(InputManager.OneXButton())
            Debug.Log("Achtung_Bombe_von Player_1");

        if (InputManager.TwoXButton())
            Debug.Log("Achtung_Bombe_von Player_2");

        if (InputManager.ThreeXButton())
            Debug.Log("Achtung_Bombe_von Player_3");

        if (InputManager.FourXButton())
            Debug.Log("Achtung_Bombe_von Player_4");
    }
}
