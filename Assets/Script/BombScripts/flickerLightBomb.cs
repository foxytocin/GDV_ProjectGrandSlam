using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerLightBomb : MonoBehaviour {
	private Light Light;
	float MaxReduction = 0.6f;
	float MaxIncrease = 0.6f;
	float RateDamping = 0.1f;
	float Strength = 300;
	float baseIntensity;

	// Use this for initialization
	void Start ()
	{
		Light = GetComponent<Light>();
		baseIntensity = Light.intensity;
		StartCoroutine(flicker());
	}
	
	private IEnumerator flicker()
	{
		while (true)
         {
             Light.intensity = Mathf.Lerp(Light.intensity, Random.Range(baseIntensity - MaxReduction, baseIntensity + MaxIncrease), Strength * Time.deltaTime);
             yield return new WaitForSeconds(RateDamping);
         }
	}

}
