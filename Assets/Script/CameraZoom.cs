using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    //private CameraMovement cameraMovement;
    private List<GameObject> players;
    private CameraMovement cm;

    private float fieldWidth;
    //public PlayerSpawner playerSpawner;


    // Update is called once per frame
    private void Start()
    {
        cm = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>();
        fieldWidth = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().AllGameObjects.GetLength(0) - 4;
    }

    void LateUpdate()
    {
        //Debug.Log(fieldWidth);
        //players = playerSpawner.playerList;
        players = cm.livingPlayers;

        //Debug.Log("verticalAxis world and local pos: " + transform.position + transform.localPosition);
        CameraMoving();
    }

    void CameraMoving()
    {
        //float zoom = Mathf.Lerp(fieldWidth/6f, fieldWidth/2f, GetGreatestDistance() / (fieldWidth + 5f));
        float zoom = Mathf.Lerp(3f, 20f, GetGreatestDistance() / (fieldWidth + 5f));
        //LookAt, SmoothFollow SmoothDirection
        //3fach verschachtelte Kamera, getrennt voneinander 

        // BUG Irgendwie zuckt hier was...
        float dist = Mathf.Lerp(transform.localPosition.y, zoom, 4f * Time.deltaTime);
        //Debug.Log("dist: " + dist);
        //Debug.Log("t.pos.y: " + transform.position.y);
        Vector3 offset = new Vector3(0, dist, 0);
        //offset = new Vector3(0, dist, cameraScroller.transform.position.z);
        transform.localPosition = offset;
    }

    float GetGreatestDistance()
    {
        if (players.Count == 1 || players.Count == 0)
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
