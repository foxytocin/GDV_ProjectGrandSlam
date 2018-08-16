using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houdini : MonoBehaviour {

	public LevelGenerator LevelGenerator;
	public GameObject KistenteilePrefab;

	public void callHoudini(int xPos, int zPos)
	{
		StartCoroutine(explode(xPos, zPos + 1));
		StartCoroutine(explode(xPos, zPos - 1));
		StartCoroutine(explode(xPos + 1, zPos));
		StartCoroutine(explode(xPos - 1, zPos));
	}

	IEnumerator explode(int xPos, int zPos)
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
		yield return null;
	}
}