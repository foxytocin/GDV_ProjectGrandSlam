using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDemoMode : MonoBehaviour {


	private float timeToDemo;
	private float countDown;
	public bool demoRunning;
	public bool demoAllowed;
	private int startLinePos;
	private int levelBreite;
	private int levelTiefe;
	private PlayerSpawner PlayerSpawner;
	private LevelRestart LevelRestart;
	private LevelGenerator levelGenerator;
	private GameManager gameManager;

	void Awake()
	{
		demoRunning = false;
		timeToDemo = 9.2f;
		PlayerSpawner = FindObjectOfType<PlayerSpawner>();
		LevelRestart = FindObjectOfType<LevelRestart>();
		levelGenerator = FindObjectOfType<LevelGenerator>();
		gameManager = FindObjectOfType<GameManager>();

	}

	void Start()
	{
		startLinePos = levelGenerator.startLinie;
		levelBreite = levelGenerator.levelBreite - 4;
		levelTiefe = levelGenerator.levelTiefe;
		countDown = timeToDemo;
		demoAllowed = true;
	}
	
	public void allowDemo()
	{
		demoAllowed = true;
		countDown = timeToDemo;
	}

	public void	forbidDemo()
	{
		demoAllowed = false;
		countDown = timeToDemo;
		demoRunning = false;
	}


	// Update is called once per frame
	void LateUpdate ()
	{
		if(demoAllowed)
		{
			if(!demoRunning && countDown > 0f)
			{
				countDown -= Time.deltaTime;

			} else if (!demoRunning) {

				demoRunning = true;
				changeColorStartLine();
				remoteControllPlayer();
			}

		} else if (demoRunning) {
			
			demoRunning = false;
			countDown = timeToDemo;
		}
		
	}

	private void changeColorStartLine()
	{
		for (int i = 2; i < levelBreite; i++)
        {
            levelGenerator.SecondaryGameObjects1[i, startLinePos].GetComponent<Renderer>().material.color = new Color32(168, 168, 168, 255);
		}
	}

	private void remoteControllPlayer()
	{
		foreach(GameObject player in PlayerSpawner.playerList)
		{
			if(player != null)
			{
				levelGenerator.AllGameObjects[(int)player.transform.position.x, (int)player.transform.position.z] = null;
				player.GetComponent<MeshRenderer>().enabled = false;
				player.GetComponent<Collider>().enabled = false;
				StartCoroutine(movePlayer(player));
			}
		}
	}

	private IEnumerator movePlayer(GameObject player)
	{
		while(player != null && demoAllowed && player.transform.position.z < (levelTiefe - levelGenerator.tiefeLevelStartBasis - 10))
		{
			Vector3 target = new Vector3(player.transform.position.x, 0, levelTiefe);
			player.transform.position = Vector3.MoveTowards(player.transform.position, target, 0.075f);
			yield return null;
		}

		gameManager.lockControlls();
		countDown = timeToDemo;
		demoRunning = false;

		if(player != null)
		{
			Destroy(player);
		}

		LevelRestart.levelRestartMainMenuFromDemo();
	}
}