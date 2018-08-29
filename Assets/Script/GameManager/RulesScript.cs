using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RulesScript : MonoBehaviour
{

    bool[] playerIsLive;
    int player;

    int[] roundResults;

    public GameObject resultScreen;
    public GameObject winner;
    public GameObject reach;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI reachText;
    public TextMeshProUGUI extraText;
    public GameObject nextRoundButton;

    private void Awake()
    {
        playerIsLive = new bool[4];

        roundResults = new int[4];

        for (int i = 0; i < 4; i++)
            roundResults[i] = 0;
    }

    public void onePlayer()
    {
        player = 1;

        for(int i = 0; i < 4; i++)
        {
            if (i == 0)
                playerIsLive[i] = true;
            else
                playerIsLive[i] = false;
        }
    }

    public void twoPlayer()
    {
        player = 2;

        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
                playerIsLive[i] = true;
            else
                playerIsLive[i] = false;
        }
    }

    public void threePlayer()
    {
        player = 3;

        for (int i = 0; i < 4; i++)
        {
            if (i < 3)
                playerIsLive[i] = true;
            else
                playerIsLive[i] = false;
        }
    }

    public void fourPlayer()
    {
        player = 4;

        for (int i = 0; i < 4; i++)
        {    
            playerIsLive[i] = true;
        }
    }

    public void playerDeath(int player, Vector3 width)
    {
        playerIsLive[player] = false;
        roundResult(width.z);
    }

    void roundResult(float width)
    {
        switch (player)
        {
            case 1:

                //Time.timeScale = 0f;
                int reachTextString = (int)width;
                FindObjectOfType<PausenMenuScript>().isInGame = false;
                resultScreen.SetActive(true);
                winnerText.SetText("Du bist im Spiel so weit gekommen:");
                winner.SetActive(true);
                reachText.SetText(reachTextString.ToString());
                reach.SetActive(true);
                Debug.LogWarning("Player1 ist nun auch tot und ist " + (int)width +" weit gekommen");

                break;
            case 2:
            case 3:
            case 4:

                int playertmp = 0;
                int lastplayer = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (playerIsLive[i] == true)
                    {
                        playertmp++;
                        lastplayer = i;
                    }
                }

                if (playertmp < 2)
                {
                    roundResults[lastplayer]++;
                    lastplayer++;
                    //Time.timeScale = 0f;
                    FindObjectOfType<PausenMenuScript>().isInGame = false;
                    resultScreen.SetActive(true);
                    winnerText.SetText("The Winner of this Round is:");
                    winner.SetActive(true);
                    reachText.SetText(string.Format("{0} {1}", "player", lastplayer.ToString()));
                    reach.SetActive(true);
                    nextRoundButton.SetActive(true);

                    Debug.LogWarning("Es gibt ein Gewinner: " + lastplayer+1 + " und ist " + (int)width + " weit gekommen");
                }

                break;

            default:
                break;
        }

        endResult();
    }

    void endResult()
    {
        for(int i = 0; i < 4; i++)
        {
            if (roundResults[i] == 3)
            {
                Debug.LogWarning("Drei Runden wurden gewonnen von: Player " + i + 1 + " !");
                Time.timeScale = 0f;
            }
            
        }
    }

}
