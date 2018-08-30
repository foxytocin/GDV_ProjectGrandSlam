using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoundButtonScript : MonoBehaviour {

    public GameObject resultMenu;
<<<<<<< HEAD
=======

    RulesScript rulesScript;
    GameManager gameManager;
>>>>>>> 7560a9515629792e4f73735e71a2d461f130b221
    LevelRestart levelRestart;
    
    void Start()
    {
        levelRestart = FindObjectOfType<LevelRestart>();
        rulesScript = FindObjectOfType<RulesScript>();
    }

    public void onClickNextButton()
    {
        levelRestart.levelRestartNextRound();
        FindObjectOfType<OverlayMethodenScript>().isInGame = true;
<<<<<<< HEAD
=======

        rulesScript.nextRoundRules();
        gameManager.unlockControlls();
>>>>>>> 7560a9515629792e4f73735e71a2d461f130b221
        resultMenu.SetActive(false);
    }

}
