using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayMethodenScript : MonoBehaviour
{
    private bool GameIsPaused;
    public bool isInGame;

    private GameManager gameManager;
    private AudioManager audioManager;
    private AudioListener audioListener;
    private CameraScroller cameraScroller;
    private InGameGUI inGameGUI;

    public GameObject pausenMenuUI;
    public GameObject optionMenu;
    public GameObject pausenMenu;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        inGameGUI = FindObjectOfType<InGameGUI>();
        audioManager = FindObjectOfType<AudioManager>();
        audioListener = FindObjectOfType<AudioListener>();
        cameraScroller = FindObjectOfType<CameraScroller>();
        GameIsPaused = false;
        isInGame = false;
    }

    void Update()
    {
        if (InputManager.OneStartButton() || InputManager.ThreeStartButton() || InputManager.FourStartButton() || InputManager.TwoStartButton() || Input.GetKeyDown(KeyCode.Escape) && isInGame)
        {
            if (GameIsPaused)
            {
                resume();
                Cursor.visible = false;

            } else if (gameManager.gameStatePlay) {
                
                pause();
                Cursor.visible = true;
            }
        }
    }

    public void resume()
    {
        Time.timeScale = 1f;
        StartCoroutine(audioManager.pitchUp());
        audioManager.playSound("buttonclick");
        gameManager.unlockControlls();
        Cursor.visible = false;
        inGameGUI.aktivInGameUI();
        GameIsPaused = false;
        isInGame = true;
        resetPausenMenu();
        pausenMenuUI.SetActive(false);
    }



    public void pause()
    {
        audioManager.playSound("buttonclick");
        StartCoroutine(audioManager.pitchDown());
        gameManager.lockControlls();
        GameIsPaused = true;
        pausenMenuUI.SetActive(true);
        inGameGUI.inAktivInGameUI();
        Time.timeScale = 0f;
        Cursor.visible = true;
        cameraScroller.fadeInSpeed = 0.01f;
        cameraScroller.fadeInAcceleration = 0.02f;
    }

    public void QuitGame()
    {
        audioManager.playSound("buttonclick");
        Application.Quit();
    }

    public void updateSoundOptions(Slider fxSlider, Slider musicSlider)
    {
        fxSlider.value = FindObjectOfType<AudioManager>().settingsFXVolume;
        musicSlider.value = FindObjectOfType<AudioManager>().settingsMusicVolume;
    }

    public void sideSwitch(GameObject offSetSide, GameObject onSetSide)
    {
        onSetSide.SetActive(true);
        offSetSide.SetActive(false);
    }

    private void resetPausenMenu()
    {
        optionMenu.SetActive(false);
        pausenMenu.SetActive(true);
    }
}