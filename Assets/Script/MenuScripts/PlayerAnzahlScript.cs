using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnzahlScript : MonoBehaviour
{
    GameManager gameManager;
    RulesScript rulesScript;

    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;
    public GameObject buttonFour;

    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        rulesScript = FindObjectOfType<RulesScript>();
    }

    public void setOnePlayer()
    {
        gameManager.player = 1;
        rulesScript.setPlayerZahl(1);
    }

    public void setTwoPlayer()
    {
        gameManager.player = 2;
        rulesScript.setPlayerZahl(2);
    }

    public void setThreePlayer()
    {
        gameManager.player = 3;
        rulesScript.setPlayerZahl(3);
    }

    public void setFourPlayer()
    {
        gameManager.player = 4;
        rulesScript.setPlayerZahl(4);
    }


}
