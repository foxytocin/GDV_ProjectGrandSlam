using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonScript : MonoBehaviour
{
    SpawnDemoItems spawnDemoItems;
    OverlayMethodenScript overlayMethodenScript;
    AudioManager audioManager;
    GameManager gameManager;

    InGameGUI inGameGUI;
    CounterScript counterScript;

    public GameObject mainMenuUI;

    void Start ()
    {
        counterScript = FindObjectOfType<CounterScript>();
        inGameGUI = FindObjectOfType<InGameGUI>();
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
        audioManager.stopMenuMusic();
        audioManager.startInGameMusic();
        overlayMethodenScript.isInGame = true;

        inGameGUI.startGUI(gameManager.player);
        inGameGUI.updateInGameGUIMultiplayer();

        counterScript.startCounter();
    }

}
