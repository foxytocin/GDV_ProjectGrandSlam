using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Dummy : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles += new Vector3(0f, 80f * Time.deltaTime, 0f);
	

	}
}
