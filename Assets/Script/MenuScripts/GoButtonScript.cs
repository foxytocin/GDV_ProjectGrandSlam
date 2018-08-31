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

    public GameObject mainMenuUI;

    void Start ()
    {
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
        FindObjectOfType<OverlayMethodenScript>().isInGame = true;
        cameraScroller.gameStatePlay = true;
        destroyScroller.gameStatePlay = true;
        gameManager.unlockControlls();

    }

}
