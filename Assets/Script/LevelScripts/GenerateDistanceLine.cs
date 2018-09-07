using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDistanceLine : MonoBehaviour {

public LevelGenerator LevelGenerator;
ObjectPooler objectPooler;
private float startPoint;
private float endPoint;
private float centerPoint;
private float scaleQuerstrebe;
public bool generateGlowStangen;
private int playerOffset = 4;

private Color Percent0 = new Color32(83, 170, 39, 1);
private Color Percent10 = new Color32(105, 170, 39, 1);
private Color Percent20 = new Color32(127, 170, 39, 1);
private Color Percent30 = new Color32(148, 170, 39, 1);
private Color Percent40 = new Color32(170, 170, 39, 1);
private Color Percent50 = new Color32(170, 148, 39, 1);
private Color Percent60 = new Color32(170, 127, 39, 1);
private Color Percent70 = new Color32(170, 105, 39, 1);
private Color Percent80 = new Color32(170, 83, 39, 1);
private Color Percent90 = new Color32(170, 61, 39, 1);
private Color Percent100 = new Color32(170, 39, 39, 1);


	void Start()
	{
		objectPooler = ObjectPooler.Instance;
		generateGlowStangen = false;
		startPoint = 0;
		endPoint = 0;
	}

	public void createDistanceLine(int row, bool normalline)
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
		centerPoint = startPoint + (endPoint - startPoint) / 2f;
		scaleQuerstrebe = endPoint - startPoint - 1f;

		//Generierung der GameObjecte mit richtiger Rotation und Scalierung entsprechend dem Abstand der Pfosten
		GameObject StartStange = objectPooler.SpawnFromPool("Stange", new Vector3(startPoint, 0.5f, row), Quaternion.Euler(0f, -90f, 0f));
		GameObject EndStange = objectPooler.SpawnFromPool("Stange", new Vector3(endPoint, 0.5f, row), Quaternion.Euler(0f, 90f, 0f));
		GameObject Querstrebe = objectPooler.SpawnFromPool("Querstrebe", new Vector3(centerPoint, 3.5f, row), Quaternion.Euler(0f, 90f, 0f));
		Querstrebe.transform.localScale = new Vector3(1f, 1f, scaleQuerstrebe);
		
		//Zuweisung der Farbe passend zum Schwierigkeitsgrad (KisteMenge)
		setEmissionAndColor(StartStange, normalline);
		setEmissionAndColor(EndStange, normalline);
		setEmissionAndColor(Querstrebe, normalline);

		//Eintragung der GameObjecte in das DistanceLines-Array damit sie später geloescht werden koennen
		LevelGenerator.DistanceLines[0, row] = StartStange;
		LevelGenerator.DistanceLines[1, row] = EndStange;
		LevelGenerator.DistanceLines[2, row] = Querstrebe;

		createMeterSchild(startPoint, endPoint, centerPoint, row, normalline);

	}

	private void setEmissionAndColor(GameObject go, bool normalline)
	{
			Material material = go.GetComponent<Renderer>().material;
			Color baseColor;
			
			if(normalline)
			{
				baseColor = setMaterialColor();
			} else {

				baseColor = new Color32(68, 115, 197, 255);
			}
			
			Color emissionColor = baseColor * Mathf.LinearToGammaSpace(1.6f);

			if(generateGlowStangen)
			{
				material.color = baseColor;
				material.SetColor("_EmissionColor", emissionColor);
				material.EnableKeyword("_EMISSION");
			} else {
				material.DisableKeyword("_EMISSION");
				material.color = baseColor;
			}

	}


	//Setzt die Farbe DistanceLine passend zum aktuellen Schwierigkeitsgrad (KistenMenge)
	public Color setMaterialColor()
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
			case 60:
				return Percent100;
			default:
				break;
		}
	return Percent0;
	}


	//Generiert abhängig von der Breite des Levelabschnittes 1, 2 oder 3 MeterSchilder
	void createMeterSchild(float startPoint, float endPoint, float centerPoint, int row, bool normalline)
	{
		float distance = endPoint - startPoint;
		float leftMiddle = startPoint - 1 + (centerPoint - startPoint) / 2f;
		float rightMiddle = centerPoint + (endPoint - centerPoint) / 2f;

		if(distance >= 26)
		{
			GameObject MeterSchild1 = objectPooler.SpawnFromPool("MeterSchild", new Vector3(centerPoint, 3.5f, row), Quaternion.Euler(20f, 0f, 0f));
			MeterSchild1.GetComponent<MeterSchild>().setMeter(row - playerOffset, normalline);
			LevelGenerator.DistanceLines[3, row] = MeterSchild1;

			GameObject MeterSchild2 = objectPooler.SpawnFromPool("MeterSchild", new Vector3(leftMiddle - 1, 3.5f, row), Quaternion.Euler(20f, 0f, 0f));
			MeterSchild2.GetComponent<MeterSchild>().setMeter(row - playerOffset, normalline);
			LevelGenerator.DistanceLines[4, row] = MeterSchild2;

			GameObject MeterSchild3 = objectPooler.SpawnFromPool("MeterSchild", new Vector3(rightMiddle + 2, 3.5f, row), Quaternion.Euler(20f, 0f, 0f));
			MeterSchild3.GetComponent<MeterSchild>().setMeter(row - playerOffset, normalline);
			LevelGenerator.DistanceLines[5, row] = MeterSchild3;

			if(generateGlowStangen)
			{
				MeterSchild1.transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
				MeterSchild2.transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
				MeterSchild3.transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
			} else {

				MeterSchild1.transform.GetChild(0).GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
				MeterSchild2.transform.GetChild(0).GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
				MeterSchild3.transform.GetChild(0).GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
			}
		}
		else if(distance >= 13)
		{
			GameObject MeterSchild1 = objectPooler.SpawnFromPool("MeterSchild", new Vector3(leftMiddle, 3.5f, row), Quaternion.Euler(20f, 0f, 0f));
			MeterSchild1.GetComponent<MeterSchild>().setMeter(row - playerOffset, normalline);
			LevelGenerator.DistanceLines[3, row] = MeterSchild1;

			GameObject MeterSchild2 = objectPooler.SpawnFromPool("MeterSchild", new Vector3(rightMiddle, 3.5f, row), Quaternion.Euler(20f, 0f, 0f));
			MeterSchild2.GetComponent<MeterSchild>().setMeter(row - playerOffset, normalline);
			LevelGenerator.DistanceLines[4, row] = MeterSchild2;

			if(generateGlowStangen)
			{
				MeterSchild1.transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
				MeterSchild2.transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
			} else {

				MeterSchild1.transform.GetChild(0).GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
				MeterSchild2.transform.GetChild(0).GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
			}
		}
		else
		{
			GameObject MeterSchild1 = objectPooler.SpawnFromPool("MeterSchild", new Vector3(centerPoint, 3.5f, row), Quaternion.Euler(20f, 0f, 0f));
			MeterSchild1.GetComponent<MeterSchild>().setMeter(row - playerOffset, normalline);
			LevelGenerator.DistanceLines[3, row] = MeterSchild1;

			if(generateGlowStangen)
			{
				MeterSchild1.transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
			} else {

				MeterSchild1.transform.GetChild(0).GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
			}
		}
	}
}
