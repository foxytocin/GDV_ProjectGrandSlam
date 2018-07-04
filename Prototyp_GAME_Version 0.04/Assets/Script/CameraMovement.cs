using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public CameraScroller cameraScroller;
    public List<GameObject> players;
    public PlayerSpawner playerSpawner;

    // Use this for initialization
    void Start()
    {
        playerSpawner = GameObject.Find("Player").GetComponent<PlayerSpawner>();
        cameraScroller = GameObject.Find("CameraScroller").GetComponent<CameraScroller>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        players = playerSpawner.playerList;
        Vector3 centerPoint = CalcCenterPoint(2);
        //Debug.Log("centerPoint world and tolocal:" + centerPoint + transform.InverseTransformPoint(centerPoint));
        //transform.position = centerPoint;

        Vector3 local = transform.InverseTransformPoint(centerPoint);
        transform.localPosition = new Vector3(centerPoint.x, 0, Mathf.Clamp(local.z / 2f, -4f, +4f));
        //transform.Translate(0, 0, 0.5f * Time.deltaTime);
        //transform.localPosition = Center.transform.localPosition;
        //Debug.Log("CenterLocal: " + Center.transform.position);
        //Debug.Log("horizontalAxis world and local pos: " + transform.position + transform.localPosition);
    }

    public Vector3 CalcCenterPoint(int numPlayers)
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
