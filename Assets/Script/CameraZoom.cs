using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    //private CameraMovement cameraMovement;
    private Vector3[] players;
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
        players = cm.positions;

        //Debug.Log("verticalAxis world and local pos: " + transform.position + transform.localPosition);
        CameraMoving();
    }

    void CameraMoving()
    {
        //float zoom = Mathf.Lerp(fieldWidth/6f, fieldWidth/2f, GetGreatestDistance() / (fieldWidth + 5f));
        float zoom = Mathf.Lerp(3f, 20f, GetGreatestDistance() / 50f);
        //LookAt, SmoothFollow SmoothDirection
        //3fach verschachtelte Kamera, getrennt voneinander 

        Debug.Log(zoom);

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
        int numPlayers = cm.numPlayers;
        float maxDist = 0f;
        switch (numPlayers)
        {
            case 1:
                return maxDist;
            case 2:                
            case 3:
            case 4:                
                for (int i = 0; i < players.Length; i++)
                {
                    for (int j = i + 1; j < players.Length; j++)
                    {
                        if(players[i].y == 0.45f && players[j].y == 0.45f)
                        {
                            float dist = Vector3.Distance(players[i], players[j]);
                            if (dist > maxDist)
                            {
                                maxDist = dist;
                            }
                        }                        
                    }
                }
                return maxDist;
            default: return maxDist;
        }        
    }
}
