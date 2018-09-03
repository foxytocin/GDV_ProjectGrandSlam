using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterScript : MonoBehaviour
{
    private GameManager gameManager;
    private Vector3 tmpKoord;
    private float tmpLightIntensity;
    private LevelGenerator levelGenerator;
    private void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameManager = FindObjectOfType<GameManager>();
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
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(147, 0, 0, 255);
                        Debug.Log("Red");
                        break;
                    case 2:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(209, 143, 0, 255);
                        Debug.Log("Orange");
                        break;
                    case 1:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(54, 165, 0, 255);
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
            yield return new WaitForSecondsRealtime(1.5f);
            counter--;
        }
        
    }

}
