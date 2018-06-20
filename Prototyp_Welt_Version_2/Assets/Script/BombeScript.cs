using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.eulerAngles += new Vector3(0, 50f*Time.deltaTime, 0);
	}
}
