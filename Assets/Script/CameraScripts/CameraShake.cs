using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public IEnumerator Shake(float duration, float intensity)
	{
		float timeMoved = 0f;
		float intensityBasis = intensity;

		while(timeMoved < duration)
		{
			float x = Random.Range(-1f, 1f) * intensity;
			float y = Random.Range(-1f, 1f) * intensity;

			transform.localPosition = new Vector3(x, y, 0f);
			timeMoved += Time.deltaTime;

			//Intensity nimmt prozentual zur vergangenen Zeit ab. So wird die Bewegung "ausgebremst"Mathf.Pow(speed,i);
			//intensity = intensityBasis * (duration - (duration * timeMoved));

			//intensity = 0.4f * (duration - (duration * timeMoved));

			intensity = intensity * Mathf.Pow(0.4f, timeMoved);

			//Debug.Log("ShakeTimeMoved: " +timeMoved);
			//Debug.Log("ShakeIntensivity: " +intensity);

			yield return null;
		}

		transform.localPosition = new Vector3(0f, 0f, 0f);
	}
}