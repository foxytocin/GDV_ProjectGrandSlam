using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houdini : MonoBehaviour {

	public LevelGenerator LevelGenerator;
	public GameObject KistenteilePrefab;
	ObjectPooler objectPooler;

	void Start()
	{
		objectPooler = ObjectPooler.Instance;
	}

	public void callHoudini(int xPos, int zPos)
	{
		if(LevelGenerator.AllGameObjects[xPos, zPos + 1] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos, zPos + 1].gameObject.CompareTag("Kiste"))
			{
				LevelGenerator.AllGameObjects[xPos, zPos + 1].SetActive(false);
				LevelGenerator.AllGameObjects[xPos, zPos + 1] = null;
				Instantiate(KistenteilePrefab, new Vector3(xPos, 0.5f, zPos + 1), Quaternion.identity, transform);
				//objectPooler.SpawnFromPool("Kiste_Destroyed", new Vector3(xPos, 0.5f, zPos + 1), Quaternion.identity);
			}
		}

		if(LevelGenerator.AllGameObjects[xPos, zPos - 1] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos, zPos - 1].gameObject.CompareTag("Kiste"))
			{
				LevelGenerator.AllGameObjects[xPos, zPos - 1].SetActive(false);
				LevelGenerator.AllGameObjects[xPos, zPos - 1] = null;
				Instantiate(KistenteilePrefab, new Vector3(xPos, 0.5f, zPos - 1), Quaternion.identity, transform);
				//objectPooler.SpawnFromPool("Kiste_Destroyed", new Vector3(xPos, 0.5f, zPos - 1), Quaternion.identity);
			}
		}

		if(LevelGenerator.AllGameObjects[xPos + 1, zPos] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos + 1, zPos].gameObject.CompareTag("Kiste"))
			{
				LevelGenerator.AllGameObjects[xPos + 1, zPos].SetActive(false);
				LevelGenerator.AllGameObjects[xPos + 1, zPos] = null;
				Instantiate(KistenteilePrefab, new Vector3(xPos + 1, 0.5f, zPos), Quaternion.identity, transform);
				//objectPooler.SpawnFromPool("Kiste_Destroyed", new Vector3(xPos + 1, 0.5f, zPos), Quaternion.identity);
			}
		}

		if(LevelGenerator.AllGameObjects[xPos - 1, zPos] != null)
		{
			if(LevelGenerator.AllGameObjects[xPos - 1, zPos].gameObject.CompareTag("Kiste"))
			{
				LevelGenerator.AllGameObjects[xPos - 1, zPos].SetActive(false);
				LevelGenerator.AllGameObjects[xPos - 1, zPos] = null;
				Instantiate(KistenteilePrefab, new Vector3(xPos - 1, 0.5f, zPos), Quaternion.identity, transform);
				//objectPooler.SpawnFromPool("Kiste_Destroyed", new Vector3(xPos - 1, 0.5f, zPos), Quaternion.identity);
			}
		}
	}

}
