using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSwitch : MonoBehaviour {

	public bool nightModus;
	public bool isDay;
	private LevelGenerator levelGenerator;
	private PlayerSpawner playerSpawner;
	private CameraScroller cameraScroller;
	private GenerateDistanceLine generateDistanceLine;
	private MenuDemoMode menuDemoMode;
	private int worldOffest;
	private int startRowPos;
	private bool update;
	private float duration;
	private float pastTime;
	private Light worldLight;
	private float worldLightOriginal;
	private float worldAmbientOriginal;

	void Awake()
	{
		isDay = true;
		update = true;
		levelGenerator = FindObjectOfType<LevelGenerator>();
		cameraScroller = FindObjectOfType<CameraScroller>();
		playerSpawner = FindObjectOfType<PlayerSpawner>();
		worldLight = FindObjectOfType<Light>();
		generateDistanceLine = FindObjectOfType<GenerateDistanceLine>();
		menuDemoMode = FindObjectOfType<MenuDemoMode>();
	}

	void Start()
	{
		duration = Random.value * 75f;
		pastTime = 0f;
		worldOffest = levelGenerator.tiefeLevelStartBasis;
		worldLightOriginal = worldLight.intensity;
		worldAmbientOriginal = RenderSettings.ambientIntensity;
	}


	// Resetet den Tag-Nacht-Wechsel beim LevelRestart basierend auf dem aktuellen Zustand
	public void restartDayNightModus()
	{
		// Stop umgehend das GlowLines und GlowBalls generiert werden duerfen
		nightModus = false;
		levelGenerator.generateGlowBalls = false;
		generateDistanceLine.generateGlowStangen = false;

		// Wird aktuell eine Nacht-Switch (Sonnenuntergang) durchgefuehrt, wird dieser Abgebrochen
		if((nightModus && isDay))
		{
			levelGenerator.generateGlowBalls = false;
			nightModus = false;
			update = true;
		}

		// Es ist aktuell dunkel: Es wird auf Tag zurueck geschaltet
		if((nightModus && !isDay))
		{
			isDay = true;
			nightModus = false;
			update = true;
			switchToDay();
			playerLightOff();
			checkGlowDistanceLines();
		}

		// Wird aktuell eine Tag-Switch (Sonnenaufgang) durchgefuehrt, wird dieser Abgebrochen
		if((!nightModus && !isDay))
		{
			isDay = true;
			update = true;
			switchToDay();
			playerLightOff();
			checkGlowDistanceLines();
		}
	}

	void Update()
	{
		if(!menuDemoMode.demoAllowed)
		{
			if(!nightModus && isDay)
			{
				if(pastTime < duration)
				{
					pastTime += Time.deltaTime;
				} else {
					nightModus = true;
					pastTime = 0f;
					duration = Random.value * 50f;
				}
			} else if(!isDay) {

				if(pastTime < duration)
				{
					pastTime += Time.deltaTime;
				} else {
					nightModus = false;
					pastTime = 0f;
					duration = Random.value * 75f;
				}
			}

			if(nightModus && isDay)
			{
				if(update)
				{
					update = false;
					startRowPos = cameraScroller.rowPosition;
				}

				levelGenerator.generateGlowBalls = true;
				if(cameraScroller.rowPosition > startRowPos + worldOffest - 10)
				{
					isDay = false;
					update = true;
					switchToNight();
					playerLightOn();

					generateDistanceLine.generateGlowStangen = true;
					checkGlowDistanceLines();
					//Debug.Log("Switch to Night");
				}
			}


			if(!nightModus && !isDay)
			{
				if(update)
				{
					update = false;
					startRowPos = cameraScroller.rowPosition;
				}

				levelGenerator.generateGlowBalls = false;
				if(cameraScroller.rowPosition > startRowPos + worldOffest - 5)
				{
					isDay = true;
					update = true;
					switchToDay();
					playerLightOff();

					generateDistanceLine.generateGlowStangen = false;
					checkGlowDistanceLines();
					//Debug.Log("Switch to Day");
				}
			}
		}
	}

	public void checkGlowDistanceLines()
	{
		if(isDay)
		{
			glowDistanceLineDimmOff();
		} else {
			StartCoroutine(glowDistanceLineDimmOn());
		}
	}

	private void playerLightOn()
	{
		for(int i = 0; i < playerSpawner.playerList.Count; i++)
		{
			GameObject player = playerSpawner.playerList[i];
			if(player != null)
				StartCoroutine(playerGlowOn(player));
		}
	}

	private void playerLightOff()
	{
		for(int i = 0; i < playerSpawner.playerList.Count; i++)
		{
			GameObject player = playerSpawner.playerList[i];
			if(player != null)
				StartCoroutine(playerGlowOff(player));
		}
	}


	 public IEnumerator playerGlowOn(GameObject player)
	{
		Material playerMaterial = player.GetComponent<Renderer>().material;
		Light playerLight = player.GetComponent<Light>();

		float emission = 0f;
		Color baseColor = playerMaterial.color;
		playerLight.intensity = 0f;
		playerLight.enabled = true;

		while(player != null && emission < 2.3f)
		{
			emission += Time.deltaTime * 0.4f;
			Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
			playerMaterial.SetColor("_EmissionColor", finalColor);
			playerMaterial.EnableKeyword("_EMISSION");
			playerLight.intensity = emission;

			yield return new WaitForSeconds(0.1f);
		}
	}

    public IEnumerator playerGlowOff(GameObject player)
	{
		Material playerMaterial = player.GetComponent<Renderer>().material;
		Light playerLight = player.GetComponent<Light>();

		float emission = 2.3f;
		Color baseColor = playerMaterial.color;

		while(player != null && emission > 0f)
		{
			emission -= Time.deltaTime * 0.5f;
			Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
			playerMaterial.SetColor("_EmissionColor", finalColor);
            playerMaterial.EnableKeyword("_EMISSION");
			playerLight.intensity = emission;

			yield return new WaitForSeconds(0.05f);
		}

		if(player != null)
		{
			playerMaterial.DisableKeyword("_EMISSION");
			playerLight.enabled = false;
			playerLight.intensity = 0f;
		}

	}


	private IEnumerator glowDistanceLineDimmOn()
	{
		yield return new WaitForSeconds(1.5f);

		foreach(GameObject go in levelGenerator.DistanceLines)
		{
			if(go != null && go.activeSelf)
			{
				if(go.CompareTag("GlowMaterial"))
				{
					go.GetComponent<GlowScript>().glowDimmOnDistanceLine();
				}

				if(go.CompareTag("MeterSchild"))
				{
					go.transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
				}
			}
		}
	}

	private void glowDistanceLineDimmOff()
	{
		foreach(GameObject go in levelGenerator.DistanceLines)
		{
			if(go != null && go.activeSelf)
			{
				if(go.CompareTag("GlowMaterial"))
				{
					go.GetComponent<GlowScript>().glowDimmOffDistanceLine();
				}

				if(go.CompareTag("MeterSchild"))
				{
					go.transform.GetChild(0).GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
				}
			}
		}
	}

	private void switchToNight()
	{
		StopAllCoroutines();
		StartCoroutine(dawnWorldAmbient(0.3f));
		StartCoroutine(dawnWorldLight(0f));
	}

	private void switchToDay()
	{
		StopAllCoroutines();
		StartCoroutine(riseWorldAmbient(2f));
		StartCoroutine(riseWorldLight(worldLightOriginal));
	}

	private IEnumerator dawnWorldLight(float value)
	{
		while(worldLight.intensity > value)
		{
			worldLight.intensity -= Time.deltaTime * 0.4f;
			yield return new WaitForSeconds(0.05f);
		}

	}
	private IEnumerator dawnWorldAmbient(float value)
	{
		while(RenderSettings.ambientIntensity > value)
		{
			RenderSettings.ambientIntensity -= Time.deltaTime * 0.3f;
			yield return new WaitForSeconds(0.05f);
		}
	}

	private IEnumerator riseWorldLight(float value)
	{
		while(worldLight.intensity < value)
		{
			worldLight.intensity += Time.deltaTime * 0.5f;
			yield return new WaitForSeconds(0.1f);
		}

		worldLight.intensity = value;
	}



	private IEnumerator riseWorldAmbient(float value)
	{
		while(RenderSettings.ambientIntensity < value)
		{
			RenderSettings.ambientIntensity += Time.deltaTime * 0.4f;
			yield return new WaitForSeconds(0.1f);
		}

		RenderSettings.ambientIntensity = worldAmbientOriginal;
	}
}