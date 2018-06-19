using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welt_Generierung : MonoBehaviour {

	public float worldsize;
	public GameObject wand;
	public GameObject bogen;
	public GameObject box;
	GameObject plane;
	float rotat = 0f;

	// Use this for initialization
	void Start () {
	worldsize = 10;

		GameObject lightGameObject = new GameObject("The Light1");
    Light lightComp = lightGameObject.AddComponent<Light>();
    lightComp.color = Color.blue;
		lightComp.range = 100;
		lightComp.shadows = LightShadows.Soft;
    lightGameObject.transform.position = new Vector3(worldsize/5f, 5f, worldsize/5f);

		plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.position = new Vector3(worldsize-1f, -0.5f, worldsize-1f);
		plane.transform.localScale = new Vector3((worldsize / 5f), 0, (worldsize / 5f));

		createWorld();
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey("d"))
		{
			Destroy(GameObject.Find("wandWorld"));
		}

		if(Input.GetKeyDown("space"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void createWorld()
	{
		for(float i = 0f; i < (worldsize * 2); i+=2f)
		{
			for(float j = 0f; j < (worldsize * 2); j+=2f)
			{
				GameObject wandWorld = (GameObject)Instantiate(wand);
				wandWorld.transform.position = new Vector3(i, 0, j);
				wandWorld.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				wandWorld.transform.eulerAngles += new Vector3(0, rotat, 0);
				wandWorld.transform.parent = transform;
				wandWorld.name = "wandWorld";
				rotat += 90f;

				if(((Mathf.Round(Random.value * 10)) == 0) && (i != 0) && (j != 0) && (i < (worldsize * 2 - 2)) && (j < (worldsize * 2 - 2)))
				{
					createBogen(i, j);
				}

				float randomNumber = Mathf.Round(Random.value * 10);
				if((randomNumber % 3 == 0) && (i < (worldsize * 2 - 2)) && (j < (worldsize * 2 - 2)))
				{
					if(randomNumber % 2 == 0)
					{
						createBox(i+1, j);
					} else {
						createBox(i, j+1);
					}
				}
			}
		}
	}

	void createBogen(float i, float j)
	{
		GameObject wandWorld = (GameObject)Instantiate(bogen);
		wandWorld.transform.position = new Vector3(i, 0.5f, j);
		wandWorld.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		wandWorld.transform.eulerAngles += new Vector3(0, rotat, 0);
		wandWorld.transform.parent = transform;
		wandWorld.name = "wandBogen";
		if(((Mathf.Round(Random.value * 10)) % 2) == 0) {
			rotat += 90f;
		}
	}

	void createBox(float i, float j)
	{
		GameObject boxWorld = (GameObject)Instantiate(box);
		boxWorld.transform.position = new Vector3(i, 0f, j);
		boxWorld.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		boxWorld.transform.parent = transform;
		boxWorld.name = "boxBogen";
	}
}
