using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterScript : MonoBehaviour
{
    private GameManager gameManager;
    private LevelGenerator levelGenerator;
    private AudioManager audioManager;

    private void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }


    public void startCounter()
    {
        StartCoroutine(startCounterCore());
    }
	

    private IEnumerator startCounterCore()
    {

        int counter = 3;
        int startLinePos = levelGenerator.startLinie; 
        int breite = levelGenerator.levelBreite - 4;

        Color32 savedColor = levelGenerator.SecondaryGameObjects1[3, startLinePos + 1].GetComponent<Renderer>().material.color;

        while (counter >= 0)
        {
            for (int i = 2; i < breite; i++)
            {
                switch (counter)
                {
                    case 3:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(149, 55, 55, 1);
                        audioManager.playSound("three");
                        //Debug.Log("Red");
                        break;
                    case 2:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(149, 100, 55, 1);
                        audioManager.playSound("two");
                        //Debug.Log("Orange");
                        break;
                    case 1:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(94, 149, 55, 1);
                        audioManager.playSound("one");
                        //Debug.Log("Green");
                        break;
                    case 0:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = savedColor;
                        audioManager.playSound("lets_go");
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