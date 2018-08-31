using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextRoundButtonScript : MonoBehaviour {

    public GameObject resultMenu;
    public TextMeshProUGUI nextButtonText;
    GameManager gameManager;
    LevelRestart levelRestart;
    RulesScript rulesScript;

    void Start()
    {
        rulesScript = FindObjectOfType<RulesScript>();
        levelRestart = FindObjectOfType<LevelRestart>();
    }

    public void onClickNextButton()
    {
        Cursor.visible = false;
        levelRestart.levelRestartNextRound();
        FindObjectOfType<OverlayMethodenScript>().isInGame = true;
        resultMenu.SetActive(false);

        // Wird niemals erreicht weil SetActive(false) das Script beendet!
        if (nextButtonText.text == "Next Battle")
        {
            Debug.LogWarning("Reset Results");
            rulesScript.restartResults();
        }

    }

}
