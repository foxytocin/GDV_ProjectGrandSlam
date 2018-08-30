﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Speicher des Player Prefabs
    public GameObject playerPrefab;
    // Anzahl der Players 
    public int players;
    //PlayerListe mit GameObjekts
    public List<GameObject> playerList;

    List<Vector3> spawnList;
    List<Color> playerColorList;
    public CameraMovement cam;

    public Material playerOne;

    void Awake()
    {
        spawnList = new List<Vector3>();
        playerColorList = new List<Color>();

        //Standardmäßig wird beim Laden der Scene ein Spieler erstellt
        players = 1;
        createPlayers();
    }

    public void createPlayers()
    {
        //Wird createPlayers() vom GameManager aufrufen werden zuerst alle Player in der playerList zerstoert und entfernt.
        foreach(GameObject go in playerList)
        {
            if(go != null)
                Destroy(go);
        }

        //Bereinigt die beiden Listen bevor sie neu gefuellt werden.
        playerList.Clear();
        spawnList.Clear();

        //SpawnListe enthaelt die Startpositionen der Player 1-4 
        spawnList.Add(new Vector3(6f, 0.45f, 2f));   // Player 1
        spawnList.Add(new Vector3(12f, 0.45f, 2f));   // Player 2
        spawnList.Add(new Vector3(18f, 0.45f, 2f));  // Player 3
        spawnList.Add(new Vector3(24f, 0.45f, 2f));  // Player 4

        // PlayerList wird Initalisiert
        playerList = new List<GameObject>();

        // ColorList enthälten die SpawnFarben der Player 1-4
        playerColorList = new List<Color>();
        playerColorList.Add(new Color32(236, 77, 19, 255));     // Player 1 (Red)
        playerColorList.Add(new Color32(82, 203, 16, 255));     // Player 2 (Green)
        playerColorList.Add(new Color32(17, 170, 212, 255));    // Player 3 (Blue)
        playerColorList.Add(new Color32(226, 195, 18, 255));    // Player 4 (Yellow)

        // for-Schleife zum erstellen der Player abhaengig von dem Datenfeld players
        for (int i = 0; i < players; i++)
        {
            // Clonen von einem PlayerPrefab in tmpPlayer 
            GameObject tmpPlayer = Instantiate(playerPrefab, spawnList[i], Quaternion.identity);

            // Der Player bekommt seinen Namen
            tmpPlayer.name = "Player_" + i.ToString();
            // Der Player bekommt sein Tag
            tmpPlayer.tag = "Player";
            // Der Player bekommt seine einzigartige Farbe
            tmpPlayer.GetComponent<Renderer>().material = playerOne;
            tmpPlayer.GetComponent<Renderer>().material.color = playerColorList[i];
            tmpPlayer.GetComponent<Light>().color = playerColorList[i];

            // Zwischenspeichern von dem PlayerSript des tmpPlayers, zum bearbeiten
            PlayerScript tmpPlayerScript = tmpPlayer.GetComponent<PlayerScript>();
            // Player bekommt seine einzigartige ID
            tmpPlayerScript.playerID = i;

            // Player wird in die PlayerListe angefuegt
            playerList.Add(tmpPlayer);
        }        
    }
  
    // Anzahl der Player, kann dann durch das Menue uebergeben werden durch aufruf der Methode 
    public void setPlayers (int players)
    {
        this.players = players;
    }

    public void setPlayersDropdown(int players)
    {
        this.players = players + 1;
    }
}
