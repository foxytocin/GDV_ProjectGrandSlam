using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayMethodenScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pausenMenuUI;

    InGameGUI inGameGUI;

    public bool isInGame;

    private void Awake()
    {
        inGameGUI = FindObjectOfType<InGameGUI>();
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
            }
            else
            {
                pause();
                Cursor.visible = true;
            }
        }
    }

    public void resume()
    {
        Cursor.visible = false;
        inGameGUI.aktivInGameUI();
        Time.timeScale = 1f;
        GameIsPaused = false;
        isInGame = true;
        pausenMenuUI.SetActive(false);
    }

    public void pause()
    {
        pausenMenuUI.SetActive(true);
        inGameGUI.inAktivInGameUI();
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
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