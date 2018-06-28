using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{

    public LevelGenerator LevelGenerator;
    public GameObject Bomb_Prefab;
    public PlayerSpawner PlayerSpawner;

    public void SpawnBomb(int id)
    {
        GameObject player = PlayerSpawner.playerList[id].gameObject;
        int xPos = (int)Mathf.Round(player.transform.position.x);
        int zPos = (int)Mathf.Round(player.transform.position.z);
        
        //if (!world.WorldArray[xPos, zPos].name.Contains("Bombe") && !world.WorldArray[xPos, zPos].name.Contains("Wand") && !world.WorldArray[xPos, zPos].name.Contains("Kiste"))
        //{
            player.GetComponent<PlayerScript>().setAvaibleBomb(-1);

            BombScript thisBombeScript = Instantiate(Bomb_Prefab, new Vector3(xPos, 0.4f, zPos), Quaternion.identity).GetComponent<BombScript>();

            thisBombeScript.bombTimer = player.GetComponent<PlayerScript>().getbombTimer();
            thisBombeScript.name = "Bombe_" +id;
            thisBombeScript.bombOwnerPlayerID = id;
            thisBombeScript.playerList = player.GetComponent<PlayerScript>().getPlayerList();
            thisBombeScript.bombPower = player.GetComponent<PlayerScript>().getRange();
            thisBombeScript.remoteBomb = player.GetComponent<PlayerScript>().getRemoteBomb();

            //world.WorldArray[xPos, zPos] = bombeInstanz;
            //angle += angle;
        //}

    }
}
