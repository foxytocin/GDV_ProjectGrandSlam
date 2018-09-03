using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterScript : MonoBehaviour
{

    public GameObject counterObject;
    public TextMeshPro counterTxt;
    public Light mainLight;
    public Light spotlight;
    public Light Blue;

    private GameManager gameManager;
    private Vector3 tmpKoord;
    private float tmpLightIntensity;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        tmpKoord = counterObject.transform.position;
        tmpLightIntensity = mainLight.intensity;
    }


    public void startCounter()
    {
        StartCoroutine(startCounterCore());
    }
	
    private IEnumerator startCounterCore()
    {
        while(mainLight.intensity > 0.2f)
        {
            Debug.Log("light");
            mainLight.intensity -= (Time.deltaTime + 0.2f);
            yield return null;
        }

        spotlight.intensity = 5;
        int counter = 3;

        counterObject.SetActive(true);

        float endKoord = 8f;

        while (counter >= 0)
        {
            if (counter == 0)
            {
                counterTxt.SetText("Let´s Go!");
            }
            else
            {
                counterTxt.SetText(counter.ToString());
            }
            counterObject.transform.position = tmpKoord;
            StartCoroutine(animatorCounter(tmpKoord.z, endKoord));
            counter--;
            yield return new WaitForSecondsRealtime(1);
        }

        spotlight.intensity = 0f;

        while (mainLight.intensity < tmpLightIntensity)
        {
            Debug.Log("light");
            mainLight.intensity += (Time.deltaTime + 0.2f);
            yield return null;
        }

        counterObject.SetActive(false);
        FindObjectOfType<AudioManager>().playSound("lets_go");
        gameManager.unlockControlls();

    }

    IEnumerator animatorCounter(float begin, float end)
    {

        while(begin < end)
        {
            counterObject.transform.Translate(0, 0, 0.2f * (Time.deltaTime + 0.2f));
            yield return null;
        }

    }
}
