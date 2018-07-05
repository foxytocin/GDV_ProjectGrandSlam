using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript2 : MonoBehaviour {

    public GameObject Explosion;

	// Use this for initialization
	void Start () {
        Instantiate(Explosion, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
