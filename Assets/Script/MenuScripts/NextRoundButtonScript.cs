using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextRoundButtonScript : MonoBehaviour {

    public GameObject resultMenu;
    public TextMeshProUGUI nextButtonText;
    private LevelRestart levelRestart;
    private RulesScript rulesScript;

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

        if (nextButtonText.text == "Next Battle")
        {
            Debug.LogWarning("Reset Results");
            rulesScript.restartResults();
        }
        
        resultMenu.SetActive(false);

    }

}
