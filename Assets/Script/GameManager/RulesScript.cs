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

    int[] roundResults;

    public GameObject resultScreen;
    public GameObject winner;
    public GameObject reach;

    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI reachText;
    public TextMeshProUGUI extraText;

    public GameObject nextRoundButton;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        playerIsLive = 1;
        battle = 1;

        roundResults = new int[4];

        for (int i = 0; i < 4; i++)
            roundResults[i] = 0;

    }

    public void setPlayerZahl(int players)
    {
        battle = players;
        playerIsLive = battle;

    }

    public void playerDeath(int player, Vector3 distanze)
    {
        playerIsLive--;
        roundResult(player, distanze.z);
    }

    void roundResult(int player, float distanze)
    {
        switch (battle)
        {
            case 1:
                onePlayerRule(distanze);
                break;
            case 2:
            case 3:
            case 4:

                playerIsLive--;

                if (playerIsLive < 2)
                {
                    FindObjectOfType<OverlayMethodenScript>().isInGame = false;
                    gameManager.lockControlls();
                    roundResults[player]++;
                    FindObjectOfType<PlayerSpawner>().playerList[player].GetComponent<PlayerScript>().winAnimationStart();

                    if (!endResult())
                    {
                        resultScreen.SetActive(true);
                        winnerText.SetText("The Winner of this Round is:");
                        winner.SetActive(true);
                        reachText.SetText(string.Format("{0} {1}", "player", player.ToString()));
                        reach.SetActive(true);
                        nextRoundButton.SetActive(true);
                    }
                    else
                    {
                        resultScreen.SetActive(true);
                        winnerText.SetText("The Winner of this Battle is:");
                        winner.SetActive(true);
                        reachText.SetText(string.Format("{0} {1}", "player", player.ToString()));
                        reach.SetActive(true);
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
            roundResults[i] = 0;
    }

    public void nextRoundRules()
    {
        playerIsLive = battle;
    }

    private void onePlayerRule(float distanze)
    {
        int reachTextString = (int)distanze;
        FindObjectOfType<OverlayMethodenScript>().isInGame = false;
        resultScreen.SetActive(true);
        winnerText.SetText("Du bist im Spiel so weit gekommen:");
        winner.SetActive(true);
        reachText.SetText(reachTextString.ToString());
        reach.SetActive(true);
    }



}
