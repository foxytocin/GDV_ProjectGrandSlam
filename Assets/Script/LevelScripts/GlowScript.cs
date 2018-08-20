using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScript : MonoBehaviour {
	Material Material;
	Color32 baseColor;
	Light Light;
	private float emissionBrigthness;

	void Awake()
	{
		emissionBrigthness = 1.6f;
		Material = GetComponent<Renderer>().material;
	}

	public void glowDimmOn()
	{
		baseColor = Material.color;
		StartCoroutine(glowStart());
	}

	public void glowDimmOff()
	{
		baseColor = Material.color;
		StartCoroutine(glowStopp());
	}

	private IEnumerator glowStart()
		{
			float emission = 0;
			while(emission < emissionBrigthness)
			{
				emission += Time.deltaTime * 0.2f;
				Color32 finalColor = (Color)baseColor * Mathf.LinearToGammaSpace(emission);
				Material.SetColor("_EmissionColor", finalColor);
				Material.EnableKeyword("_EMISSION");

				yield return null;
			}
		}

	private IEnumerator glowStopp()
		{
			float emission = emissionBrigthness;
			while(emission > 0f)
			{
				emission -= Time.deltaTime * 0.2f;
				Color32 finalColor = (Color)baseColor * Mathf.LinearToGammaSpace(emission);
				Material.SetColor("_EmissionColor", finalColor);
				Material.EnableKeyword("_EMISSION");

				yield return null;
			}

			Material.DisableKeyword("_EMISSION");
		}
}