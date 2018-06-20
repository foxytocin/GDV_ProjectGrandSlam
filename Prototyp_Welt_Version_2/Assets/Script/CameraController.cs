using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public World World;
    public GameObject player;

    private float speed = 3f;
    private Vector3 offset = new Vector3(0, 6, 0);


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player 1");
        int x = World.ColumnLength;
        int z = World.RowHeight;
        this.transform.position = new Vector3(x / 2, z - (x * 0.03f), z / 2);        
    }

    private void LateUpdate()
    {
        Vector3 endPos = player.transform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
        if(Time.fixedTime > 3)
        {
            transform.position = smoothPos;
            //transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
        }        
    }
}
