using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerPrefab;
    public int players;
    public List<GameObject> playerList;
    Vector3 spawnPosition;
    List<Color> playerColorList;

	// Use this for initialization
	void Start ()
    {
        spawnPosition = new Vector3(-2, 0, 0);
        playerList = new List<GameObject>();

        playerColorList = new List<Color>();
        playerColorList.Add(new Color(1, 0, 0));
        playerColorList.Add(new Color(0, 1, 0));
        playerColorList.Add(new Color(0, 0, 1));
        playerColorList.Add(new Color(0, 0, 0));

        for (int i = 0; i < players; i++)
        {
            GameObject tmpPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            tmpPlayer.name = "Player_" + i.ToString();
            tmpPlayer.GetComponent<Renderer>().material.color = playerColorList[i];
            playerList.Add(tmpPlayer);
            playerList[i].GetComponent<PlayerScript>().setPlayerID(i);
            playerList[i].GetComponent<PlayerScript>().setPlayerList(playerList);
            spawnPosition.x += 2;
        }
    }
}
