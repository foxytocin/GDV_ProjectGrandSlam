using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour {

	public void positon(Vector3 center)
	{
		Vector3 newPos = transform.position;
		newPos.z = center.z + 3f;
		transform.position = newPos;
	}

}
