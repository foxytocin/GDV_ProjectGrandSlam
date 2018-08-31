using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public GameObject mainMenuUI;
    public GameObject offSide;
    private SpawnDemoItems spawnDemoItems;

    AudioManager audioManager;
    LevelRestart levelRestart;
    OverlayMethodenScript overlayMethodenScript;


	void Start ()
    {
        audioManager = FindObjectOfType<AudioManager>();
        levelRestart = FindObjectOfType<LevelRestart>();
        spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
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
