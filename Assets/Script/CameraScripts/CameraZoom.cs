using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    
    private Vector3[] players;
    private CameraMovement cm;    

    private void Start()
    {
        cm = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>();
    }

    void LateUpdate()
    {
        players = cm.positions;
        CameraMoving();
    }

    void CameraMoving()
    {
        float zoom = Mathf.Lerp(3f, 20f, GetGreatestDistance() / 30f);
        //LookAt, SmoothFollow SmoothDirection
        //3fach verschachtelte Kamera, getrennt voneinander 

        float dist = Mathf.Lerp(transform.localPosition.y, zoom + cm.OffsetAccordingToMaxDistance()/3, 0.5f * Time.deltaTime);
        //offset = new Vector3(0, dist, cameraScroller.transform.position.z);
        transform.localPosition = new Vector3(0, Mathf.Clamp(dist, 0f, 18f), 0);
    }

    float GetGreatestDistance()
    {
        int numPlayers = cm.numPlayers;
        float maxDist = 0f;

        if(numPlayers == 1)
        {
            return maxDist;
        } else
        {
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = i + 1; j < players.Length; j++)
                {
                    if (players[i].y == 0.45f && players[j].y == 0.45f)
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
        }   
    }
}
