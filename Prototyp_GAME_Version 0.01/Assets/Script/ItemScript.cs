using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	public WorldScript World;
	public float availableBombs;
	public float playerSpeed;
	public float itemColor;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.eulerAngles += new Vector3(0, -50f*Time.deltaTime, 0);
	}
}
