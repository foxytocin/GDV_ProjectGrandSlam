using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public GameObject mainMenuUI;
    public GameObject offSide;
    AudioManager audioManager;
    LevelRestart levelRestart;


	void Start ()
    {
        audioManager = FindObjectOfType<AudioManager>();
        levelRestart = FindObjectOfType<LevelRestart>();
    }
	
    public void onClickMainButton()
    {
        Time.timeScale = 1f;
        mainMenuUI.GetComponent<GroupFadeScript>().fadeIn();
        audioManager.stopInGameMusic();
        audioManager.playSound("menumusic");
        levelRestart.levelRestartMainMenu();
        offSide.SetActive(false);
    }
}
