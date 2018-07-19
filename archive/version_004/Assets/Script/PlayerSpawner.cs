using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Speicher des Player Prefabs
    public GameObject playerPrefab;
    // Anzahl der Players 
    public int players;
    private int startplayers;
    //PlayerListe mit GameObjekts
    public List<GameObject> playerList;

    // Daten die weitergereicht werden (LevelGenerator, ghostSpawner)
    public LevelGenerator levelGenerator;
    public GhostSpawnerScript ghostSpawner;
    List<Vector3> spawnList;
    List<Color> playerColorList;

    void Start()
    {
        spawnList = new List<Vector3>();
        playerColorList = new List<Color>();

        startplayers = 1;
        players = startplayers;
        createPlayers();
    }

    public void createPlayers()
    {
        foreach(GameObject go in playerList)
        {
            Destroy(go);
        }

        spawnList.Clear();

        //SpawnListe enthaelt die Startpositionen der Player 1-4 
        spawnList.Add(new Vector3(6f, 0.4f, 2f));   // Player 1
        spawnList.Add(new Vector3(9f, 0.4f, 2f));   // Player 2
        spawnList.Add(new Vector3(12f, 0.4f, 2f));  // Player 3
        spawnList.Add(new Vector3(15f, 0.4f, 2f));  // Player 4

        // PlayerList wird Initalisiert
        playerList = new List<GameObject>();

        // ColorList enthälten die SpawnFarben der Player 1-4
        playerColorList = new List<Color>();
        playerColorList.Add(new Color(1, 0, 0));    // Player 1 (Red)
        playerColorList.Add(new Color(0, 1, 0));    // Player 2 (Green)
        playerColorList.Add(new Color(0, 0, 1));    // Player 3 (Blue)
        playerColorList.Add(new Color(0, 0, 0));    // Player 4 (Black)

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
            tmpPlayer.GetComponent<Renderer>().material.color = playerColorList[i];

            // Zwischenspeichern von dem PlayerSript des tmpPlayers, zum bearbeiten
            PlayerScript tmpPlayerScript = tmpPlayer.GetComponent<PlayerScript>();
            // Player bekommt seine einzigartige ID
            tmpPlayerScript.setPlayerID(i);
            // Player bekommt seine die PlayerListe mit Ueberreicht
            tmpPlayerScript.setPlayerList(playerList);
            // Player bekommt den LevelGenerator Ueberreicht damit der Player auf das AllGameObject-Array zugreifen kann
            tmpPlayerScript.setWorld(levelGenerator);
            // Player bekommt den GhostSpawner Uebergeben, damit beim Tod ein Geist Spawnen kann
            tmpPlayerScript.ghostSpawner = ghostSpawner;

            // Player wird in die PlayerListe angefuegt
            playerList.Add(tmpPlayer);

        }
    }
  
    // Anzahl der Player, kann dann durch das Menue uebergeben werden durch aufruf der Methode 
    public void setPlayers (int players)
    {
        this.players = players;
    }
}
