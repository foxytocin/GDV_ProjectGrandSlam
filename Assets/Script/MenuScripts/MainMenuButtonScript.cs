using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public GameObject mainMenuUI;
    public GameObject offSide;
    AudioManager audioManager;
    LevelRestart levelRestart;
    InGameGUI inGameGUI;
    private MenuDemoMode MenuDemoMode;
    private OverlayMethodenScript overlayMethodenScript;
    
	void Start ()
    {
        inGameGUI = FindObjectOfType<InGameGUI>();
        audioManager = FindObjectOfType<AudioManager>();
        levelRestart = FindObjectOfType<LevelRestart>();
        MenuDemoMode = FindObjectOfType<MenuDemoMode>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
    }
	
    public void onClickMainButton()
    {
        AudioListener.volume = 1f;
        audioManager.playSound("buttonclick");
        overlayMethodenScript.isInGame = false;
        levelRestart.levelRestartMainMenu();
        Time.timeScale = 1f;
        inGameGUI.inAktivInGameUI();
        mainMenuUI.GetComponent<GroupFadeScript>().fadeIn();
        audioManager.stopInGameMusic();
        audioManager.playSound("menumusic");
        MenuDemoMode.demoAllowed = true;
        offSide.SetActive(false);
    }
}
