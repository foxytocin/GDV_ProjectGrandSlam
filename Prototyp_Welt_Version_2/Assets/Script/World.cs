using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour {

	public GameObject[,] WorldArray;
	public int ColumnLength;
 	public int RowHeight;
	public GameObject Wall;
	public GameObject Box;
	public GameObject Bogen;
	public GameObject Boden;
	float angle = 0f;
	public float generationStepDelay;

	void Start ()
	{
		ColumnLength = 31;
		RowHeight = 21;
		WorldArray = new GameObject[ColumnLength,RowHeight];
		Boden = (GameObject)Instantiate(Boden, new Vector3((ColumnLength / 2f) - 0.5f, -0.5f, (RowHeight / 2f) - 0.5f), Quaternion.identity);
		//Boden.transform.position = new Vector3((ColumnLength / 2f) - 0.5f, -0.5f, (RowHeight / 2f) - 0.5f);
		Boden.transform.localScale = new Vector3((ColumnLength), 1, (RowHeight));

		createWorld();
	}

	void Update ()
	{
		if(Input.GetKeyDown("r"))
		{
			StopAllCoroutines();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void createWorld()
	{
		StartCoroutine(createWalls());
	}


	public IEnumerator createWalls()
	{
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
  	for (int i = 0; i < ColumnLength; i+=2)
   	{
    	for (int j = 0; j < RowHeight; j+=2)
     	{
				yield return delay;
				//Generiert die Waende (jedes zweite Feld).
				WorldArray[i,j] = Instantiate(Wall, new Vector3(i, 0, j), Quaternion.Euler(0, angle, 0));

				//Generiert eine Saeule
				if(((Mathf.Round(Random.value * 10)) % 4 == 0))
				{
					Instantiate(Wall, new Vector3(i, 1f, j), Quaternion.Euler(0, angle, 0));
					angle += 90f;
				}

				//Generiert einen Bogen: Nicht Teil des Arrays, da es keinen Einfluss auf das Spiel hat.
					if(((Mathf.Round(Random.value * 10)) == 0) && ((i > 1) && (j > 1)) && ((i < ColumnLength - 2) && (j < RowHeight -2)))
				{
					Instantiate(Bogen, new Vector3(i, 0.5f, j), Quaternion.Euler(0, angle, 0));
					angle += 90f;
				}
			}
		}
		StartCoroutine(createBoxes());
	}


	public IEnumerator createBoxes()
	{
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		for (int i = 0; i < ColumnLength; i++)
	 	{
	  	for (int j = 0; j < RowHeight; j++)
	   	{
				//if(WorldArray[i,j] == null)
				if((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0) || (j % 2 != 0))
				{
					if((Mathf.Round(Random.value * 10)) % 4 == 0)
					{
						yield return delay;
						WorldArray[i,j] = Instantiate(Box, new Vector3(i, 0, j), Quaternion.identity);
					}
				}
			}
  	}
	}
}
