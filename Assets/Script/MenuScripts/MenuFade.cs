using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFade : MonoBehaviour {

	private CanvasGroup canvasGroup;
	private Canvas canvas;
	private float menueOpacity;
	private float fadeSpeed;

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvas = GetComponent<Canvas>();
	}

	// Use this for initialization
	void Start ()
	{
		fadeSpeed = 1f;
		menueOpacity = 1f;
	}


	public void fadeOut()
	{
		StartCoroutine(fadeOutCore());
	}

	public void fadeIn()
	{
		StartCoroutine(fadeInCore());
	}

	private IEnumerator fadeOutCore()
	{
		while(menueOpacity > 0f)
		{
			menueOpacity -= Time.deltaTime * fadeSpeed;
			canvasGroup.alpha = menueOpacity;
			yield return null;
		}
		canvasGroup.alpha = 0f;
		canvas.enabled = false;
	}

	private IEnumerator fadeInCore()
	{
		canvas.enabled = true;
		
		while(menueOpacity < 1f)
		{
			menueOpacity += Time.deltaTime * fadeSpeed;
			canvasGroup.alpha = menueOpacity;
			yield return null;
		}
		canvasGroup.alpha = 1f;
	}

}