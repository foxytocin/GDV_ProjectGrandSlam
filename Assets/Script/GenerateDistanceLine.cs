using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDistanceLine : MonoBehaviour {

public LevelGenerator LevelGenerator;
public GameObject StangePrefab;
public GameObject QuerstrebePrefab;
public GameObject MeterSchildPrefab;
private int startPoint;
private int endPoint;
private int centerPoint;
private int scaleQuerstrebe;

private Color32 Percent0 = new Color32(83, 170, 39, 1);
private Color32 Percent10 = new Color32(105, 170, 39, 1);
private Color32 Percent20 = new Color32(127, 170, 39, 1);
private Color32 Percent30 = new Color32(148, 170, 39, 1);
private Color32 Percent40 = new Color32(170, 170, 39, 1);
private Color32 Percent50 = new Color32(170, 148, 39, 1);
private Color32 Percent60 = new Color32(170, 127, 39, 1);
private Color32 Percent70 = new Color32(170, 105, 39, 1);
private Color32 Percent80 = new Color32(170, 83, 39, 1);
private Color32 Percent90 = new Color32(170, 61, 39, 1);
private Color32 Percent100 = new Color32(170, 39, 39, 1);

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
		GameObject StartStange = Instantiate(StangePrefab, new Vector3(startPoint, 0.5f, row), Quaternion.Euler(0f, -90f, 0f), transform);
		GameObject EndStange = Instantiate(StangePrefab, new Vector3(endPoint, 0.5f, row), Quaternion.Euler(0f, 90f, 0f), transform);
		GameObject Querstrebe = Instantiate(QuerstrebePrefab, new Vector3(centerPoint, 3.5f, row), Quaternion.Euler(0f, 90f, 0f), transform);
		Querstrebe.transform.localScale = new Vector3(1f, 1f, scaleQuerstrebe);

		//Zuweisung der Farbe passend zum Schwierigkeitsgrad (KisteMenge)
		StartStange.GetComponent<Renderer>().material.color = setColor();
		EndStange.GetComponent<Renderer>().material.color = setColor();
		Querstrebe.GetComponent<Renderer>().material.color = setColor();

		//Eintragung der GameObjecte in das DistanceLines-Array damit sie später geloescht werden koennen
		LevelGenerator.DistanceLines[0, row] = StartStange;
		LevelGenerator.DistanceLines[1, row] = EndStange;
		LevelGenerator.DistanceLines[2, row] = Querstrebe;

		createMeterSchild(startPoint, endPoint, centerPoint, row);
	}


	//Setzt die Farbe DistanceLine passend zum aktuellen Schwierigkeitsgrad (KistenMenge)
	public Color32 setColor()
	{
		int Difficulty = (int)LevelGenerator.KistenMenge;

		switch(Difficulty)
		{
			case 0:
				return Percent0;
			case 10:
				return Percent10;
			case 15:
				return Percent20;
			case 20:
				return Percent30;
			case 25:
				return Percent40;
			case 30:
				return Percent50;
			case 35:
				return Percent60;
			case 40:
				return Percent70;
			case 45:
				return Percent80;
			case 50:
				return Percent90;
			case 70:
				return Percent100;
			default:
				break;
		}
	return Percent0;
	}


	//Generiert abhängig von der Breite des Levelabschnittes 1, 2 oder 3 MeterSchilder
	void createMeterSchild(int startPoint, int endPoint, int centerPoint, int row)
	{
		int distance = endPoint - startPoint;
		int leftMiddle = startPoint + (centerPoint - startPoint) / 2;
		int rightMiddle = centerPoint + (endPoint - centerPoint) / 2;

		if(distance >= 26)
		{
			GameObject MeterSchild1 = Instantiate(MeterSchildPrefab, new Vector3(centerPoint, 3.5f, row), Quaternion.Euler(20f, 0f, 0f), transform);
			LevelGenerator.DistanceLines[3, row] = MeterSchild1;

			GameObject MeterSchild2 = Instantiate(MeterSchildPrefab, new Vector3(leftMiddle - 1, 3.5f, row), Quaternion.Euler(20f, 0f, 0f), transform);
			LevelGenerator.DistanceLines[4, row] = MeterSchild2;

			GameObject MeterSchild3 = Instantiate(MeterSchildPrefab, new Vector3(rightMiddle + 2, 3.5f, row), Quaternion.Euler(20f, 0f, 0f), transform);
			LevelGenerator.DistanceLines[5, row] = MeterSchild3;
		}
		else if(distance >= 13)
		{
			GameObject MeterSchild1 = Instantiate(MeterSchildPrefab, new Vector3(leftMiddle, 3.5f, row), Quaternion.Euler(20f, 0f, 0f), transform);
			LevelGenerator.DistanceLines[3, row] = MeterSchild1;

			GameObject MeterSchild2 = Instantiate(MeterSchildPrefab, new Vector3(rightMiddle, 3.5f, row), Quaternion.Euler(20f, 0f, 0f), transform);
			LevelGenerator.DistanceLines[4, row] = MeterSchild2;

			GameObject MeterSchild3 = new GameObject();
			LevelGenerator.DistanceLines[5, row] = MeterSchild3;
		}
		else
		{
			GameObject MeterSchild1 = Instantiate(MeterSchildPrefab, new Vector3(centerPoint, 3.5f, row), Quaternion.Euler(20f, 0f, 0f), transform);
			LevelGenerator.DistanceLines[3, row] = MeterSchild1;

			GameObject MeterSchild2 = new GameObject();
			LevelGenerator.DistanceLines[4, row] = MeterSchild2;

			GameObject MeterSchild3 = new GameObject();
			LevelGenerator.DistanceLines[5, row] = MeterSchild3;
		}
	}
}
