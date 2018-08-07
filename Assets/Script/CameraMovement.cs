using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private CameraScroller cameraScroller;
    public List<GameObject> livingPlayers;
    public Vector3[] positions;
    private PlayerSpawner playerSpawner;
    public int numPlayers;
    public Vector3 centerPoint;

    private void Awake()
    {
        positions = new Vector3[4];
        cameraScroller = GameObject.Find("CameraScroller").GetComponent<CameraScroller>();
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
    }
    void LateUpdate()
    {
        //livingPlayers = playerSpawner.playerList;
        //int i = 0;
        //foreach (GameObject player in livingPlayers)
        //{
        //    if(player.transform.position.y == 0.45f)
        //    {
        //        positions[i] = player.transform.position;
        //        i++;
        //    }
        //    
            /*
            if(player.GetComponent<PlayerScript>().getALife() == false)
            {
                livingPlayers.Remove(player);
                break;
            }
            */
        //}
        //numPlayers = i; 
        //Debug.Log(livingPlayers.Count);
        centerPoint = CalcCenterPoint();
        //Debug.Log(centerPoint);
        //Debug.Log(positions[0] + "" + positions[1] + "" + positions[2]);


        Vector3 local = transform.InverseTransformPoint(centerPoint);
        //transform.localPosition = new Vector3(centerPoint.x, 0, Mathf.Clamp(local.z / 2f, -4f, +4f));


        //float x = Mathf.Lerp(transform.localPosition.x, centerPoint.x, 4f * Time.deltaTime);
        //float z = Mathf.Lerp(transform.localPosition.z, Mathf.Clamp(local.z / 2f, -4f, +4f), 4f * Time.deltaTime);
        //float x = centerPoint.x;
        float z = Mathf.Clamp(local.z / 2f, -4f, 4f);

        //Dynamischer Levelspeed
        cameraScroller.LevelGenerator.setLevelSpeed(((z + z + 5f) / 4f) + 0.5f);
        

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(centerPoint.x, 0f, z), 4f * Time.deltaTime);
    }

    public Vector3 CalcCenterPoint()
    {
        int numPlayers = playerSpawner.playerList.Count;
        Vector3 center = Vector3.zero;

        switch(numPlayers)
        {
            case 1: return positions[0];
            case 2:
                center = positions[1] - positions[0];
                return positions[0] + 0.5f * center;                
            case 3:
                //Min und Max Values
                List<float> xPos = new List<float>();
                List<float> zPos = new List<float>();

                for (int i = 0; i < numPlayers; i++)
                {
                    Debug.Log(positions[i]);
                    if (positions[i].y == 0.45f)
                    {
                        xPos.Add(positions[i].x);
                        zPos.Add(positions[i].z);
                    }
                }

                float maxX = Mathf.Max(xPos.ToArray());
                float maxZ = Mathf.Max(zPos.ToArray());
                float minX = Mathf.Min(xPos.ToArray());
                float minZ = Mathf.Min(zPos.ToArray());

                Vector3 minPos = new Vector3(minX, 0, minZ);
                Vector3 maxPos = new Vector3(maxX, 0, maxZ);

                center = (minPos + maxPos) * 0.5f;
                return center;
            case 4: 
                //Min und Max Values
                xPos = new List<float>();
                zPos = new List<float>();

                foreach (Vector3 player in positions)
                {
                    Debug.Log(player);
                    if (player.y == 0.45f)
                    {
                        xPos.Add(player.x);
                        zPos.Add(player.z);
                    }
                }

                maxX = Mathf.Max(xPos.ToArray());
                maxZ = Mathf.Max(zPos.ToArray());
                minX = Mathf.Min(xPos.ToArray());
                minZ = Mathf.Min(zPos.ToArray());

                minPos = new Vector3(minX, 0, minZ);
                maxPos = new Vector3(maxX, 0, maxZ);

                center = (minPos + maxPos) * 0.5f;
                return center;
            default: return Vector3.zero;
        }
    }

    public Vector3 CalcCenterPointOld()
    {
        //int numPlayers = livingPlayers.Count;
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


    // Prototyp der Position übergaben 
    // Joshua bitte dann noch fertig schreiben!
    // DU bekommst ein Vector und eine ID von mir. 
    // ID ist dafue das du den Vector an die richtige stelle im Array oder der Liste Speichern kannst
    public void PlayerPosition(Vector3 pos, int iD)
    {
        positions[iD] = pos;
    }
}