using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoundButtonScript : MonoBehaviour {

    public GameObject resultMenu;

    GameManager gameManager;
    LevelRestart levelRestart;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelRestart = FindObjectOfType<LevelRestart>();
    }

    public void onClickNextButton()
    {
        levelRestart.levelRestart();
        resultMenu.SetActive(false);
        FindObjectOfType<OverlayMethodenScript>().isInGame = true;
        gameManager.unlockControlls();

    }
}
