using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private PlayerSpawner playerSpawner;
    private InGameGUI inGameGUI;
    private Camera miniMapCam;
    private Canvas miniMapCanvas;
    public bool gameStatePlay;
    private bool showMiniMap;
    public int player;
    private int playertmp;
    public bool isInGame;
    public int controller; // 0 = Keinen, 1 = Xbox360, 2 = PS4

    void Awake()
    {
        player = 1;
        playertmp = player;
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        inGameGUI = FindObjectOfType<InGameGUI>();
        miniMapCam = GameObject.Find("MiniMapCam").GetComponent<Camera>();
        miniMapCanvas = GameObject.Find("MiniMapCanvas").GetComponent<Canvas>();
        controller = 0;
        gameStatePlay = false;
        
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
        gameStatePlay = true;
        //Debug.Log("UNLOCKED");
    }

    public void lockControlls()
    {
        gameStatePlay = false;
        //Debug.Log("LOCKED");
    }

}