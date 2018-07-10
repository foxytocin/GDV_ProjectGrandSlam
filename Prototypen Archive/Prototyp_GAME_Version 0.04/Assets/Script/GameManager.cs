using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public PlayerSpawner playerSpawner;


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
        }
        if (Input.GetKeyDown("2"))
        {
            playerSpawner.setPlayers(2);
            playerSpawner.createPlayers();
        }
        if (Input.GetKeyDown("3"))
        {
            playerSpawner.setPlayers(3);
            playerSpawner.createPlayers();
        }
        if (Input.GetKeyDown("4"))
        {
            playerSpawner.setPlayers(4);
            playerSpawner.createPlayers();
        }
    }
}
