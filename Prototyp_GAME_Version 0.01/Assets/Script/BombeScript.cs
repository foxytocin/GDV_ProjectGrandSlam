using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombeScript : MonoBehaviour
{
    public int bombOwnerPlayerID;
    public bool remoteBomb = false;
    public int bombPower = 1;
    public int bombTimer = 3;
    private float startTime;
    public List<GameObject> playerList;

    //Zeitpunkt an dem die Bombe erzeugt wurde
    void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 70f * Time.deltaTime, 0);

        if ((bombTimer < (Time.time - startTime)) && !remoteBomb)
        {
            CallExplode();
        }
    }

    public void CallExplode()
    {
        int x = (int)gameObject.transform.position.x;
        int z = (int)gameObject.transform.position.z;

        Destroy(gameObject);
        playerList[bombOwnerPlayerID].GetComponent<PlayerScript>().setAvaibleBomb(1);
        //Debug.Log("MAPDESTROYER EXPLODE WIRD AUFGERUFEN mit Position: " +x+ " / " +z);
        FindObjectOfType<MapDestroyer>().Explode(x, z);

    }
}