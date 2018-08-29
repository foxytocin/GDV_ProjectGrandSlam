using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	private CameraScroller cameraScroller;
	private DestroyScroller destroyScroller;
	private GameManager gameManager;
	private MenuFade menuFade;
	private AudioSource audioSourceGameManager;
	private PlayerSpawner playerSpawner;
	private List<GameObject> playerList;
	private SpawnDemoItems spawnDemoItems;
	private bool gamePaused;
	private bool matchStartet;

    public Slider fxSlider;
    public Slider musicSlider;

    private void Awake()
	{
		cameraScroller = FindObjectOfType<CameraScroller>();
		destroyScroller = FindObjectOfType<DestroyScroller>();
		gameManager = FindObjectOfType<GameManager>();
		menuFade = FindObjectOfType<MenuFade>();
		playerSpawner = FindObjectOfType<PlayerSpawner>();
		spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
	}

	void Start()
	{
		gamePaused = false;
		matchStartet = false;
	}

	private void Update()
	{
		if(Input.GetKeyDown("p") && matchStartet)
			if(!gamePaused)
			{
				gamePaused = true;
				PauseGame();
			} else {

				ContinueGame();
				gamePaused = false;
			}
	}

    public void PlayGame()
	{
		playerList = playerSpawner.playerList;
		cameraScroller.gameStatePlay = true;
		destroyScroller.gameStatePlay = true;
		unlockControlls();
		menuFade.fadeOut();
		gamePaused = false;
		spawnDemoItems.cleanDemoItems();
		
		if(!matchStartet)
		{
			gameManager.playLetsGo();
			matchStartet = true;
            FindObjectOfType<PausenMenuScript>().isInGame = true;
		}
	}
	public void PauseGame()
	{
		menuFade.fadeIn();
		lockControlls();
		cameraScroller.gameStatePlay = false;
		destroyScroller.gameStatePlay = false;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

    private void ContinueGame()
    {
		menuFade.fadeOut();
		unlockControlls();
        cameraScroller.gameStatePlay = true;
		destroyScroller.gameStatePlay = true;
    }


	public void unlockControlls()
	{
		playerList = playerSpawner.playerList;
		foreach(GameObject go in playerList)
		{
			if(go != null)
				go.GetComponent<PlayerScript>().gameStatePlay = true;
		}
	}

	void lockControlls()
	{
		playerList = playerSpawner.playerList;
		foreach(GameObject go in playerList)
		{
			if(go != null)
				go.GetComponent<PlayerScript>().gameStatePlay = false;
		}
	}

    void updateSoundOptions()
    {
        fxSlider.value = FindObjectOfType<AudioManager>().settingsFXVolume;
        musicSlider.value = FindObjectOfType<AudioManager>().settingsMusicVolume;
    }


}
