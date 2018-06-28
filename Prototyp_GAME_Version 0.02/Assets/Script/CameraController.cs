using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour
{

    public WorldScript World;
    private List<GameObject> players;
    //public Transform player;
    //public Transform player2;
    public PlayerSpawner playerSpawner;
    private Camera cam;
    //private PlayerScript playerScript;

    private float speed = 4f;
    //private Vector3 vel = new Vector3(0, 0, 0);
    private Vector3 offset;

    private float levelWidth;
    private float levelHeight;

    private float maxHeight;
    private float minHeight = 10f;



    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        //players.Add(GameObject.Find("Player_0").transform);
        //players.Add(GameObject.Find("Player_1").transform);
        playerSpawner = GameObject.Find("Player").GetComponent<PlayerSpawner>();


        //Liste von Spielern direkt bekommen und drauf zugreifen
        //playerScript = player.getComponent<PlayerScript>;
        //....
        levelWidth = World.levelBreite;
        levelHeight = World.levelTiefe;
        maxHeight = levelWidth * (levelHeight * 0.04f);
        this.transform.position = new Vector3(levelWidth / 2, maxHeight, levelHeight / 2);

        offset = new Vector3(0, maxHeight, 0);
    }

    private void LateUpdate()
    {
        players = playerSpawner.playerList;

        //Debug.Log(players[0]);

        if (players.Count == 0)
        {

            return;
        }
        if (Time.fixedTime > 3)
        {
            CameraMoving(players.Count);
        }


    }

    void CameraMoving(int numPlayers)
    {
        Vector3 centerPoint = CalcCenterPoint(numPlayers);

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
                float dist = Vector3.Distance(players[i].transform.position, players[j].transform.position);
                if (dist > maxDist)
                {
                    maxDist = dist;
                }
            }
        }
        return maxDist;
    }

    GameObject[] GetGreatestDistanceObjects()
    {
        if (players.Count == 1)
        {
            return null;
        }
        float maxDist = 0f;
        GameObject[] objs = new GameObject[2];
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = i + 1; j < players.Count; j++)
            {
                float dist = Vector3.Distance(players[i].transform.position, players[j].transform.position);
                if (dist > maxDist)
                {
                    maxDist = dist;
                    objs[0] = players[i];
                    objs[1] = players[j];
                }
            }
        }
        return objs;
    }

    Vector3 CalcCenterPoint(int numPlayers)
    {
        Vector3 center = Vector3.zero;
        int count = 0;
        if (numPlayers == 1)
        {
            return players[0].transform.position;
        }
        else if (numPlayers == 2)
        {
            center = players[1].transform.position - players[0].transform.position;
            return players[0].transform.position + 0.5f * center;

        }
        else if (numPlayers == 3)
        {
            //Min und Max Values
            List<float> xPos = new List<float>();
            List<float> zPos = new List<float>();

            foreach (GameObject player in players)
            {
                xPos.Add(player.transform.position.x);
                zPos.Add(player.transform.position.z);
            }

            float maxX = Mathf.Max(xPos.ToArray());
            float maxZ = Mathf.Max(zPos.ToArray());
            float minX = Mathf.Min(xPos.ToArray());
            float minZ = Mathf.Min(zPos.ToArray());

            Vector3 minPos = new Vector3(minX, 0, minZ);
            Vector3 maxPos = new Vector3(maxX, 0, maxZ);

            center = (minPos + maxPos) * 0.5f;
            return center;

            // Größte Distanz und nur abhängig davon Kamera, springt aber..
            /*
            GameObject[] p = GetGreatestDistanceObjects();
            center = p[1].transform.position - p[0].transform.position;
            return p[0].transform.position + 0.5f * center;

            */
            //Mathematisch korrekt, allerdings ist ein Objeckt meistens außerhalb
            /*
            float dist = GetGreatestDistance();
            Debug.Log(dist);

            foreach(GameObject player in players)
            {
                center += player.transform.position;
                count++;
            }
            return center/count;
            */
        }
        else
        {
            List<float> xPos = new List<float>();
            List<float> zPos = new List<float>();

            foreach (GameObject player in players)
            {
                xPos.Add(player.transform.position.x);
                zPos.Add(player.transform.position.z);
            }

            float maxX = Mathf.Max(xPos.ToArray());
            float maxZ = Mathf.Max(zPos.ToArray());
            float minX = Mathf.Min(xPos.ToArray());
            float minZ = Mathf.Min(zPos.ToArray());

            Vector3 minPos = new Vector3(minX, 0, minZ);
            Vector3 maxPos = new Vector3(maxX, 0, maxZ);

            center = (minPos + maxPos) * 0.5f;
            return center;
        }
    }
}
