using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houdini : MonoBehaviour {

	public LevelGenerator LevelGenerator;
	public GameObject KistenteilePrefab;

	public void callHoudini(int xPos, int zPos)
	{
		// int xPos = Mathf.RoundToInt(positon.x);
		// int zPos = Mathf.RoundToInt(positon.z);

		if(LevelGenerator.AllGameObjects[xPos, zPos + 1] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos, zPos + 1].gameObject.CompareTag("Kiste"))
			{
				Destroy(LevelGenerator.AllGameObjects[xPos, zPos + 1]);
				Instantiate(KistenteilePrefab, new Vector3(xPos, 0.5f, zPos + 1), Quaternion.identity, transform);
			}
		}

		if(LevelGenerator.AllGameObjects[xPos, zPos - 1] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos, zPos - 1].gameObject.CompareTag("Kiste"))
			{
				Destroy(LevelGenerator.AllGameObjects[xPos, zPos - 1]);
				Instantiate(KistenteilePrefab, new Vector3(xPos, 0.5f, zPos - 1), Quaternion.identity, transform);
			}
		}

		if(LevelGenerator.AllGameObjects[xPos + 1, zPos] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos + 1, zPos].gameObject.CompareTag("Kiste"))
			{
				Destroy(LevelGenerator.AllGameObjects[xPos + 1, zPos]);
				Instantiate(KistenteilePrefab, new Vector3(xPos + 1, 0.5f, zPos), Quaternion.identity, transform);
			}
		}

		if(LevelGenerator.AllGameObjects[xPos - 1, zPos] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos - 1, zPos].gameObject.CompareTag("Kiste"))
			{
				Destroy(LevelGenerator.AllGameObjects[xPos - 1, zPos]);
				Instantiate(KistenteilePrefab, new Vector3(xPos - 1, 0.5f, zPos), Quaternion.identity, transform);
			}
		}
	}

}
