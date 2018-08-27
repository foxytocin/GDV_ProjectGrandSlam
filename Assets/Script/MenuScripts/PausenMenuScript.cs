
using UnityEngine;
using UnityEngine.UI;

public class PausenMenuScript : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pausenMenuUI;

    public Slider fxSlider;
    public Slider musicSlider;

	void Update ()
    {
		if(InputManager.OneStartButton() || InputManager.ThreeStartButton() || InputManager.FourStartButton() || InputManager.TwoStartButton() || Input.GetKeyDown(KeyCode.Escape))
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
    }

    void pause()
    {
        pausenMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void updateSoundOptions()
    {
        fxSlider.value = FindObjectOfType<AudioManager>().settingsFXVolume;
        musicSlider.value = FindObjectOfType<AudioManager>().settingsMusicVolume;
    }
}
