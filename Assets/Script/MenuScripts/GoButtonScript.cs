using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonScript : MonoBehaviour {

    CanvasGroup canvasGroup;
    Canvas canvas;

    SpawnDemoItems spawnDemoItems;

    OverlayMethodenScript overlayMethodenScript;

    GameManager gameManager;

    CameraScroller cameraScroller;
    DestroyScroller destroyScroller;

    void Start ()
    {

        canvasGroup = FindObjectOfType<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();

        spawnDemoItems = FindObjectOfType<SpawnDemoItems>();

        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();

        gameManager = FindObjectOfType<GameManager>();

        cameraScroller = FindObjectOfType<CameraScroller>();
        destroyScroller = FindObjectOfType<DestroyScroller>();

    }

    public void onClickGoButton()
    {
        overlayMethodenScript.fadeOut(canvasGroup, canvas);
        gameManager.unlockControlls();
        spawnDemoItems.cleanDemoItems();
        FindObjectOfType<AudioManager>().playSound("lets_go");
        FindObjectOfType<OverlayMethodenScript>().isInGame = true;
        cameraScroller.gameStatePlay = true;
        destroyScroller.gameStatePlay = true;

    }
    

}
