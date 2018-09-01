using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonScript : MonoBehaviour
{
    SpawnDemoItems spawnDemoItems;

    OverlayMethodenScript overlayMethodenScript;

    AudioManager audioManager;

    GameManager gameManager;

    CameraScroller cameraScroller;
    DestroyScroller destroyScroller;

    InGameGUI inGameGUI;

    public GameObject mainMenuUI;

    void Start ()
    {
        inGameGUI = FindObjectOfType<InGameGUI>();
        audioManager = FindObjectOfType<AudioManager>();
        spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
        gameManager = FindObjectOfType<GameManager>();
        cameraScroller = FindObjectOfType<CameraScroller>();
        destroyScroller = FindObjectOfType<DestroyScroller>();
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
        cameraScroller.gameStatePlay = true;
        destroyScroller.gameStatePlay = true;
        inGameGUI.startGUI(gameManager.player);
        inGameGUI.updateInGameGUIMultiplayer();
        gameManager.unlockControlls();

    }

}
