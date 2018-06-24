using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{

    public WorldScript world;
    public GameObject bombe_Prefab;
    public PlayerSpawner PlayerSpawner;

    public void SpawnBomb(int id)
    {
        GameObject player = PlayerSpawner.playerList[id].gameObject;
        int xPos = (int)Mathf.Round(player.transform.position.x);
        int zPos = (int)Mathf.Round(player.transform.position.z);
        
        if (world.WorldArray[xPos, zPos] == null)
        {
            player.GetComponent<PlayerScript>().setAvaibleBomb(-1);

            GameObject bombeInstanz;
            bombeInstanz = Instantiate(bombe_Prefab, new Vector3(xPos, 0, zPos), Quaternion.identity);

            BombeScript thisBombeScript = bombeInstanz.GetComponent<BombeScript>();

            thisBombeScript.bombTimer = player.GetComponent<PlayerScript>().getbombTimer();
            thisBombeScript.name = "Bombe_Player_" +id;
            thisBombeScript.bombOwnerPlayerID = id;
            thisBombeScript.playerList = player.GetComponent<PlayerScript>().getPlayerList();
            thisBombeScript.bombPower = player.GetComponent<PlayerScript>().getRange();
            thisBombeScript.remoteBomb = player.GetComponent<PlayerScript>().getRemoteBomb();

            world.WorldArray[xPos, zPos] = bombeInstanz;
            Debug.Log("Player " +id+ " hat Bombe gelegt: " +xPos+ " / " +zPos);
            Debug.Log("Prüfung Bombe: " +world.WorldArray[xPos, zPos]);
            //angle += angle;
        }

    }
}
