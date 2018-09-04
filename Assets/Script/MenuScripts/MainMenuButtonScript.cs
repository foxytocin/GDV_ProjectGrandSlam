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

	void Start ()
    {
        inGameGUI = FindObjectOfType<InGameGUI>();
        audioManager = FindObjectOfType<AudioManager>();
        levelRestart = FindObjectOfType<LevelRestart>();
        MenuDemoMode = FindObjectOfType<MenuDemoMode>();
    }
	
    public void onClickMainButton()
    {
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
