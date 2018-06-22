using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour
{

    public WorldScript World;
    public List<Transform> players;
    public Transform player;
    public Transform player2;
    private Camera cam;
    //private PlayerScript playerScript;

    private float speed = 4f;
    //private Vector3 vel = new Vector3(0, 0, 0);
    private Vector3 offset;

    private float levelWidth;
    private float levelHeight;

    private float maxHeight;
    private float minHeight = 20f;



    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        players.Add(GameObject.Find("Player 1").transform);
        players.Add(GameObject.Find("Player 2").transform);
        //Liste von Spielern direkt bekommen und drauf zugreifen
        //playerScript = player.getComponent<PlayerScript>;
        //....
        levelWidth = World.levelBreite;
        levelHeight = World.levelTiefe;
        maxHeight = levelWidth * (levelHeight * 0.07f);
        this.transform.position = new Vector3(levelWidth / 2, maxHeight, levelHeight / 2);

        offset = new Vector3(0, maxHeight, 0);
    }

    private void LateUpdate()
    {
        if (players.Count == 0)
        {
            return;
        }
        if (Time.fixedTime > 3)
        {
            CameraMoving();
        }


    }

    void CameraMoving()
    {
        Vector3 centerPoint = CalcCenterPoint();

        float zoom = Mathf.Lerp(minHeight, maxHeight, GetGreatestDistance() / (levelWidth + 1));
        float dist = Mathf.Lerp(transform.position.y, zoom, speed * Time.deltaTime);
        offset = new Vector3(0, dist, 0);
        Vector3 endPos = centerPoint + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);

        transform.position = smoothPos;
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
                if (dist > maxDist)
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
