using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale += new Vector3(6f * Time.deltaTime, 6f * Time.deltaTime, 6f * Time.deltaTime);
	}
}
