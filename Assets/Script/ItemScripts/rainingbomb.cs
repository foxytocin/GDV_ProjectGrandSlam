using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainingbomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (levelGenerator.SecondaryGameObjects1[x, z].gameObject.CompareTag("Boden") && levelGenerator.AllGameObjects[x, z] == null)
        {
            StartCoroutine(BombSound());
            bombego = Instantiate(bombPrefab, new Vector3(x, 5f, z), Quaternion.identity, transform);
            while (bombego.transform.position.y > 0.6)
            {
                bombego.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
                bombego.transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            }
            if (bombego.transform.position.y == 0.6)
            {
                Destroy(bombego);
                levelGenerator.AllGameObjects[x, z] = bombSpawner.SpawnBomb(x, z, 5, 1, 3, false, true, bombraincolor);
            }
        }

    }
}
