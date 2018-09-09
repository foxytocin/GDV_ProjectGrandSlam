using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayMethodenScript : MonoBehaviour
{
    public bool GameIsPaused;
    public GameObject pausenMenuUI;
    public GameManager gameManager;
    private AudioManager audioManager;
    private AudioListener audioListener;
    private CameraScroller cameraScroller;


    InGameGUI inGameGUI;

    public bool isInGame;

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
        pausenMenuUI.SetActive(false);
        AudioListener.volume = 1f;
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
        AudioListener.volume = 0f;
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
}