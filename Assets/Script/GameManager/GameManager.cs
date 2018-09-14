using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private PlayerSpawner playerSpawner;
    private AudioManager audioManager;
    private Camera miniMapCam;
    private Canvas miniMapCanvas;
    private LevelGenerator levelGenerator;
    public bool gameStatePlay;
    private bool showMiniMap;
    public int player;
    public int playertmp;
    public bool isInGame;
    public int controller;

    void Awake()
    {
        player = 1;
        playertmp = player;
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        audioManager = FindObjectOfType<AudioManager>();
        levelGenerator = FindObjectOfType<LevelGenerator>();

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
                case 1: audioManager.playSound("one"); break;
                case 2: audioManager.playSound("two"); break;
                case 3: audioManager.playSound("three"); break;
                case 4: audioManager.playSound("four"); break;
                default: break;
            }
        }

        // Wird das mehr als einem Player gestartet, werden die Enemys aus dem Startscreen aus dem Level entfernt
        if(player > 1 && levelGenerator.allowEnemies && gameStatePlay)
        {
            levelGenerator.allowEnemies = false;
            levelGenerator.generateEnemies = false;
            
            GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");

		    foreach (GameObject go in list)
            {
                if(go != null)
                {
                    Destroy(go);
                }
            }
        }
        
        // Blendet die Enemys im Startscreen aus wenn mehr als ein Player ausgewählt ist
        // Blendet die Enemys wieder ein, wenn Singleplayer ausgewält wird
        if(player > 1 && levelGenerator.allowEnemies && !gameStatePlay) {

            GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
		    foreach (GameObject go in list)
            {
                if(go != null)
                {
                    go.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        } else if(player == 1 && levelGenerator.allowEnemies && !gameStatePlay)
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
		    foreach (GameObject go in list)
            {
                if(go != null)
                {
                    go.GetComponent<MeshRenderer>().enabled = true;
                }
            }
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