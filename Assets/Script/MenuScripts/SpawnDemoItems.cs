using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemoItems : MonoBehaviour {

	public GameObject Item_Prefab;
	private GameObject ItemObjekt;
	public List<GameObject> demoItemList;

	public void spawnDemoItems()
	{
		for(int i = 10; i < 27; i++)
		{
			for(int j = 2; j < 27; j++)
			{
				if(Random.value > 0.95f)
				{
					if(i % 2 != 0)
					{
						ItemObjekt = Instantiate(Item_Prefab, new Vector3(j, 0.7f, i), Quaternion.identity, transform);
					} else {
						ItemObjekt = Instantiate(Item_Prefab, new Vector3(j, 0.7f, i), Quaternion.identity, transform);
						j++;

					}
				}
				demoItemList.Add(ItemObjekt);
			}
		}	
	}

	public void cleanDemoItems()
	{
		foreach(GameObject go in demoItemList)
		{
			Destroy(go, Random.value * 3f);
		}

		demoItemList.Clear();
	}

}