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
    public int[] distanzRecords;
    public GameObject resultScreen;
    public GameObject winner;
    public GameObject reach;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI reachText;
    public TextMeshProUGUI extraText;
    public TextMeshProUGUI nextButtonText;
    public GameObject nextRoundButton;
    public GameObject recordsTextfield;
    public int record;
    public TextMeshProUGUI recordsText; 

    InGameGUI inGameGUI;

    private GameManager gameManager;
    private LevelGenerator LevelGenerator;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        inGameGUI = FindObjectOfType<InGameGUI>();
        LevelGenerator = FindObjectOfType<LevelGenerator>();
    }

    void Start()
    {
        playerIsLive = 1;
        battle = 1;
        record = 0;

        roundResults = new int[4];

        for (int i = 0; i < 4; i++)
            roundResults[i] = 0;

        distanzRecords = new int[4];
        for (int i = 0; i < 4; i++)
            distanzRecords[i] = 0;

        playerLifeBool = new bool[4];
    }

    public void setPlayerZahl(int players)
    {
        battle = players;
        playerIsLive = battle;
        setPlayerLiveBool(battle);
    }

    public void playerDeath(int player, Vector3 distance)
    {
        playerIsLive--;
        roundResult(player, (int)distance.z);
    }

    void roundResult(int player, int distance)
    {
        switch (battle)
        {
            case 1:
                onePlayerRule(distance);
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
                        recordsTextfield.SetActive(false);
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
                        recordsTextfield.SetActive(false);
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

    private void onePlayerRule(int distance)
    {
        gameManager.lockControlls();
        distance = distance - (LevelGenerator.startLinie - 1);

        // Verhinderte negative Werte falls der Spieler hinter der Zielline stirbt
        if(distance < 0)
        {
            distance = 0;
        }

        // Setzt den Record der zurueckgelegten Strecke auf das neue Maximum
        if(distance > record)
        {
            record = distance;
        }

        recodsOrganize(recordSearch(distance, 4), distance);
        
        int reachTextString = distance;
        FindObjectOfType<OverlayMethodenScript>().isInGame = false;
        resultScreen.SetActive(true);
        winner.SetActive(false);
        //winnerText.SetText("Du bist im Spiel so weit gekommen:");
        //winner.SetActive(true);
        reachText.SetText(reachTextString.ToString() + " Meter");
        reach.SetActive(true);

        recordsText.SetText("The Last Three Records:\n"+
                            "1: " + distanzRecords[0] + " Meter\n" +
                            "2: " + distanzRecords[1] + " Meter\n" +
                            "3: " + distanzRecords[2] + " Meter\n" );
                           

        recordsTextfield.SetActive(true);
        Cursor.visible = true;
    }

    private int recordSearch(int distanz, int index)
    {
        if (distanz > distanzRecords[index -= 1] && index != 0)
            return recordSearch(distanz, index);
        else if (distanz > distanzRecords[index] && index == 0)
            return 0;
        else
            return index += 1;
    }

    private void recodsOrganize(int index, int distance)
    {
        switch(index)
        {
            case 0:
                distanzRecords[3] = distanzRecords[2];
                distanzRecords[2] = distanzRecords[1];
                distanzRecords[1] = distanzRecords[0];
                distanzRecords[0] = distance;
                break;

            case 1:
                distanzRecords[3] = distanzRecords[2];
                distanzRecords[2] = distanzRecords[1];
                distanzRecords[1] = distance;
                break;

            case 2:
                distanzRecords[3] = distanzRecords[2];
                distanzRecords[2] = distance;
                break;

            case 3:
                distanzRecords[3] = distance;
                break;

            case 4:
                Debug.Log("Kein Neuer Rekord");
                break;

            default:
                Debug.LogWarning("Fehler im RecordOrganize");
                break;

        }
    }



}
