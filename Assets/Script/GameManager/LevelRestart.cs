using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestart : MonoBehaviour {


	private LevelGenerator levelGenerator;
	private DestroyScroller destroyScroller;
	private CameraScroller cameraScroller;
	private PlayerSpawner playerSpawner;

	void Awake()
	{
		levelGenerator = FindObjectOfType<LevelGenerator>();
		destroyScroller = FindObjectOfType<DestroyScroller>();
		cameraScroller = FindObjectOfType<CameraScroller>();
		playerSpawner = FindObjectOfType<PlayerSpawner>();
	}

	public void levelRestart()
	{

        for (int i = playerSpawner.players - 1; i >= 0; i--)
        {
            Color32 tmpPlayerColor = playerSpawner.playerList[i].GetComponent<Renderer>().material.color;

            for (int j = tmpPlayerColor.a; j > 0; j--)
            {
                tmpPlayerColor.a--;
            }

            Destroy(playerSpawner.playerList[i]);
        }

        StartCoroutine(eraseCurrentWorld());
	}

	public void nextRound()
	{
		levelRestart();
	}

	private IEnumerator eraseCurrentWorld()
	{
		foreach(GameObject go in levelGenerator.AllGameObjects)
		{
			if(go != null)
			{
				switch (go.tag)
				{
					case "Player":
						Destroy(go);
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
		cameraScroller.restartCameraScroller();
		destroyScroller.restartDestroyScroller();
		levelGenerator.restartLevel();
		playerSpawner.createPlayers();

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
