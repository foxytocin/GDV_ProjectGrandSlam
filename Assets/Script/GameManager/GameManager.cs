using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private PlayerSpawner playerSpawner;
    private InGameGUI inGameGUI;
    private Camera miniMapCam;
    private Canvas miniMapCanvas;
    private CameraScroller cameraScroller;
    private DestroyScroller destroyScroller;
    private bool showMiniMap;
    public int player;
    private int playertmp;
    public bool isInGame;

    void Awake()
    {
        player = 1;
        playertmp = player;
        playerSpawner = FindObjectOfType<PlayerSpawner>();
<<<<<<< HEAD
        inGameGUI = FindObjectOfType<InGameGUI>();
=======
        cameraScroller = FindObjectOfType<CameraScroller>();
        destroyScroller = FindObjectOfType<DestroyScroller>();
>>>>>>> master
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

        if(isInGame && player == 1)
        {
            Debug.Log("if funktioniert");
            inGameGUI.updateInGameUISingleplayer();
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

        cameraScroller.gameStatePlay = true;
        destroyScroller.gameStatePlay = true;
    }

    public void lockControlls()
    {
        cameraScroller.gameStatePlay = false;
        destroyScroller.gameStatePlay = false;

        List<GameObject> playerList = playerSpawner.playerList;
        foreach (GameObject go in playerList)
        {
            if (go != null)
                go.GetComponent<PlayerScript>().gameStatePlay = false;
        }
    }

}