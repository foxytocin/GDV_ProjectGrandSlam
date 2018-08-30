using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public GameObject mainMenuUI;
    public GameObject offSide;
    private SpawnDemoItems spawnDemoItems;

    LevelRestart levelRestart;
    OverlayMethodenScript overlayMethodenScript;


	void Start ()
    {
        levelRestart = FindObjectOfType<LevelRestart>();
        spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
    }
	
    public void onClickMainButton()
    {
        Time.timeScale = 1f;
        mainMenuUI.GetComponent<GroupFadeScript>().fadeIn();
        StartCoroutine(levelRestart.levelRestartMainMenu());
        offSide.SetActive(false);
    }
}
