using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public WorldScript World;
    public GameObject player;

    private float speed = 3f;
    private Vector3 offset = new Vector3(0, 10, 0);


    // Use this for initialization
    void Start ()
    {
      player = GameObject.Find("Player 1");
      int x = World.levelBreite * 2 - 1;
      int z = World.levelTiefe * 2 - 1;
      this.transform.position = new Vector3(x / 2, z * (x * 0.08f), z / 2);
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
