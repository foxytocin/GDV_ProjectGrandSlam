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
        centerPoint = CalcCenterPoint();

        Vector3 local = transform.InverseTransformPoint(centerPoint);
        float z = Mathf.Clamp(local.z / 2f, -4f, 4f);

        //Dynamischer Levelspeed
        cameraScroller.LevelGenerator.setLevelSpeed(((z + z + 5f) / 4f) + 0.5f);
        

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(centerPoint.x, 0f, z), 4f * Time.deltaTime);
    }

    public Vector3 CalcCenterPoint()
    {
        numPlayers = playerSpawner.playerList.Count;
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

    public void PlayerPosition(Vector3 pos, int iD)
    {
        positions[iD] = pos;
    }
}