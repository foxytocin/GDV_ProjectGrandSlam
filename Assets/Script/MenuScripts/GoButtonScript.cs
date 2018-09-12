using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonScript : MonoBehaviour
{
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
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void onClickGoButton()
    {
        audioManager.playSound("buttonclick");
        mainMenuUI.GetComponent<GroupFadeScript>().fadeOut();
        Cursor.visible = false;
        audioManager.stopMenuMusic();
        audioManager.playNextSong();
        overlayMethodenScript.isInGame = true;
        inGameGUI.startGUI(gameManager.player);
        inGameGUI.updateInGameGUIMultiplayer();
        counterScript.startCounter();
    }

}
