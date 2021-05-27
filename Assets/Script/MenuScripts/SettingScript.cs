using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Accessibility;

public class SettingScript : MonoBehaviour
{

    public Dropdown resolutionDropdown;
    GameManager gameManager;

    Resolution[] resolutions;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        setQuality(3);
        Screen.SetResolution(1920, 1080, true);
    }

    public void settingFX(float fxVolume)
    {
        Debug.Log(fxVolume);
    }

    public void settingMusic(float musicVolume)
    {
        Debug.Log(musicVolume);
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution newResolution = resolutions[resolutionIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    public void setQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void setFullscrenn(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void setController(int index)
    {
        Debug.Log("SetControllerIndex: " + index);
        gameManager.controller = index;

        switch(index)
        {
            case 0:
                break;
        }
    }

}
