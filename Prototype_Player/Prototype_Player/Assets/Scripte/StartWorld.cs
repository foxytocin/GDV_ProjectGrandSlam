using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWorld: MonoBehaviour {

    List<PlayerScript> playerList;
    List<bool> playerIsActiv;
    int playerID;

	// Use this for initialization
	void Start () {
        playerID = 0;
        playerList = new List<PlayerScript>();
        playerIsActiv = new List<bool>();

        //Player One
        PlayerScript playerOne = new PlayerScript(playerID);
        playerList.Add(playerOne);
        Debug.Log("Player_1 erstellt!");
        playerID++;
        playerIsActiv.Add(true);

        // Player Two
        PlayerScript playerTwo = new PlayerScript(playerID);
        playerList.Add(playerTwo);
        Debug.Log("Player_2 erstellt!");
        playerID++;
        playerIsActiv.Add(true);

    }
	
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i <= playerID; i++)
        playerList[i].move();

        if (InputManager.OneXButton() && playerIsActiv[0])
            playerList[0].setBomb();

        if (InputManager.TwoXButton() && playerIsActiv[1])
            playerList[1].setBomb();

        if (InputManager.ThreeXButton() && playerIsActiv[2])
            playerList[2].setBomb();

        if (InputManager.FourXButton() && playerIsActiv[3])
            playerList[4].setBomb();
    }
}
