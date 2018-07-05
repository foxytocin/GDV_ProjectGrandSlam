using UnityEngine;

public class BombSpawner : MonoBehaviour

{
    public LevelGenerator LevelGenerator;
    public GameObject Bomb_Prefab;
    public PlayerSpawner PlayerSpawner;
    public xzPosition BombPositon;

    public void SpawnBomb(int id)
    {
        PlayerScript player = PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>();
        BombPositon = new xzPosition(Mathf.RoundToInt(player.gameObject.transform.position.x), Mathf.RoundToInt(player.gameObject.transform.position.z));

        if(LevelGenerator.AllGameObjects[BombPositon.x, BombPositon.z] == null || LevelGenerator.AllGameObjects[BombPositon.x, BombPositon.z].gameObject.tag == "Player")
        {
            
            player.setAvaibleBomb(-1);

            GameObject bombeInstanz = Instantiate(Bomb_Prefab, new Vector3(BombPositon.x, 0.4f, BombPositon.z), Quaternion.identity);
            BombScript thisBombeScript = bombeInstanz.GetComponent<BombScript>();

            thisBombeScript.playerList = player.getPlayerList();
            thisBombeScript.bombTimer = player.getbombTimer();
            thisBombeScript.bombOwnerPlayerID = id;
            thisBombeScript.bombPower = player.getRange();
            thisBombeScript.remoteBomb = player.getRemoteBomb();
            thisBombeScript.tag = "Bombe";

            LevelGenerator.AllGameObjects[BombPositon.x, BombPositon.z] = bombeInstanz;
        }

        player.creatingBomb = false;
    }
}