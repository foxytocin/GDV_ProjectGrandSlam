using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public GameObject offSide;
    public GameObject onSide;

    LevelRestart levelRestart;
    OverlayMethodenScript overlayMethodenScript;

    public CanvasGroup canvasGroup;
    public Canvas canvas;

	void Start ()
    {
        levelRestart = FindObjectOfType<LevelRestart>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
    }
	
    public void onClickMainButton()
    {
        offSide.SetActive(false);
        onSide.SetActive(true);
        levelRestart.levelRestart();
        //overlayMethodenScript.fadeIn(canvasGroup, canvas);
    }
}
