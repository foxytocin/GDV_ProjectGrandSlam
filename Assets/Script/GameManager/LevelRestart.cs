using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestart : MonoBehaviour {


	private LevelGenerator levelGenerator;
	private DestroyScroller destroyScroller;
	private CameraScroller cameraScroller;
	private PlayerSpawner playerSpawner;
	private DayNightSwitch dayNightSwitch;
    private RulesScript rulesScript;

	void Awake()
	{
		levelGenerator = FindObjectOfType<LevelGenerator>();
		destroyScroller = FindObjectOfType<DestroyScroller>();
		cameraScroller = FindObjectOfType<CameraScroller>();
		playerSpawner = FindObjectOfType<PlayerSpawner>();
		dayNightSwitch = FindObjectOfType<DayNightSwitch>();
        rulesScript = FindObjectOfType<RulesScript>();
	}

    public void mainMenuRestart()
    {
        rulesScript.restartResults();
        levelRestart();
    }

	public void levelRestart()
	{
		// for (int i = playerSpawner.players - 1; i >= 0; i--)
        // {
        //     PlayerScript player = playerSpawner.playerList[i].GetComponent<PlayerScript>();
		// 	player.StopCoroutine(player.playerGlowOff());
		// 	player.StopCoroutine(player.playerGlowOn());
        // }

        StartCoroutine(eraseCurrentWorld());
	}

	private IEnumerator eraseCurrentWorld()
	{
		cameraScroller.restartCameraScroller();
		destroyScroller.restartDestroyScroller();

		foreach(GameObject go in levelGenerator.AllGameObjects)
		{
			if(go != null)
			{
				switch (go.tag)
				{
					case "Player":
						PlayerScript player = go.GetComponent<PlayerScript>();
						StartCoroutine(player.playerFallRestart());
						//player.StartCoroutine(player.fadeToDeath());
						break;

					case "Bombe":
						go.SetActive(false);
						break;

					default:
						FallScript fc = go.GetComponent<FallScript>();
						if (fc != false)
							StartCoroutine(fc.fallingLevelCleanup());
						break;
				}
        
			}
		}

		foreach(GameObject go in levelGenerator.SecondaryGameObjects1)
		{
			if(go != null)
			{
				FallScript fc = go.GetComponent<FallScript>();
				if (fc != false)
					StartCoroutine(fc.fallingLevelCleanup());
			}
		}

		foreach(GameObject go in levelGenerator.SecondaryGameObjects2)
		{
			if(go != null)
			{
				FallScript fc = go.GetComponent<FallScript>();
				if (fc != false)
					StartCoroutine(fc.fallingLevelCleanup());
			}
		}

		foreach(GameObject go in levelGenerator.SecondaryGameObjects3)
		{
			if(go != null)
			{
				FallScript fc = go.GetComponent<FallScript>();
				if (fc != false)
					StartCoroutine(fc.fallingLevelCleanup());
			}
		}

		foreach(GameObject go in levelGenerator.DistanceLines)
		{
			if(go != null)
			{
				FallScript fc = go.GetComponent<FallScript>();
				if (fc != false)
					StartCoroutine(fc.fallingLevelCleanup());
			}
		}

		yield return new WaitForSecondsRealtime(3.5f);
		recreateWorld();
	}

	private void recreateWorld()
	{
		levelGenerator.restartLevel();
		playerSpawner.createPlayers();
		dayNightSwitch.restartDayNightModus();

		// Fuer den unwahrscheinlichen Fall das nicht alle Objecte deaktiviert wurden
		// War eine Bugreife stelle, bisher haber problemfrei behoben
		// Code bleibt zur Beobachtung drin
		foreach(GameObject go in levelGenerator.AllGameObjects)
		{
			if(go != null)
				if(go.CompareTag("FreeFall"))
				{
					go.SetActive(false);
					Debug.Log("Alte FreeFall entfernt");
				}
					
		}
	}

}
