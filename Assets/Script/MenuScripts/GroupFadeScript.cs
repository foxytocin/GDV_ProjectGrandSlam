using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupFadeScript : MonoBehaviour
{


    private float menueOpacity;
    private float fadeSpeed;

    CanvasGroup canvasGroup; 
    Canvas canvas;

    private void Awake()
    {
        fadeSpeed = 1f;
        menueOpacity = 1f;

        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();
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
        while (menueOpacity > 0f)
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

        while (menueOpacity < 1f)
        {
            menueOpacity += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = menueOpacity;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
