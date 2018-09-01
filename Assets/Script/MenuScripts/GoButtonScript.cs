using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonScript : MonoBehaviour
{
    SpawnDemoItems spawnDemoItems;
    OverlayMethodenScript overlayMethodenScript;
    AudioManager audioManager;
    GameManager gameManager;

    public GameObject mainMenuUI;

    void Start ()
    {
        audioManager = FindObjectOfType<AudioManager>();
        spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void onClickGoButton()
    {
        mainMenuUI.GetComponent<GroupFadeScript>().fadeOut();
        Cursor.visible = false;
        spawnDemoItems.cleanDemoItems();
        audioManager.playSound("lets_go");
        audioManager.stopMenuMusic();
        audioManager.startInGameMusic();
        overlayMethodenScript.isInGame = true;
        gameManager.unlockControlls();

    }

}
