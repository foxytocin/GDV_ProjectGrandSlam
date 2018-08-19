using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houdini : MonoBehaviour {

	public LevelGenerator LevelGenerator;
	public GameObject KistenteilePrefab;

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
			if(LevelGenerator.AllGameObjects[xPos, zPos].gameObject.CompareTag("Kiste"))
			{
				LevelGenerator.AllGameObjects[xPos, zPos].SetActive(false);
				LevelGenerator.AllGameObjects[xPos, zPos] = null;
				Instantiate(KistenteilePrefab, new Vector3(xPos, 0.5f, zPos), Quaternion.identity, transform);
			}
		}
	}
}