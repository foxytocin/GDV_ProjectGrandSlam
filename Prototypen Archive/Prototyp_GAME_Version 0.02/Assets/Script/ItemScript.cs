using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(Renderer))]

public class ItemScript : MonoBehaviour {

    public float availableBombs;
    public float playerSpeed;
    public float speedBonus;


	// Update is called once per frame
	void Update () {
		transform.eulerAngles += new Vector3(0, -50f*Time.deltaTime, 0);
	}
}
