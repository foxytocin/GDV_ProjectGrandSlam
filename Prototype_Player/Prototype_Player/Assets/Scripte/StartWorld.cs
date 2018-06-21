using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWorld: MonoBehaviour {

    List<PlayerScript> playerList;
    List<bool> playerIsActiv;
    int playerID;
    public int players = 0;
    Vector3 spawnPosition;
    List<Color> playerColorList;

	// Use this for initialization
	void Start ()
    {
        playerID = 0;
        playerList = new List<PlayerScript>();
        playerIsActiv = new List<bool>();
        spawnPosition = new Vector3(-2, 0, 0);
        playerColorList = new List<Color>();
        playerColorList.Add(new Color(1, 0, 0));
        playerColorList.Add(new Color(0, 1, 0));
        playerColorList.Add(new Color(0, 0, 1));
        playerColorList.Add(new Color(0, 0, 0));


        for (int j = 0; j<players;j++)
        {
            //Player Generator
            PlayerScript player = new PlayerScript(playerID, spawnPosition, playerColorList[j]);
            playerList.Add(player);
            Debug.Log("Player_" + playerID.ToString() +" erstellt!");
            playerID++;
            spawnPosition.x += 2;
            playerIsActiv.Add(true);

        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i < playerID; i++)
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
