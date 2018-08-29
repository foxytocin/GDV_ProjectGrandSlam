using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayMethodenScript : MonoBehaviour
{

    private GameManager gameManager;
    private AudioSource audioSourceGameManager;
    private PlayerSpawner playerSpawner;
    private List<GameObject> playerList;
    private SpawnDemoItems spawnDemoItems;
    private bool gamePaused;
    private bool matchStartet;
    private LevelRestart levelRestart;

    public static bool GameIsPaused = false;
    public GameObject pausenMenuUI;

    public bool isInGame;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
        levelRestart = FindObjectOfType<LevelRestart>();
        isInGame = false;
    }

    void Update()
    {
        if (InputManager.OneStartButton() || InputManager.ThreeStartButton() || InputManager.FourStartButton() || InputManager.TwoStartButton() || Input.GetKeyDown(KeyCode.Escape) && isInGame)
        {
            if (GameIsPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    public void resume()
    {
        pausenMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        isInGame = true;

    }

    public void pause()
    {
        pausenMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
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
        offSetSide.SetActive(false);
        onSetSide.SetActive(true);
    }
}