using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    
    private PlayerSpawner playerSpawner;
    private CameraMovement cm;
    private RulesScript rules;

    private void Start()
    {
        cm = FindObjectOfType<CameraMovement>();
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        rules = FindObjectOfType<RulesScript>();
    }

    void Update()
    {
        if (!rules.resultScreen.activeSelf)
        {
            CameraMoving();
        }
    }

    void CameraMoving()
    {
        float zoom = Mathf.Lerp(3f, 20f, GetGreatestDistance() / 30f);

        float dist = Mathf.Lerp(transform.localPosition.y, zoom + cm.OffsetAccordingToMaxDistance()/3, 0.5f * Time.deltaTime);
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
            foreach (GameObject go in playerSpawner.playerList)
            {
                if (go != null)
                {
                    foreach (GameObject gobj in playerSpawner.playerList)
                    {
                        if (gobj != null)
                        {
                            float dist = Vector3.Distance(go.transform.position, gobj.transform.position);
                            if (dist > maxDist)
                            {
                                maxDist = dist;
                            }
                        }
                    }
                }
            }                            
            return maxDist;
        }   
    }
}
