using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private CameraScroller cameraScroller;
    public List<GameObject> livingPlayers;
    private PlayerSpawner playerSpawner;
    public Vector3 centerPoint;

    private void Start()
    {
        cameraScroller = GameObject.Find("CameraScroller").GetComponent<CameraScroller>();
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        livingPlayers = playerSpawner.playerList;
        foreach (GameObject player in livingPlayers)
        {
            if(player.GetComponent<PlayerScript>().getALife() == false)
            {
                livingPlayers.Remove(player);
                break;
            }
        }
        //Debug.Log(livingPlayers.Count);
        centerPoint = CalcCenterPoint();

        Vector3 local = transform.InverseTransformPoint(centerPoint);
        //transform.localPosition = new Vector3(centerPoint.x, 0, Mathf.Clamp(local.z / 2f, -4f, +4f));


        //float x = Mathf.Lerp(transform.localPosition.x, centerPoint.x, 4f * Time.deltaTime);
        //float z = Mathf.Lerp(transform.localPosition.z, Mathf.Clamp(local.z / 2f, -4f, +4f), 4f * Time.deltaTime);
        float x = centerPoint.x;
        float z = Mathf.Clamp(local.z / 2f, -4f, 4f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(x, 0f, z), 4f * Time.deltaTime);
    }

    public Vector3 CalcCenterPoint()
    {
        int numPlayers = livingPlayers.Count;
        Vector3 center = Vector3.zero;
        
        if (numPlayers == 1)
        {
            return livingPlayers[0].transform.position;
        }
        else if (numPlayers == 2)
        {
            center = livingPlayers[1].transform.position - livingPlayers[0].transform.position;
            return livingPlayers[0].transform.position + 0.5f * center;

        }
        else if (numPlayers == 3)
        {
            //Min und Max Values
            List<float> xPos = new List<float>();
            List<float> zPos = new List<float>();

            foreach (GameObject player in livingPlayers)
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
        else
        {
            List<float> xPos = new List<float>();
            List<float> zPos = new List<float>();

            foreach (GameObject player in livingPlayers)
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