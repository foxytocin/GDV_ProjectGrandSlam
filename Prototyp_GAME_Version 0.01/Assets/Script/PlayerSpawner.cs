using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerPrefab;
    public WorldScript world;
    public int players;
    public List<GameObject> playerList;
    public List<Vector3> spawnList;
    Vector3 spawnPosition;
    List<Color> playerColorList;

	// Use this for initialization
	void Start ()
    {
        spawnList.Add(new Vector3(1f, -0.1f, 1f));
        spawnList.Add(new Vector3((world.levelBreite - 1) * 2 - 1, -0.1f, (world.levelTiefe - 1) * 2 - 1));
        playerList = new List<GameObject>();

        playerColorList = new List<Color>();
        playerColorList.Add(new Color(1, 0, 0));
        playerColorList.Add(new Color(0, 1, 0));
        playerColorList.Add(new Color(0, 0, 1));
        playerColorList.Add(new Color(0, 0, 0));

        for (int i = 0; i < players; i++)
        {
            GameObject tmpPlayer = Instantiate(playerPrefab, spawnList[i], Quaternion.identity);
            tmpPlayer.name = "Player_" + i.ToString();
            tmpPlayer.GetComponent<Renderer>().material.color = playerColorList[i];
            playerList.Add(tmpPlayer);
            playerList[i].GetComponent<PlayerScript>().setPlayerID(i);
            playerList[i].GetComponent<PlayerScript>().setPlayerList(playerList);
            playerList[i].GetComponent<PlayerScript>().setWorld(world);
            spawnPosition.x += 2;
        }
    }
}
