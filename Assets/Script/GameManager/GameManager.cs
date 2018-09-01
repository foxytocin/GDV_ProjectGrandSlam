using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private PlayerSpawner playerSpawner;
    private Camera miniMapCam;
    private Canvas miniMapCanvas;
    private bool showMiniMap;
    public int player;
    private int playertmp;
    public bool isInGame;

    void Awake()
    {
        player = 1;
        playertmp = player;
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        miniMapCam = GameObject.Find("MiniMapCam").GetComponent<Camera>();
        miniMapCanvas = GameObject.Find("MiniMapCanvas").GetComponent<Canvas>();
        
    }

    void Start()
    {
        showMiniMap = false;
        miniMapCam.enabled = false;
        miniMapCanvas.enabled = false;
        isInGame = false;
    }

    // Update is called once per frame
    void Update ()
    {

        if(Input.GetKeyDown("c"))
        {
            if(showMiniMap)
            {
                miniMapCanvas.enabled = false;
                miniMapCam.enabled = false;
                showMiniMap = false;
            } else {
                miniMapCam.enabled = true;
                miniMapCanvas.enabled = true;
                showMiniMap = true;
            }
        }

        if(player != playertmp) {
            playertmp = player;
            playerSpawner.setPlayers(player);
            playerSpawner.createPlayers();            

            switch (player)
            {
                case 1: FindObjectOfType<AudioManager>().playSound("one"); break;
                case 2: FindObjectOfType<AudioManager>().playSound("two"); break;
                case 3: FindObjectOfType<AudioManager>().playSound("three"); break;
                case 4: FindObjectOfType<AudioManager>().playSound("four"); break;
                default: break;
            }
        }

    }

    public void unlockControlls()
    {
        List <GameObject> playerList = playerSpawner.playerList;
        foreach (GameObject go in playerList)
        {
            Debug.Log("UNLOCK: " +go.gameObject);
            if (go != null)
                go.GetComponent<PlayerScript>().gameStatePlay = true;
        }
    }

    public void lockControlls()
    {
        List<GameObject> playerList = playerSpawner.playerList;
        foreach (GameObject go in playerList)
        {
            if (go != null)
                go.GetComponent<PlayerScript>().gameStatePlay = false;
        }
    }

}