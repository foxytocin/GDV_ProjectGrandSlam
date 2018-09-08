using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    private GameManager gameManager;
    private LevelGenerator levelGenerator;
    private AudioManager audioManager;

    private int counter;
    private int startLinePos;
    private int breite;


    private void Start()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void startCounter()
    {
        counter = 3;
        startLinePos = levelGenerator.startLinie;
        breite = levelGenerator.levelBreite - 4;
        
        StartCoroutine(startCounterCore());
    }


    private IEnumerator startCounterCore()
    {
        while (counter >= 0)
        {
            switch (counter)
            {
                case 3:
                    audioManager.playSound("three");
                    break;
                case 2:
                    audioManager.playSound("two");
                    changeColor(new Color32(149, 100, 55, 1));
                    break;
                case 1:
                    audioManager.playSound("one");
                    changeColor(new Color32(94, 149, 55, 1));
                    break;
                case 0:
                    audioManager.playSound("lets_go");
                    changeColor(new Color32(168, 168, 168, 1));
                    gameManager.unlockControlls();
                    break;
                default:
                    break;
            }

            yield return new WaitForSecondsRealtime(1.5f);
            counter--;
        }

    }

    private void changeColor(Color32 color)
    {
        for (int i = 2; i < breite; i++)
        {
            levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = color;
        }
    }

}