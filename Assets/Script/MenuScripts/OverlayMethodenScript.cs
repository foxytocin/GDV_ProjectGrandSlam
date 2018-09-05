using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayMethodenScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pausenMenuUI;
    public GameManager gameManager;
    private AudioManager audioManager;

    InGameGUI inGameGUI;

    public bool isInGame;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        inGameGUI = FindObjectOfType<InGameGUI>();
        audioManager = FindObjectOfType<AudioManager>();
        isInGame = false;
    }

    void Update()
    {
        if (InputManager.OneStartButton() || InputManager.ThreeStartButton() || InputManager.FourStartButton() || InputManager.TwoStartButton() || Input.GetKeyDown(KeyCode.Escape))
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
    }

    public void pause()
    {
        audioManager.playSound("buttonclick");
        StartCoroutine(audioManager.pitchDown());
        audioManager.stopInGameMusic();
        gameManager.lockControlls();
        pausenMenuUI.SetActive(true);
        inGameGUI.inAktivInGameUI();
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
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