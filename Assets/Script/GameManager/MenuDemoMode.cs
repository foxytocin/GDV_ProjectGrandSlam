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
	private PlayerSpawner PlayerSpawner;
	private LevelRestart LevelRestart;
	private SpawnDemoItems spawnDemoItems;
	private LevelGenerator levelGenerator;

	void Awake()
	{
		PlayerSpawner = FindObjectOfType<PlayerSpawner>();
		LevelRestart = FindObjectOfType<LevelRestart>();
		spawnDemoItems = FindObjectOfType<SpawnDemoItems>();
		levelGenerator = FindObjectOfType<LevelGenerator>();
	}

	void Start()
	{
		demoRunning = false;
		startLinePos = levelGenerator.startLinie;
		levelBreite = levelGenerator.levelBreite - 4;
		timeToDemo = 15;
		countDown = timeToDemo;
		demoAllowed = true;
	}
	
	public void alloDemo()
	{
		demoAllowed = true;
	}

	public void	forbidDemo()
	{
		demoAllowed = false;
	}


	// Update is called once per frame
	void LateUpdate ()
	{
		if(demoAllowed)
		{
			if(!demoRunning && countDown > 0f)
			{
				countDown -= Time.deltaTime;
				//Debug.Log("Demo Modus in: " +(int)countDown+ " Sekunden");

			} else if (!demoRunning) {

				demoRunning = true;
				spawnDemoItems.cleanDemoItems();
				changeColorStartLine();
				searchPlayer();
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

	private void searchPlayer()
	{
		foreach(GameObject player in PlayerSpawner.playerList)
		{
			if(player != null)
			{
				player.GetComponent<MeshRenderer>().enabled = false;
				StartCoroutine(movePlayer(player));
			}
		}
	}

	private IEnumerator movePlayer(GameObject player)
	{
		while(demoAllowed && player.transform.position.z < 2000f)
		{
			Vector3 target = new Vector3(player.transform.position.x, 0, 2000f);
			player.transform.position = Vector3.MoveTowards(player.transform.position, target, 0.075f);
			yield return null;
		}
			LevelRestart.levelRestartMainMenu();
	}


}
