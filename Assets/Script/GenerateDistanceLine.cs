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
			if(LevelGenerator.AllGameObjects[i, row] != null && !foundStart)
			{
				//Begin des Levels suchen (das erste Mauerstück)
				if(LevelGenerator.AllGameObjects[i, row].gameObject.CompareTag("Wand")
				&& (LevelGenerator.AllGameObjects[i + 1, row] == null || LevelGenerator.AllGameObjects[i + 1, row].gameObject.CompareTag("Kiste")))
				{
					foundStart = true;
					startPoint = i;
					break;
				}
			}
		}

		bool foundEnd = false;
		for (int i = LevelGenerator.levelSectionData[0].Length; i > startPoint; i--)
		{
			if(LevelGenerator.AllGameObjects[i, row] != null && !foundEnd)
			{
				//Ende des Levels suchen (das letzte Mauerstück )
				if(LevelGenerator.AllGameObjects[i, row].gameObject.CompareTag("Wand")
				&& (LevelGenerator.AllGameObjects[i - 1, row] == null || LevelGenerator.AllGameObjects[i - 1, row].gameObject.CompareTag("Kiste")))
				{
					foundEnd = true;
					endPoint = i;
					break;
				}
			}
		}

		//Berechnung der Mitte zwischen beiden Pfosten am Spielrand
		centerPoint = startPoint + (endPoint - startPoint) / 2;
		scaleQuerstrebe = endPoint - startPoint - 1;

		//Generierung der GameObjecte mit richtiger Rotation und Scalierung entsprechend dem Abstand der Pfosten
		GameObject StartStange = Instantiate(StangePrefab, new Vector3(startPoint, 1f, row), Quaternion.Euler(0f, -90f, 0f), transform);
		GameObject EndStange = Instantiate(StangePrefab, new Vector3(endPoint, 1f, row), Quaternion.Euler(0f, 90f, 0f), transform);
		GameObject Querstrebe = Instantiate(QuerstrebePrefab, new Vector3(centerPoint, 4f, row), Quaternion.Euler(0f, 90f, 0f), transform);
		Querstrebe.transform.localScale = new Vector3(1f, 1f, scaleQuerstrebe);

		//Eintragung der GameObjecte in das DistanceLines-Array damit sie später geloescht werden koennen
		LevelGenerator.DistanceLines[0, row] = StartStange;
		LevelGenerator.DistanceLines[1, row] = EndStange;
		LevelGenerator.DistanceLines[2, row] = Querstrebe;
	}
}
