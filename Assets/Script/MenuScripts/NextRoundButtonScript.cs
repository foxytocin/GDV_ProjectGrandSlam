using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoundButtonScript : MonoBehaviour {

    public GameObject resultMenu;

    RulesScript rulesScript;
    GameManager gameManager;
    LevelRestart levelRestart;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelRestart = FindObjectOfType<LevelRestart>();
        rulesScript = FindObjectOfType<RulesScript>();
    }

    public void onClickNextButton()
    {
        StartCoroutine(restart());
    }

    private IEnumerator restart()
    {
        levelRestart.levelRestart();

        yield return new WaitForSecondsRealtime(4f);

        FindObjectOfType<OverlayMethodenScript>().isInGame = true;

        rulesScript.nextRoundRules();
        gameManager.unlockControlls();
        resultMenu.SetActive(false);
    }
}
