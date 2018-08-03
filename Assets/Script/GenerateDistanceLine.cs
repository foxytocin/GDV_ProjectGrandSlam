using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDistanceLine : MonoBehaviour {

public LevelGenerator LevelGenerator;
public GameObject StangePrefab;
public GameObject QuerstrebePrefab;
private int startPoint;
private int endPoint;
private int centerPoint;
private int scaleQuerstrebe;

	void Start()
	{
		startPoint = 0;
		endPoint = 0;
	}

	public void createDistanceLine(int row)
	{
		bool foundStart = false;
		for (int i = 0; i < LevelGenerator.levelSectionData[0].Length - 1; i++)
		{
			if(LevelGenerator.AllGameObjects[i, row] != null)
			{
				//Begin des Levels suchen (das erste Mauerstück)
				if(LevelGenerator.AllGameObjects[i, row].gameObject.CompareTag("Wand") && !foundStart) {
					foundStart = true;
					startPoint = i;
				}
			}

			//Ende des Levels suchen (das letzte Mauerstück)
			if(LevelGenerator.AllGameObjects[i, row] != null && foundStart) {
				endPoint = i;
			}

			centerPoint = startPoint + (endPoint - startPoint) / 2;
			scaleQuerstrebe = endPoint - startPoint - 1;
		}

		GameObject StartStange = Instantiate(StangePrefab, new Vector3(startPoint, 1f, row), Quaternion.Euler(0f, -90f, 0f), transform);
		GameObject EndStange = Instantiate(StangePrefab, new Vector3(endPoint, 1f, row), Quaternion.Euler(0f, 90f, 0f), transform);
		GameObject Querstrebe = Instantiate(QuerstrebePrefab, new Vector3(centerPoint, 4f, row), Quaternion.Euler(0f, 90f, 0f), transform);
		Querstrebe.transform.localScale = new Vector3(1f, 1f, scaleQuerstrebe);

		LevelGenerator.DistanceLines[0, row] = StartStange;
		LevelGenerator.DistanceLines[1, row] = EndStange;
		LevelGenerator.DistanceLines[2, row] = Querstrebe;
	}
}
