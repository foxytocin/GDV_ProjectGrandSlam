using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScript : MonoBehaviour {
	Material material;
	Color baseColor;
	Light Light;
	private float emissionBrigthness;

	void Awake()
	{
		emissionBrigthness = 1.6f;
		material = GetComponent<Renderer>().material;
	}

	public void glowDimmOnDistanceLine()
	{
		baseColor = material.color;
		StartCoroutine(glowStart());
	}

	public void glowDimmOffDistanceLine()
	{
		baseColor = material.color;
		StartCoroutine(glowStopp());
	}

	private IEnumerator glowStart()
		{
			float emission = 0;
			while(emission < emissionBrigthness)
			{
				emission += Time.deltaTime * 0.1f;
				Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
				material.SetColor("_EmissionColor", finalColor);
				material.EnableKeyword("_EMISSION");

				yield return null;
			}
		}

	private IEnumerator glowStopp()
		{
			float emission = emissionBrigthness;
			while(emission > 0f)
			{
				emission -= Time.deltaTime * 0.2f;
				Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
				material.SetColor("_EmissionColor", finalColor);
				material.EnableKeyword("_EMISSION");

				yield return null;
			}

			material.DisableKeyword("_EMISSION");
		}
}