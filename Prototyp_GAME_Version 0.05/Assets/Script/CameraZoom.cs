using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    //private CameraMovement cameraMovement;
    public List<GameObject> players;
    public PlayerSpawner playerSpawner;


    // Update is called once per frame
    void Update()
    {

        //players = playerSpawner.playerList;
        players = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>().livingPlayers;

        //Debug.Log("verticalAxis world and local pos: " + transform.position + transform.localPosition);
        CameraMoving(players.Count);
    }

    void CameraMoving(int numPlayers)
    {

        float zoom = Mathf.Lerp(3f, 10f, GetGreatestDistance() / (21 + 1));
        //LookAt, SmoothFollow SmoothDirection
        //3fach verschachtelte Kamera, getrennt voneinander 

        // BUG Irgendwie zuckt hier was...
        float dist = Mathf.Lerp(transform.position.y, zoom, 4f * Time.deltaTime);
        //Debug.Log("dist: " + dist);
        //Debug.Log("t.pos.y: " + transform.position.y);
        Vector3 offset = new Vector3(0, dist, 0);
        //offset = new Vector3(0, dist, cameraScroller.transform.position.z);
        transform.localPosition = offset;

    }

    float GetGreatestDistance()
    {
        if (players.Count == 1)
        {
            return 0;
        }
        float maxDist = 0f;
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = i + 1; j < players.Count; j++)
            {
                float dist = Vector3.Distance(players[i].transform.position, players[j].transform.position);
                if (dist > maxDist)
                {
                    maxDist = dist;
                }
            }
        }
        return maxDist;
    }
}
