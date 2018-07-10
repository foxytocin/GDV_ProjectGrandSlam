using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcamera : MonoBehaviour {

    GameObject target;

	void Start () {
        target = GameObject.Find("Main Camera");
	}
	

    void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
		
