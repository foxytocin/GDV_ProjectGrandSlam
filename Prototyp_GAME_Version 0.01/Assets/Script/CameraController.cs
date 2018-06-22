using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour {

    public WorldScript World;
    public List<Transform> players;
    public Transform player;
    public Transform player2;
    private Camera cam;

    private float speed = 4f;
    //private Vector3 vel = new Vector3(0, 0, 0);
    private Vector3 offset;

    private float levelWidth;
    private float levelHeight;

    private float minFOV = 30f;
    private float maxFOV = 10f;



    // Use this for initialization
    void Start ()
    {
        cam = GetComponent<Camera>();
        players.Add(GameObject.Find("Player 1").transform);
        players.Add(GameObject.Find("Player 2").transform);
        //Liste von Spielern direkt bekommen und drauf zugreifen
        //getScript...player[]
        //....
        levelWidth = World.levelBreite;
        levelHeight = World.levelTiefe;
        this.transform.position = new Vector3(levelWidth / 2, levelWidth * (levelHeight * 0.07f), levelHeight / 2);
        offset = new Vector3(0, levelWidth * (levelHeight * 0.07f) , 0);
    }

    private void LateUpdate()
    {
        if(players.Count == 0)
        {
            return;
        }
        
        CameraMoving();
        CameraZooming();
        
    }

    void CameraMoving()
    {
        Vector3 centerPoint = CalcCenterPoint();

        Vector3 endPos = centerPoint + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
        //Vector3 smoothPos2 = Vector3.SmoothDamp(transform.position, endPos, ref vel, speed/10 * Time.deltaTime);
        
        transform.position = smoothPos;
        //transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
    }

    void CameraZooming()
    {
        float zoom = Mathf.Lerp(maxFOV, minFOV, GetGreatestDistance() / (levelWidth + 1));
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime / 3);
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
                float dist = Vector3.Distance(players[i].position, players[j].position);
                if(dist > maxDist)
                {
                    maxDist = dist;
                }
            }
        }
        return maxDist;

        /*
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.size.x;
    */
    }

    Vector3 CalcCenterPoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }
        Vector3 center = Vector3.zero;
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = i + 1; j < players.Count; j++)
            {
                center = players[j].position - players[i].position;
            }
        }
        return players[0].position + 0.5f * center;

        /*
        if(players.Count == 1)
        {
            return players[0].position;
        }
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for(int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.center;
    */
    }
}
