using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private CameraScroller cameraScroller;
    private MiniMapCam miniMapCam;
    public Vector3[] positions;
    private PlayerSpawner playerSpawner;
    public int numPlayers;
    public Vector3 centerPoint;
    private RulesScript rulesScript;

    private List<float> xPos;
    private List<float> zPos;
    private float maxX;
    private float maxZ;
    private float minX;
    private float minZ;
    private Vector3 minPos;
    private Vector3 maxPos;
    public int roundPlayers;
    private GameManager gameManager;
    public bool nextRoundAnimation;


    private void Awake()
    {
        positions = new Vector3[4];
        cameraScroller = FindObjectOfType<CameraScroller>();
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        rulesScript = FindObjectOfType<RulesScript>();
        miniMapCam = FindObjectOfType<MiniMapCam>();
        gameManager = FindObjectOfType<GameManager>();
        nextRoundAnimation = false;
    }

    void Update()
    {
        if(!nextRoundAnimation)
        {
            centerPoint = CalcCenterPoint();
            miniMapCam.positon(centerPoint);

            Vector3 local = transform.InverseTransformPoint(centerPoint);
            float z = Mathf.Clamp(local.z / 2f, -4f, 4f);

            Vector3 targetPos = new Vector3(centerPoint.x - 12f, 0f, z - OffsetAccordingToMaxDistance());

            //Dynamischer Levelspeed
            cameraScroller.scrollSpeed = (((z + z + 5f - OffsetAccordingToMaxDistance() * 0.9f) / 3f) + 0.5f);
            if (!rulesScript.resultScreen.activeSelf)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 4f * Time.deltaTime);
            }
        }       
        
    }

    public Vector3 CalcCenterPoint()
    {        
        numPlayers = rulesScript.playerIsLive;
        Vector3 center = Vector3.zero;
   
        roundPlayers = playerSpawner.playerList.Count;
        if(gameManager.gameStatePlay && numPlayers == 0)
        {
            return new Vector3(200, 0, 0);
        }
        else if (numPlayers == 1)
        {
            foreach(GameObject go in playerSpawner.playerList)
            {
                if(go != null)
                {
                    return new Vector3(go.transform.position.x, 0, go.transform.position.z);
                }
            }
        } else {

            xPos = new List<float>();
            zPos = new List<float>();

            foreach (GameObject go in playerSpawner.playerList)
            {
                if (go != null)
                {
                    xPos.Add(go.transform.position.x);
                    zPos.Add(go.transform.position.z);
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
        }
        
        return center;
    }

    public float MaxZDistancePlayers()
    {
        return maxZ - minZ;
    }
    public float OffsetAccordingToMaxDistance()
    {
        return Mathf.Clamp(MaxZDistancePlayers() * MaxZDistancePlayers() / 15f, 0f, 8f);
    }


    public void RestartCameraMovement(bool mainmenu)
    {
        nextRoundAnimation = true;
        Vector3 target = new Vector3(0f, transform.position.y, transform.position.z) + new Vector3(15f, 10f, -15f);
        if(mainmenu)
        {
            StartCoroutine(HoldCamera(target, 3.5f));
        }
        else
        {
            StartCoroutine(HoldCamera(target, 0.7f));
        }
        
    }

    private IEnumerator HoldCamera(Vector3 target, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSecondsRealtime(0.7f);
        nextRoundAnimation = false;
    }
}