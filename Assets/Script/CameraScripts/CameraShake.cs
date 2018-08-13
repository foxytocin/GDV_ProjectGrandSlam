using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public IEnumerator Shake(float duration, float intensity)
	{
		float timeMoved = 0f;

		while(timeMoved < duration)
		{
			float x = Random.Range(-1f, 1f) * intensity;
			float y = Random.Range(-1f, 1f) * intensity;

			transform.localPosition = new Vector3(x, y, 0f);
			timeMoved += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = new Vector3(0f, 0f, 0f);
	}
}
