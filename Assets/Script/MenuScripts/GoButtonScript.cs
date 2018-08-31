using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonScript : MonoBehaviour
{
    SpawnDemoItems spawnDemoItems;

    OverlayMethodenScript overlayMethodenScript;

    GameManager gameManager;

    CameraScroller cameraScroller;
    DestroyScroller destroyScroller;

    public GameObject mainMenuUI;

    void Start ()
    {

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
        FindObjectOfType<AudioManager>().playSound("lets_go");
        FindObjectOfType<OverlayMethodenScript>().isInGame = true;
        cameraScroller.gameStatePlay = true;
        destroyScroller.gameStatePlay = true;
        gameManager.unlockControlls();

    }

}
