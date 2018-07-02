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

        PlayerScript player = PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>();
        int xPos = (int)Mathf.Round(player.transform.position.x);
        int zPos = (int)Mathf.Round(player.transform.position.z);
        
        //if (!world.WorldArray[xPos, zPos].name.Contains("Bombe") && !world.WorldArray[xPos, zPos].name.Contains("Wand") && !world.WorldArray[xPos, zPos].name.Contains("Kiste"))
        //{
            player.setAvaibleBomb(-1);

            GameObject bombeInstanz = Instantiate(Bomb_Prefab, new Vector3(xPos, 0.4f, zPos), Quaternion.identity);
            BombScript thisBombeScript = bombeInstanz.GetComponent<BombScript>();

            thisBombeScript.bombTimer = player.getbombTimer();
            thisBombeScript.name = "Bombe_" +id;
            thisBombeScript.bombOwnerPlayerID = id;
            thisBombeScript.playerList = player.getPlayerList();
            thisBombeScript.bombPower = player.getRange();
            thisBombeScript.remoteBomb = player.getRemoteBomb();

            Debug.Log("Bombe gelegt: " +thisBombeScript.transform.position);

            player.creatingBomb = false;

            LevelGenerator.AllGameObjects[xPos,zPos] = bombeInstanz;
            //world.WorldArray[xPos, zPos] = bombeInstanz;
            //angle += angle;
        //}

    }
}
