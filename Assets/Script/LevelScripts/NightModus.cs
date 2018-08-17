using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightModus : MonoBehaviour {

	public bool nightModus;
	private bool isDay;
	private LevelGenerator levelGenerator;
	private Light worldLight;
	private float worldLightIntensivity;

	void Awake()
	{
		levelGenerator = FindObjectOfType<LevelGenerator>();
		worldLight = FindObjectOfType<Light>();
	}

	void Start()
	{
		isDay = true;
		worldLightIntensivity = worldLight.intensity;
	}

	void Update()
	{
		if(nightModus && isDay)
		{
			isDay = false;
			switchToNight();
			Debug.Log("Switch to Night");
		}

		if(!nightModus && !isDay)
		{
			isDay = true;
			switchToDay();
			Debug.Log("Switch to Day");

		} 
	}

	private void switchToNight()
	{
		StopAllCoroutines();
		levelGenerator.generateGlowBalls = true;
		RenderSettings.ambientIntensity = 0.5f;
		StartCoroutine(dawnWorldLight(0f));
	}

	private void switchToDay()
	{
		StopAllCoroutines();
		levelGenerator.generateGlowBalls = false;
		RenderSettings.ambientIntensity = 2f;
		StartCoroutine(riseWorldLight(worldLightIntensivity));
	}

	private IEnumerator dawnWorldLight(float value)
	{
		while(worldLight.intensity > value)
		{
			worldLight.intensity -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator riseWorldLight(float value)
	{
		while(worldLight.intensity < value)
		{
			worldLight.intensity += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		worldLight.intensity = value;
	}
}
