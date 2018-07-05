using System.Collections.Generic;
using UnityEngine;


public class BombScript : MonoBehaviour
{
    public int bombOwnerPlayerID;
    public int bombPower;
    public int bombTimer;
    public bool remoteBomb;
    private float startTime;
    public List<GameObject> playerList;
    public LevelGenerator levelGenerator;
    public xzPosition bombPosition;

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
        bombPosition = new xzPosition(Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z));
      
        if((int)playerList[bombOwnerPlayerID].gameObject.transform.position.x == bombPosition.x && (int)playerList[bombOwnerPlayerID].gameObject.transform.position.z == bombPosition.z)
        {
            playerList[bombOwnerPlayerID].GetComponent<PlayerScript>().dead(bombOwnerPlayerID);
        }

        Destroy(gameObject);
        playerList[bombOwnerPlayerID].GetComponent<PlayerScript>().setAvaibleBomb(1);
        FindObjectOfType<MapDestroyer>().Explode(bombPosition, bombPower, bombOwnerPlayerID);
    }
}