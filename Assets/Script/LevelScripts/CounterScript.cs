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
    private LevelGenerator levelGenerator;
    private void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
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

        int counter = 3;
        int startLinePos = 5;
        int breite = levelGenerator.levelBreite - 2;

        Material SaveMaterial = levelGenerator.SecondaryGameObjects1[6, 1].GetComponent<Renderer>().material;

        while (counter >= 0)
        {
            for (int i = 2; i < breite-2; i++)
            {
                switch (counter)
                {
                    case 3:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 255);
                        Debug.Log("Red");
                        break;
                    case 2:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(255, 222, 0, 255);
                        Debug.Log("Orange");
                        break;
                    case 1:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 255);
                        Debug.Log("Green");
                        break;
                    case 0:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material = SaveMaterial;
                        FindObjectOfType<AudioManager>().playSound("lets_go");
                        gameManager.unlockControlls();
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForSecondsRealtime(1f);
            counter--;
        }
        
    }

    /*
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
    */
}
