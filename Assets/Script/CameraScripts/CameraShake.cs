using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	private PostProcessingEditing postProcessingEditing;

	void Awake()
	{
		postProcessingEditing = FindObjectOfType<PostProcessingEditing>();
	}

	public IEnumerator Shake(float duration, float intensity)
	{
		float timeMoved = 0f;

		while(timeMoved < duration)
		{
			float x = Random.Range(-1f, 1f) * intensity;
			float y = Random.Range(-1f, 1f) * intensity;
			//float z = Random.Range(-1f, 1f) * intensity;

			transform.localPosition = new Vector3(x, y, 0f);
			timeMoved += Time.deltaTime;

			//Intensity nimmt exponentiell ab
			intensity = intensity * Mathf.Pow(0.4f, timeMoved);
			postProcessingEditing.chromaticAberrationStrength = intensity * 3f;

			//Debug.Log("ShakeTimeMoved: " +timeMoved);
			//Debug.Log("ShakeIntensivity: " +intensity);

			yield return null;
		}

		transform.localPosition = new Vector3(0f, 0f, 0f);
		postProcessingEditing.chromaticAberrationStrength = 0f;
	}
}