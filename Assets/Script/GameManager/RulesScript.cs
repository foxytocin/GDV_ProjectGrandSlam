using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RulesScript : MonoBehaviour
{

    //Speichert die Player die am Leben sind in der Runde
    public int playerIsLive;
    //Speichert die Playeranzahl für die 3 Runden
    public int battle;
    bool[] playerLifeBool;
    public int[] roundResults;
    public GameObject resultScreen;
    public GameObject winner;
    public GameObject reach;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI reachText;
    public TextMeshProUGUI extraText;
    public TextMeshProUGUI nextButtonText;
    public GameObject nextRoundButton;
    public int record;

    InGameGUI inGameGUI;

    private GameManager gameManager;
    private LevelGenerator LevelGenerator;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        inGameGUI = FindObjectOfType<InGameGUI>();
        LevelGenerator = FindObjectOfType<LevelGenerator>();

        playerIsLive = 1;
        battle = 1;
        record = 0;

        roundResults = new int[4];

        for (int i = 0; i < 4; i++)
            roundResults[i] = 0;

        playerLifeBool = new bool[4];

    }

    public void setPlayerZahl(int players)
    {
        battle = players;
        playerIsLive = battle;
        setPlayerLiveBool(battle);

    }

    public void playerDeath(int player, Vector3 distanze)
    {
        playerIsLive--;
        roundResult(player, (int)distanze.z);
    }

    void roundResult(int player, int distanze)
    {
        switch (battle)
        {
            case 1:
                onePlayerRule(distanze);
                break;
            case 2:
            case 3:
            case 4:

                playerLifeBool[player] = false;

                if (playerIsLive < 2)
                {
                    gameManager.lockControlls();
                    int winnerNumber = searchWinner();
                    FindObjectOfType<OverlayMethodenScript>().isInGame = false;
                    roundResults[winnerNumber]++;
                    inGameGUI.updateInGameGUIMultiplayer();
                    //Debug.LogWarning(roundResults[winnerNumber]);
                    FindObjectOfType<PlayerSpawner>().playerList[winnerNumber].GetComponent<PlayerScript>().winAnimationStart();

                    if (!endResult())
                    {
                        resultScreen.SetActive(true);
                        Cursor.visible = true;
                        winnerText.SetText("The Winner of this Round is:");
                        winner.SetActive(true);
                        reachText.SetText(string.Format("{0} {1}", "Player", (winnerNumber+1).ToString()));
                        reach.SetActive(true);
                        nextRoundButton.SetActive(true);
                        nextButtonText.SetText("Next Round");
                        Cursor.visible = true;
                        
                    }
                    else
                    {
                        resultScreen.SetActive(true);
                        Cursor.visible = true;
                        winnerText.SetText("The Winner of this Battle is:");
                        winner.SetActive(true);
                        reachText.SetText(string.Format("{0} {1}", "Player", (winnerNumber+1).ToString()));
                        reach.SetActive(true);
                        nextButtonText.SetText("Next Battle");
                        Cursor.visible = true;
                        
                    }

                }

                break;

            default:
                break;
        }

    }

    bool endResult()
    {
        for(int i = 0; i < 4; i++)
        {
            if (roundResults[i] == 3)
            {
                return true;
            }
            
        } 

        return false;
    }

    public void restartResults()
    {
        for (int i = 0; i < 4; i++)
        {
            roundResults[i] = 0;
        }

        record = 0;
           
    }

    public void nextRoundRules()
    {
        playerIsLive = battle;
        setPlayerLiveBool(battle);
    }

    private void setPlayerLiveBool(int playerAnzahl)
    {
        for (int i = 0; i < playerAnzahl; i++)
            playerLifeBool[i] = true;
    }

    private int searchWinner()
    {
        for(int i = 0; i < battle; i++)
        {
            if(playerLifeBool[i] == true)
            {
                return i;
            }
        }
        Debug.LogWarning("Im Search Winner ist ein Fehler aufgetaucht");
        return 0;
       
    }

    private void onePlayerRule(int distanze)
    {

        distanze = distanze - (LevelGenerator.startLinie - 1);

        // Verhinderte negative Werte falls der Spieler hinter der Zielline stirbt
        if(distanze < 0)
        {
            distanze = 0;
        }

        // Setzt den Record der zurueckgelegten Strecke auf das neue Maximum
        if(distanze > record)
        {
            record = distanze;
        }

        gameManager.lockControlls();
        
        int reachTextString = distanze;
        inGameGUI.inAktivInGameUI();
        FindObjectOfType<OverlayMethodenScript>().isInGame = false;
        resultScreen.SetActive(true);
        winnerText.SetText("Du bist im Spiel so weit gekommen:");
        winner.SetActive(true);
        reachText.SetText(reachTextString.ToString() + " Meter");
        reach.SetActive(true);
        Cursor.visible = true;
    }



}
