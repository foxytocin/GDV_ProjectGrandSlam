using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoundButtonScript : MonoBehaviour {

    public GameObject resultMenu;

    LevelRestart levelRestart;
    
    void Start()
    {
        levelRestart = FindObjectOfType<LevelRestart>();
    }

    public void onClickNextButton()
    {
        levelRestart.levelRestart();
        resultMenu.SetActive(false);
        FindObjectOfType<OverlayMethodenScript>().isInGame = true;
    }
}
