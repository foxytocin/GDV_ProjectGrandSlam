using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioBackgoundMusic;
    public AudioClip audioStart;
    //public AudioClip audioOnePlayer;
    public AudioClip audioTwoPlayer;
    public AudioClip audioThreePlayer;
    public AudioClip audioFourPlayer;

    public PlayerSpawner playerSpawner;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioStart);
        audioSource.PlayOneShot(audioBackgoundMusic, 0.15f);
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown("1"))
        {
            playerSpawner.setPlayers(1);
            playerSpawner.createPlayers();
            //audioSource.PlayOneShot(audioOnePlayer, 0.5f);
        }
        if (Input.GetKeyDown("2"))
        {
            playerSpawner.setPlayers(2);
            playerSpawner.createPlayers();
            audioSource.PlayOneShot(audioTwoPlayer, 0.5f);
        }
        if (Input.GetKeyDown("3"))
        {
            playerSpawner.setPlayers(3);
            playerSpawner.createPlayers();
            audioSource.PlayOneShot(audioThreePlayer, 0.5f);
        }
        if (Input.GetKeyDown("4"))
        {
            playerSpawner.setPlayers(4);
            playerSpawner.createPlayers();
            audioSource.PlayOneShot(audioFourPlayer, 0.5f);
        }
    }
}