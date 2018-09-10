using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
	private LevelGenerator levelGenerator;
	private CameraScroller cameraScroller;
	private GameManager gameManager;
	private int EnemyCount;

	// Use this for initialization
	void Awake ()
	{
		levelGenerator = FindObjectOfType<LevelGenerator>();
		cameraScroller = FindObjectOfType<CameraScroller>();
		gameManager = FindObjectOfType<GameManager>();
	}

	void Start()
	{
		EnemyCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey("t") && gameManager.gameStatePlay)
        {
			Vector3Int spawnPos = new Vector3Int((int)Random.Range(2f, 29f), 0, cameraScroller.rowPosition + (int)Random.Range(10f, 50f));
			checkWorld(spawnPos);
        }

		if(EnemyCount < 15 && gameManager.gameStatePlay)
		{
			Vector3Int spawnPos = new Vector3Int((int)Random.Range(2f, 29f), 0, cameraScroller.rowPosition + (int)Random.Range(10f, 50f));
			checkWorld(spawnPos);

		}

		if(!gameManager.gameStatePlay)
			EnemyCount = 0;
	}

	private void checkWorld(Vector3Int spawnPos)
    {
        int xPos = spawnPos.x;
        int zPos = spawnPos.z;

        if (levelGenerator.SecondaryGameObjects1[xPos, zPos] != null)
        {
            if (levelGenerator.SecondaryGameObjects1[xPos, zPos].gameObject.CompareTag("Boden") && levelGenerator.AllGameObjects[xPos, zPos] == null)
            {
                GameObject tmpEnemy = Instantiate(enemyPrefab, new Vector3(xPos, 0.73f, zPos), Quaternion.identity);
				EnemyCount++;
            }
    	}
	}
}
