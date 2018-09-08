using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextRoundButtonScript : MonoBehaviour {

    public GameObject resultMenu;
    public TextMeshProUGUI nextButtonText;
    private AudioManager audioManager;

    private InGameGUI inGameGUI;
    private LevelRestart levelRestart;
    private RulesScript rulesScript;
    private OverlayMethodenScript overlayMethodenScript;


    void Start()
    {
        inGameGUI = FindObjectOfType<InGameGUI>();
        rulesScript = FindObjectOfType<RulesScript>();
        levelRestart = FindObjectOfType<LevelRestart>();
        audioManager = FindObjectOfType<AudioManager>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
    }

    public void onClickNextButton()
    {
        audioManager.playSound("buttonclick");
        Cursor.visible = false;
        levelRestart.levelRestartNextRound();
        overlayMethodenScript.isInGame = true;

        if (nextButtonText.text == "Next Battle")
        {
            rulesScript.restartResults();
            inGameGUI.updateInGameGUIMultiplayer();
        }
        
        resultMenu.SetActive(false);

    }

}
