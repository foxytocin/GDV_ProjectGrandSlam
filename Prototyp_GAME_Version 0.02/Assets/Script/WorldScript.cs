using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScript : MonoBehaviour {

	public GameObject[,] WorldArray;
	public int levelBreite;
 	public int levelTiefe;
	public GameObject levelWand;
	public GameObject levelKiste;
	public GameObject levelBogen;
	public GameObject levelBoden;
	public GameObject Item_SpeedBoost_Prefab;
    public GameObject Item_BombPowerUp;
	public int kistenCounter;
	float elementRotation = 0f;
	float generationStepDelay = 0.01f;

	void Awake()
	{
		levelBreite = 15;
		levelTiefe = 15;
	}

	void Start ()
	{
		levelBreite = levelBreite * 2 - 1;
		levelTiefe = levelTiefe * 2 - 1;
		WorldArray = new GameObject[levelBreite,levelTiefe];
		levelBoden = Instantiate(levelBoden, new Vector3((levelBreite / 2f) - 0.5f, -0.5f, (levelTiefe / 2f) - 0.5f), Quaternion.identity);
		levelBoden.transform.localScale = new Vector3((levelBreite), 1, (levelTiefe));

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
        //StartCoroutine(createWalls());
        createWalls();
        StartCoroutine(createBoxes());
	}


	//public IEnumerator createWalls()
    public void createWalls()
	{
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
  	for (int i = 0; i < levelBreite; i+=2)
   	{
    	for (int j = 0; j < levelTiefe; j+=2)
     	{
				//yield return delay;
				//Generiert die Waende (jedes zweite Feld).
				GameObject wand;
				wand = Instantiate(levelWand, new Vector3(i, 0, j), Quaternion.Euler(0, elementRotation, 0));
				wand.name = "Wand";
				wand.transform.parent = transform;
				WorldArray[i,j] = wand;

				//Generiert eine Saeule
				if(((Mathf.Round(Random.value * 10)) % 4 == 0))
				{
					GameObject wand_saule;
					wand_saule = Instantiate(levelWand, new Vector3(i, 1f, j), Quaternion.Euler(0, elementRotation, 0));
					wand_saule.name = "Wand_Saeule";
					wand_saule.transform.parent = transform;
					WorldArray[i,j] = wand_saule;
					elementRotation += 90f;
				}

				//Generiert einen Bogen: Nicht Teil des Arrays, da es keinen Einfluss auf das Spiel hat.
				if(((Mathf.Round(Random.value * 10)) == 0) && ((i > 1) && (j > 1)) && ((i < levelBreite - 2) && (j < levelTiefe - 2)))
				{
					// Instantiate(levelBogen, new Vector3(i, 0.5f, j), Quaternion.Euler(0, elementRotation, 0));
					// elementRotation += 90f;
					if(((Mathf.Round(Random.value * 10)) % 2 == 0))
					{
						Instantiate(levelWand, new Vector3(i, 1, j), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i, 2, j), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i+1, 2, j), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i+2, 1, j), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i+2, 2, j), Quaternion.Euler(0, elementRotation, 0));
					} else {
						Instantiate(levelWand, new Vector3(i, 1, j), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i, 2, j), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i, 2, j+1), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i, 1, j+2), Quaternion.Euler(0, elementRotation, 0));
						Instantiate(levelWand, new Vector3(i, 2, j+2), Quaternion.Euler(0, elementRotation, 0));
					}

				}
			}
		}
		//StartCoroutine(createBoxes());
	}


	public IEnumerator createBoxes()
	{
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		for (int i = 0; i < levelBreite; i++)
	 	{
	  	for (int j = 0; j < levelTiefe; j++)
	   	{
				//if(WorldArray[i,j] == null)
				if(((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0) || (j % 2 != 0)) && ((i != 1) || (j != 1)))
				{
					if((Mathf.Round(Random.value * 10)) % 3 == 0)
					{
						yield return delay;
						kistenCounter++;
						GameObject kiste;
						kiste = Instantiate(levelKiste, new Vector3(i, 0, j), Quaternion.Euler(0, elementRotation, 0));
						kiste.transform.parent = transform;
						kiste.name = "Kiste";
						WorldArray[i,j] = kiste;
						elementRotation += 90f;
					}
				}
			}
  	    }
		spawnItem();
	}

	void spawnItem() {
		for (int i = 0; i < levelBreite; i++)
	 	{
	  	for (int j = 0; j < levelTiefe; j++)
	   	{
				//if(WorldArray[i,j] == null)
				if(((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0) || (j % 2 != 0)) && ((i != 1) || (j != 1)) && WorldArray[i,j] == null)
				{
					if((Mathf.Round(Random.value * 10)) == 0)
					{
                        if ((Mathf.Round(Random.value * 10)) % 2 == 0) {
                            GameObject item;
                            item = Instantiate(Item_BombPowerUp, new Vector3(i, 0, j), Quaternion.Euler(0, elementRotation, 0));
                            item.transform.parent = transform;
                            item.name = "Item_BombPowerUp";
                            WorldArray[i, j] = item;
                            elementRotation += 90f;
                        } else {
                            GameObject item;
                            item = Instantiate(Item_SpeedBoost_Prefab, new Vector3(i, 0, j), Quaternion.Euler(0, elementRotation, 0));
                            item.transform.parent = transform;
                            item.name = "Item_SpeedBoost";
                            WorldArray[i, j] = item;
                            elementRotation += 90f;
                        }
					}
				}
			}
		}
	}
}