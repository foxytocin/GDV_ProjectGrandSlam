using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	bool moveX = true;
	bool moveZ = true;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		player.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		player.transform.position = new Vector3(1f, 0, 1f);
		player.name = "Player 1";
	}


	float speedMultiply = 0.01f;
	// Update is called once per frame
	void Update () {

		if((Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("s")) && (moveX || moveZ)) {
			speed();
			wallTest(player);
		} else if(speedMultiply > 0.05f) {
			speedMultiply -= 0.05f;
			//Debug.Log("Increase SM: " +speedMultiply);
		} if(speedMultiply < 0.01f) {
			speedMultiply = 0.01f;
			//Debug.Log("Setting SM: " +speedMultiply);
		}

		if(Input.GetKey("a")) {
			//whatsNext(1);
			if(moveX)
				player.transform.Translate(speedMultiply*-1f*Time.deltaTime,0,0);
		}

		if(Input.GetKey("d")) {
			//whatsNext(2);
			if(moveX)
				player.transform.Translate(speedMultiply*1f*Time.deltaTime,0,0);
		}

		if(Input.GetKey("w")) {
			//whatsNext(3);
			if(moveZ)
				player.transform.Translate(0,0,speedMultiply*1f*Time.deltaTime);
		}

		if(Input.GetKey("s")) {
			//whatsNext(4);
			if(moveZ)
				player.transform.Translate(0,0,speedMultiply*-1f*Time.deltaTime);
		}

		if(Input.GetKey("m")) {
			//whatsNext(1);
			Destroy(GameObject.Find("Wall(Clone)"));
		}

		if(Input.GetKey("b")) {
			//whatsNext(1);
			Destroy(GameObject.Find("Box(Clone)"));
		}
	}

	float xPosition;
	float zPosition;
	void wallTest(GameObject player) {
		xPosition = Mathf.Round(player.transform.position.x);
		zPosition = Mathf.Round(player.transform.position.z);

		if(FindObjectOfType<World>().WorldArray[(int)xPosition,(int)zPosition] != null)
		{
			Debug.Log("Object an aktueller Stelle: " +FindObjectOfType<World>().WorldArray[(int)xPosition,(int)zPosition]);
		}	else {
			Debug.Log("Object an aktueller Stelle: Freier Weg");
		}

		// moveZ = xPosition % 2 != 0 ? true : false;
		// moveX = zPosition % 2 != 0 ? true : false;

		// Debug.Log("X-Position: " +moveX);
		// Debug.Log("Z-Position: " +moveZ);
	}

	// void whatsNext(int richtung)
	// {
	// 	if(richtung == 4 && (FindObjectOfType<World>().WorldArray[(int)xPosition,(int)zPosition+1] != null)) {
	// 		moveZ = false;
	// 	} else {
	// 		moveZ = true;
	// 	}
	// 	if(richtung == 3 && (FindObjectOfType<World>().WorldArray[(int)xPosition,(int)zPosition-1] != null)) {
	// 		moveZ = false;
	// 	} else {
	// 		moveZ = true;
	// 	}
	// 	if(richtung == 2 && (FindObjectOfType<World>().WorldArray[(int)xPosition+1,(int)zPosition] != null)) {
	// 		moveX = false;
	// 	} else {
	// 		moveX = true;
	// 	}
	// 	if(richtung == 1 && (FindObjectOfType<World>().WorldArray[(int)xPosition-1,(int)zPosition] != null)) {
	// 		moveX = false;
	// 	} else {
	// 		moveX = true;
	// 	}
	// }

		// if(Input.GetKey("a")) {
		// 	wallTest(player1);
		// 	bombTest(player1, -1f, 0);
		// 	if(moveX && noBomb)
		// 		player1.transform.Translate(-1f,0,0);
		// 	noBomb = true;
		// }
		//
		// if(Input.GetKey("d")) {
		// 	wallTest(player1);
		// 	bombTest(player1, 1f, 0);
		// 	if(moveX && noBomb)
		// 		player1.transform.Translate(1f,0,0);
		// 	noBomb = true;
		// }
		//
		// if(Input.GetKey("w")) {
		// 	wallTest(player1);
		// 	bombTest(player1, 0, 1f);
		// 	if(moveZ && noBomb)
		// 		player1.transform.Translate(0,0,1f);
		// 	noBomb = true;
		// }
		//
		// if(Input.GetKey("s")) {
		// 	wallTest(player1);
		// 	bombTest(player1, 0, -1f);
		// 	if(moveZ && noBomb)
		// 		player1.transform.Translate(0,0,-1f);
		// 	noBomb = true;
		// }

	void speed() {
		if(speedMultiply < 5.0f) {
				speedMultiply += (speedMultiply / (speedMultiply * 10.0f));
				//Debug.Log("Degreace SM: " +speedMultiply);
		}
	}
}
