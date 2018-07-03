using UnityEngine;

public class BombSpawner : MonoBehaviour
<<<<<<< HEAD
{
    public LevelGenerator LevelGenerator;
    public GameObject Bomb_Prefab;
    public PlayerSpawner PlayerSpawner;

    public void SpawnBomb(int id)
    {
        PlayerScript player = PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>();
        int xPos = (int)Mathf.Round(player.transform.position.x);
        int zPos = (int)Mathf.Round(player.transform.position.z);

        player.setAvaibleBomb(-1);

        GameObject bombeInstanz = Instantiate(Bomb_Prefab, new Vector3(xPos, 0.4f, zPos), Quaternion.identity);
        BombScript thisBombeScript = bombeInstanz.GetComponent<BombScript>();

        thisBombeScript.playerList = player.getPlayerList();
        thisBombeScript.bombTimer = player.getbombTimer();
        thisBombeScript.bombOwnerPlayerID = id;
        thisBombeScript.bombPower = player.getRange();
        thisBombeScript.remoteBomb = player.getRemoteBomb();
        thisBombeScript.tag = "Bombe";

        player.creatingBomb = false;

        LevelGenerator.AllGameObjects[xPos,zPos] = bombeInstanz;

        if(thisBombeScript.remoteBomb) {
            PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>().remoteBombList.Add(bombeInstanz);
            Debug.Log("Remote Bombe zur Liste hinzugefügt" +player.remoteBombList.Count);
        }
    }
=======
{
    public LevelGenerator LevelGenerator;
    public GameObject Bomb_Prefab;
    public PlayerSpawner PlayerSpawner;

    public void SpawnBomb(int id)
    {
        PlayerScript player = PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>();
        int xPos = (int)Mathf.Round(player.transform.position.x);
        int zPos = (int)Mathf.Round(player.transform.position.z);

        //Debug.Log(LevelGenerator.AllGameObjects[xPos, zPos]);
        if(LevelGenerator.AllGameObjects[xPos, zPos] == null) {
            
            player.setAvaibleBomb(-1);

            GameObject bombeInstanz = Instantiate(Bomb_Prefab, new Vector3(xPos, 0.4f, zPos), Quaternion.identity);
            BombScript thisBombeScript = bombeInstanz.GetComponent<BombScript>();

            thisBombeScript.playerList = player.getPlayerList();
            thisBombeScript.bombTimer = player.getbombTimer();
            thisBombeScript.bombOwnerPlayerID = id;
            thisBombeScript.bombPower = player.getRange();
            thisBombeScript.remoteBomb = player.getRemoteBomb();
            thisBombeScript.tag = "Bombe";

            LevelGenerator.AllGameObjects[xPos, zPos] = bombeInstanz;
        }

        player.creatingBomb = false;
    }
>>>>>>> master
}