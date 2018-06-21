using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public WorldScript World;
    public GameObject player;

    private float speed = 4f;
    private Vector3 offset = new Vector3(0, 14, 0);


    // Use this for initialization
    void Start ()
    {
      player = GameObject.Find("Player 1");
      float x = World.levelBreite;
      float z = World.levelTiefe;
      this.transform.position = new Vector3(x / 2, x * (z * 0.07f), z / 2);
    }

    private void LateUpdate()
    {
      Vector3 endPos = player.transform.position + offset;
      Vector3 smoothPos = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
      if(Time.fixedTime > 5)
      {
          transform.position = smoothPos;
          //transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
      }
    }
}
