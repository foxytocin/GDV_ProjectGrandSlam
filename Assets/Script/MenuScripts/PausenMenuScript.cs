
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausenMenuScript : MonoBehaviour
{

    private LevelRestart levelRestart;
    public static bool GameIsPaused = false;
    public bool isInGame = false;
    public GameObject pausenMenuUI;
    public MainMenu MainMenu;
    public Slider fxSlider;
    public Slider musicSlider;

    
    void Awake()
    {
        levelRestart = FindObjectOfType<LevelRestart>();
    }

	void Update ()
    {
		if(InputManager.OneStartButton() || InputManager.ThreeStartButton() || InputManager.FourStartButton() || InputManager.TwoStartButton() || Input.GetKeyDown(KeyCode.Escape) && isInGame)
        {
            if(GameIsPaused)
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

    public void mainMenu()
    {
        Time.timeScale = 1f;
        MainMenu.PauseGame();
        levelRestart.levelRestart();
    }

    public void updateSoundOptions()
    {
        fxSlider.value = FindObjectOfType<AudioManager>().settingsFXVolume;
        musicSlider.value = FindObjectOfType<AudioManager>().settingsMusicVolume;
    }
}
