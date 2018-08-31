﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private CameraScroller cameraScroller;
    private MiniMapCam miniMapCam;
    public List<GameObject> livingPlayers;
    public Vector3[] positions;
    private PlayerSpawner playerSpawner;
    public int numPlayers;
    public Vector3 centerPoint;
    private LevelGenerator levelGenerator;
    private RulesScript rulesScript;

    private List<float> xPos;
    private List<float> zPos;
    private float maxX;
    private float maxZ;
    private float minX;
    private float minZ;
    private Vector3 minPos;
    private Vector3 maxPos;
    private int oldPosZ;


    private void Awake()
    {
        positions = new Vector3[4];
        cameraScroller = FindObjectOfType<CameraScroller>();
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        rulesScript = FindObjectOfType<RulesScript>();
        miniMapCam = FindObjectOfType<MiniMapCam>();
        //levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        
    }
    void Update()
    {
        centerPoint = CalcCenterPoint();
        miniMapCam.positon(centerPoint);
        /*
        for(int i = 0; i < positions.Length; i++)
        {
            //Debug.Log("Anz spieler: " + numPlayers);
            //Debug.Log("Anz spieler2: " + playerSpawner.players);
            Debug.Log("Position " + i + positions[i]);
            Debug.Log("CP: " + centerPoint);

        }
        */        

        Vector3 local = transform.InverseTransformPoint(centerPoint);
        float z = Mathf.Clamp(local.z / 2f, -4f, 4f);

        Vector3 targetPos = new Vector3(centerPoint.x - 12f, 0f, z - OffsetAccordingToMaxDistance());

         //Dynamischer Levelspeed
        cameraScroller.LevelGenerator.setLevelSpeed(((z + z + 5f - OffsetAccordingToMaxDistance()*0.9f) / 4f) + 0.5f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 4f * Time.deltaTime);
    }

    public Vector3 CalcCenterPoint()
    {
        //numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        numPlayers = rulesScript.playerIsLive;
        Vector3 center = Vector3.zero;

        int roundPlayers = playerSpawner.playerList.Count;
        if (numPlayers == 1)
        {
            return GameObject.FindGameObjectWithTag("Player").transform.position;
        }
            
        xPos = new List<float>();
        zPos = new List<float>();
        
        for (int i = 0; i < roundPlayers; i++)
        {
            if (playerSpawner.playerList[i] != null)
            {
                
                xPos.Add(positions[i].x);
                zPos.Add(positions[i].z);
            }
        }
        
        maxX = Mathf.Max(xPos.ToArray());
        maxZ = Mathf.Max(zPos.ToArray());
        minX = Mathf.Min(xPos.ToArray());
        minZ = Mathf.Min(zPos.ToArray());

        minPos = new Vector3(minX, 0, minZ);
        maxPos = new Vector3(maxX, 0, maxZ);

        center = (minPos + maxPos) * 0.5f;
        if (numPlayers == 1)
        {
            Debug.Log("xp:" + xPos.Count);
            Debug.Log("Jetzt einer!");
            Debug.Log(center);
        }
        return center;        
    }

    public Vector3 CalcCenterPointOld()
    {
        //numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        numPlayers = rulesScript.playerIsLive;
        Debug.Log(numPlayers);
        Vector3 center = Vector3.zero;

        switch (numPlayers)
        {
            case 1:
                return positions[0];
            case 2:
                //Min und Max Values
                xPos = new List<float>();
                zPos = new List<float>();
                Debug.Log(playerSpawner.playerList[1]);
                for (int i = 0; i < numPlayers; i++)
                {
                    if (positions[i].y == 0.45f)
                    {
                        xPos.Add(positions[i].x);
                        zPos.Add(positions[i].z);
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

            case 3:
                //Min und Max Values
                xPos = new List<float>();
                zPos = new List<float>();
                Debug.Log(playerSpawner.playerList[1]);
                Debug.Log(playerSpawner.playerList[2]);
                for (int i = 0; i < numPlayers; i++)
                {
                    if (positions[i].y == 0.45f)
                    {
                        xPos.Add(positions[i].x);
                        zPos.Add(positions[i].z);
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
            case 4:
                //Min und Max Values
                xPos = new List<float>();
                zPos = new List<float>();
                Debug.Log(playerSpawner.playerList[1]);
                Debug.Log(playerSpawner.playerList[2]);
                Debug.Log(playerSpawner.playerList[3]);
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

    public float MaxZDistancePlayers()
    {
        return maxZ - minZ;
    }
    public float OffsetAccordingToMaxDistance()
    {
        return Mathf.Clamp(MaxZDistancePlayers() * MaxZDistancePlayers() / 15f, 0f, 8f);
    }

    public void PlayerPosition(Vector3 pos, int iD)
    {
        positions[iD] = pos;
    }    
}