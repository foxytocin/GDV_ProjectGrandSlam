using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        while (counter >= 0)
        {
            switch (counter)
            {
                case 3:
                    audioManager.playSound("three");
                    break;
                case 2:
                    audioManager.playSound("two");
                    break;
                case 1:
                    audioManager.playSound("one");
                    break;
                case 0:
                    audioManager.playSound("lets_go");
                    gameManager.unlockControlls();
                    break;
                default:
                    break;
            }

            for (int i = 2; i < breite; i++)
            {
                switch (counter)
                {
                    case 2:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(149, 100, 55, 1);
                        break;
                    case 1:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(94, 149, 55, 1);
                        break;
                    case 0:
                        levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(168, 168, 168, 1);
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