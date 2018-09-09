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
	private SpawnDemoItems spawnDemoItems;
	private AudioManager audioManager;
	private CounterScript CounterScript;
		
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
		spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
		audioManager = FindObjectOfType<AudioManager>();
		CounterScript = FindObjectOfType<CounterScript>();
	}


	// Methode um alles für das StartMenu vorzubereiten
  	public void levelRestartMainMenu()
	{
		StartCoroutine(levelRestartMainMenuCore());
	}
	private IEnumerator levelRestartMainMenuCore()
	{
		audioManager.playSound("fallingstones");
        cameraMovement.RestartCameraMovement(true);
        StartCoroutine(eraseCurrentWorld(true));
		rulesScript.restartResults();

		yield return new WaitForSecondsRealtime(3.5f);
		spawnDemoItems.spawnDemoItems();
	}


	// Methode um alles für das StartMenu vorzubereiten (vom DemoMode aus)
  	public void levelRestartMainMenuFromDemo()
	{
		StartCoroutine(levelRestartMainMenuFromDemoCore());
	}
	private IEnumerator levelRestartMainMenuFromDemoCore()
	{
		audioManager.playSound("fallingstones");
        cameraMovement.RestartCameraMovement(false);
        StartCoroutine(eraseCurrentWorld(false));
		yield return null;
	}


	// Mehode um alles fuer die naechste Runde vorzubreiten
	public void levelRestartNextRound()
	{
		StartCoroutine(levelRestartNextRoundCore());
	}
	public IEnumerator levelRestartNextRoundCore()
	{
		audioManager.playSound("fallingstones");
        cameraMovement.RestartCameraMovement(false);
        StartCoroutine(eraseCurrentWorld(false));
        rulesScript.nextRoundRules();

		yield return new WaitForSecondsRealtime(1.4f);
        CounterScript.startCounter();
	}

	// Steuert die Levelzersoerung und Neugenerierung
	private IEnumerator eraseCurrentWorld(bool animiert)
	{	
		searchAndDestroy("Player", animiert);
		searchAndDestroy("Boden", animiert);
		searchAndDestroy("Wand", animiert);
		searchAndDestroy("Kiste", animiert);
		searchAndDestroy("Bombe", animiert);
		searchAndDestroy("GlowBall", animiert);
		searchAndDestroy("GlowMaterial", animiert);
		searchAndDestroy("Item", animiert);
		
		searchAndDestroy("MeterSchild", animiert);
		searchAndDestroy("FreeFall", animiert);
		searchAndDestroy("Ghost", animiert);
		searchAndDestroy("Enemy", animiert);

		// Wir das Level animiert zerstoert, wird 3.6 Sekunden gewartet bis die Animation zuende ist
		if(animiert)
		{
			yield return new WaitForSecondsRealtime(3.7f);
			playerSpawner.restartPlayers();

		} else {
			
			yield return new WaitForSecondsRealtime(0.4f);
			audioManager.playSound("levelrestart");
			yield return new WaitForSecondsRealtime(0.5f);
		}

		recreateWorld(animiert);
	}


	// Sucht alle GameObjecte die zerstoert werden sollen anhand ihres Tags und zerstoert sie mit oder ohne Animation
	private void searchAndDestroy(string objectTag, bool animiert)
	{
		GameObject[] objectArray = GameObject.FindGameObjectsWithTag(objectTag);

		foreach (GameObject go in objectArray)
		{
			if(go != null) {

				FallScript fc = go.GetComponent<FallScript>();

				if(fc != null)
				{
					if(animiert)
					{
						StartCoroutine(fc.fallingLevelCleanup());
					} else {
						StartCoroutine(deaktivationDelay(go));
					}

				} else if(go.CompareTag("Player") || go.CompareTag("Enemy")) {

					if(animiert)
					{
						StartCoroutine(playerFall(go));
					} else {
						Destroy(go);
					}

				} else if(go.CompareTag("Bombe") || go.CompareTag("FreeFall") || go.CompareTag("Ghost")) {

					go.SetActive(false);
				}
			}
		}
	}


	// Sorgt fuer eine Verzoegerung beim Aufloesen des Levels
	private IEnumerator deaktivationDelay(GameObject go)
	{
		yield return new WaitForSeconds(Random.value * 0.6f);

		if(go != null)
		{
			go.SetActive(false);
		}
		
	}


	// Wird nachdem Loeschen des Levels aufgerufen und setzt alle noetigen Parameter zurueck
	private void recreateWorld(bool animiert)
	{
		dayNightSwitch.restartDayNightModus();
		cameraScroller.restartCameraScroller();
		cameraDirection.restartCameraDirection();
		destroyScroller.restartDestroyScroller();

		levelGenerator.restartLevel(animiert);
		playerSpawner.createPlayers();
	}


	// Steuert die FallAnimation der Player bei animierter Levelzerstoerung
	// Auf Grund der Objecteigenschaften muss der Player gesondert behandelt werden
    private IEnumerator playerFall(GameObject player)
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
