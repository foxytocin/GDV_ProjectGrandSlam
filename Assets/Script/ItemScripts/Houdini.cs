using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houdini : MonoBehaviour {

	private LevelGenerator LevelGenerator;
	private AudioManager audioManager;
	public GameObject KistenteilePrefab;

	void Awake()
	{
		audioManager = FindObjectOfType<AudioManager>();
		LevelGenerator = FindObjectOfType<LevelGenerator>();
	}

	public void callHoudini(int xPos, int zPos)
	{
		explode(xPos, zPos + 1);
		explode(xPos, zPos - 1);
		explode(xPos + 1, zPos);
		explode(xPos - 1, zPos);
	}

	void explode(int xPos, int zPos)
	{
		if(LevelGenerator.AllGameObjects[xPos, zPos] != null)
		{
			GameObject go = LevelGenerator.AllGameObjects[xPos, zPos].gameObject;

			switch(go.tag)
			{
				case "Kiste":
					Instantiate(KistenteilePrefab, new Vector3(xPos, 0.5f, zPos), Quaternion.identity, transform);
					LevelGenerator.AllGameObjects[xPos, zPos] = null;
					go.SetActive(false);
					break;
				
			}
		}
	}
}