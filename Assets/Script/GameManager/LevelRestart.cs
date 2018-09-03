using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestart : MonoBehaviour {

	private LevelGenerator levelGenerator;
	private DestroyScroller destroyScroller;
	private CameraScroller cameraScroller;
	private CameraDirection cameraDirection;
	private CameraMovement cameraMovement;
	private PlayerSpawner playerSpawner;
	private DayNightSwitch dayNightSwitch;
    private RulesScript rulesScript;
	private GameManager GameManager;
	private SpawnDemoItems spawnDemoItems;
	private AudioManager audioManager;
	private CounterScript CounterScript;
	//private CameraMovement cam;

	void Awake()
	{
		levelGenerator = FindObjectOfType<LevelGenerator>();
		destroyScroller = FindObjectOfType<DestroyScroller>();
		cameraScroller = FindObjectOfType<CameraScroller>();
		cameraDirection = FindObjectOfType<CameraDirection>();
		cameraMovement = FindObjectOfType<CameraMovement>();
		playerSpawner = FindObjectOfType<PlayerSpawner>();
		dayNightSwitch = FindObjectOfType<DayNightSwitch>();
        rulesScript = FindObjectOfType<RulesScript>();
		GameManager = FindObjectOfType<GameManager>();
		spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
		audioManager = FindObjectOfType<AudioManager>();
		CounterScript = FindObjectOfType<CounterScript>();
		//cam = FindObjectOfType<CameraMovement>();
	}


	// Methode um alles für das StartMenu vorzubereiten
  	public void levelRestartMainMenu()
	{
		StartCoroutine(levelRestartMainMenuCore());
	}
	private IEnumerator levelRestartMainMenuCore()
	{
        cameraMovement.RestartCameraMovement();
        StartCoroutine(eraseCurrentWorld(true));
		rulesScript.restartResults();

		yield return new WaitForSecondsRealtime(3.5f);
		spawnDemoItems.spawnDemoItems();
	}

	// Mehode um alles fuer die naechste Runde vorzubreiten
	public void levelRestartNextRound()
	{
		StartCoroutine(levelRestartNextRoundCore());
	}
	public IEnumerator levelRestartNextRoundCore()
	{
        cameraMovement.RestartCameraMovement();
        StartCoroutine(eraseCurrentWorld(false));

		yield return new WaitForSecondsRealtime(1f);
        rulesScript.nextRoundRules();

		yield return new WaitForSecondsRealtime(0.1f);
        CounterScript.startCounter();
	}

	private IEnumerator eraseCurrentWorld(bool animiert)
	{
        
        foreach (GameObject go in levelGenerator.AllGameObjects)
		{
			if(go != null)
			{
				switch (go.tag)
				{
					case "Player":
						
						if(animiert)
						{
							StartCoroutine(playerFall(go));
							
						} else {

							levelGenerator.AllGameObjects[(int)go.transform.position.x, (int)go.transform.position.z] = null;
							Destroy(go);
						}
						break;

					case "Bombe":
						go.SetActive(false);
						break;

					default:
						if(animiert)
						{
							FallScript fc = go.GetComponent<FallScript>();
							if (fc != false)
								StartCoroutine(fc.fallingLevelCleanup());

						} else {

							StartCoroutine(deaktivationDelay(go));
						}
					break;
				}
        
			}
		}


        //Hier kamera methode?
        

        cleanObjectArray(levelGenerator.SecondaryGameObjects1, animiert);
		cleanObjectArray(levelGenerator.SecondaryGameObjects2, animiert);
		cleanObjectArray(levelGenerator.SecondaryGameObjects3, animiert);
		cleanObjectArray(levelGenerator.DistanceLines, animiert);
	
		// Wir das Level animiert zerstoert, wird 3.5 Sekunden gewartet bis die Animation zuende ist
		if(animiert)
		{
			yield return new WaitForSecondsRealtime(3.5f);

		} else {

			yield return new WaitForSecondsRealtime(0.7f);
		}

		recreateWorld(animiert);
	}


	// Ubernimmt die Bereinigung der Arrays indem alle gefunden Objekte in die ObjectPool zurueckgelegt werden
	private void cleanObjectArray(GameObject[,] array, bool animiert)
	{
		foreach(GameObject go in array)
		{
			if(go != null)
			{
				if(animiert)
				{
					FallScript fc = go.GetComponent<FallScript>();
					if (fc != false)

						StartCoroutine(fc.fallingLevelCleanup());

				} else {

					StartCoroutine(deaktivationDelay(go));
				}
			}
		}
	}

	private IEnumerator deaktivationDelay(GameObject go)
	{
		yield return new WaitForSeconds(Random.value * 0.6f);
		go.SetActive(false);
	}

	private void recreateWorld(bool animiert)
	{
		dayNightSwitch.restartDayNightModus();
		cameraScroller.restartCameraScroller();
		cameraDirection.restartCameraDirection();
        
		destroyScroller.restartDestroyScroller();
		levelGenerator.restartLevel(animiert);

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

		playerSpawner.createPlayers();
	}


    public IEnumerator playerFall(GameObject player)
    {
        levelGenerator.AllGameObjects[(int)player.transform.position.x, (int)player.transform.position.z] = null;
		Vector3 target = player.transform.position;
		target.y = -50f;

        while(player != null && player.transform.position.y > target.y)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 0.3f);
            yield return null;
        }

		if(player != null)
		{
			Destroy(player);
		}
    }

}
