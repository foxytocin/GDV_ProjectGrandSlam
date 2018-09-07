using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameGUI : MonoBehaviour {

    RulesScript rulesScript;
    GameManager gameManager;

    public GameObject inGameCanvasUI;

    public GameObject singleplayerCanvas;
    public GameObject multiPlayerCanvas;

    public TextMeshProUGUI singlePlayerText;

    public TextMeshProUGUI player1Win;
    public TextMeshProUGUI player2Win;
    public TextMeshProUGUI player3Win;
    public TextMeshProUGUI player4Win;

    PlayerSpawner playerSpawner;
    bool singlePlayerActive;
    private int distance;
    GameObject playerOne;
    LevelGenerator levelGenerator;
    

	void Awake ()
    {
        singlePlayerActive = false;
        gameManager = FindObjectOfType<GameManager>();
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        rulesScript = FindObjectOfType<RulesScript>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
	}

    private void Update()
    {
        if(singlePlayerActive && gameManager.gameStatePlay)
        {
            distance = (int)(playerOne.transform.position.z - (levelGenerator.startLinie - 1));

            if(distance < 0)
            {
                distance = 0;
            }

            singlePlayerText.SetText(distance.ToString()+ " Meter");
        }
    }

    public void startGUI(int player)
    {
        switch(player)
        {
            case 1:

                multiPlayerCanvas.SetActive(false);
                singlePlayerText.SetText("0 Meter");
                aktivInGameUI();
                singlePlayerActive = true;
                restartSinglePlayerGui();
                singleplayerCanvas.SetActive(true);
                
                break;

            case 2:
                singlePlayerActive = false;
                singleplayerCanvas.SetActive(false);
                multiPlayerCanvas.SetActive(true);
                player1Win.color = new Color32(236, 77, 19, 255);
                player2Win.color = new Color32(82, 203, 16, 255);
                player3Win.color = new Color32(150,150,150,255);
                player4Win.color = new Color32(150, 150, 150, 255);
                aktivInGameUI();
                break;

            case 3:
                singlePlayerActive = false;
                singleplayerCanvas.SetActive(false);
                multiPlayerCanvas.SetActive(true);
                player1Win.color = new Color32(236, 77, 19, 255);
                player2Win.color = new Color32(82, 203, 16, 255);
                player3Win.color = new Color32(17, 170, 212, 255);
                player4Win.color = new Color32(150, 150, 150, 255);
                aktivInGameUI();
                break;

            case 4:
                singlePlayerActive = false;
                singleplayerCanvas.SetActive(false);
                multiPlayerCanvas.SetActive(true);
                player1Win.color = new Color32(236, 77, 19, 255);
                player2Win.color = new Color32(82, 203, 16, 255);
                player3Win.color = new Color32(17, 170, 212, 255);
                player4Win.color = new Color32(226, 195, 18, 255);
                aktivInGameUI();
                break;

            default:
                Debug.Log("Verklemmung hier!");
                break;



        }
    }

    public void updateInGameGUIMultiplayer()
    {

        player1Win.SetText(rulesScript.roundResults[0].ToString());
        player2Win.SetText(rulesScript.roundResults[1].ToString());
        player3Win.SetText(rulesScript.roundResults[2].ToString());
        player4Win.SetText(rulesScript.roundResults[3].ToString());

    }

    public void aktivInGameUI()
    {
        inGameCanvasUI.SetActive(true);
    }

    public void inAktivInGameUI()
    {
        inGameCanvasUI.SetActive(false);
    }

    public void restartSinglePlayerGui()
    {
        singlePlayerText.SetText("0 Meter");
        playerOne = playerSpawner.playerList[0];
    }
}
