using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    Vector3 targetPosition;

    GameObject target;

	void Start () {
        target = GameObject.FindWithTag("MainCamera");
	}
	

    void Update()
    {
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
		
