using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public GameObject mainMenuUI;
    public GameObject offSide;

    LevelRestart levelRestart;
    OverlayMethodenScript overlayMethodenScript;

	void Start ()
    {
        levelRestart = FindObjectOfType<LevelRestart>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
    }
	
    public void onClickMainButton()
    {
        offSide.SetActive(false);
        mainMenuUI.GetComponent<GroupFadeScript>().fadeIn();
        levelRestart.levelRestart();
    }
}
