using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("t"))
        {
            GameObject tmpEnemy = Instantiate(enemyPrefab, new Vector3(5f, 0.43f, 5f), Quaternion.identity);

            // Der Player bekommt seinen Namen
            tmpEnemy.name = "Enemy";
            // Der Player bekommt sein Tag
            tmpEnemy.tag = "Player";
        }
	}
}
