using UnityEngine;

public class BombSpawner : MonoBehaviour

{
    public LevelGenerator LevelGenerator;
    public GameObject Bomb_Prefab;
    public PlayerSpawner PlayerSpawner;

    public void SpawnBomb(xzPosition bombPosition, int id)
    {
        PlayerScript player = PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>();

        if(LevelGenerator.AllGameObjects[bombPosition.x, bombPosition.z] == null || LevelGenerator.AllGameObjects[bombPosition.x, bombPosition.z].gameObject.tag == "Player")
        {
            
            player.setAvaibleBomb(-1);

            GameObject bombeInstanz = Instantiate(Bomb_Prefab, new Vector3(bombPosition.x, 0.4f, bombPosition.z), Quaternion.identity);
            BombScript thisBombeScript = bombeInstanz.GetComponent<BombScript>();

            thisBombeScript.bombTimer = player.getbombTimer();
            thisBombeScript.bombOwnerPlayerID = id;
            thisBombeScript.bombPower = player.getRange();
            thisBombeScript.remoteBomb = player.getRemoteBomb();
            thisBombeScript.tag = "Bombe";

            LevelGenerator.AllGameObjects[bombPosition.x, bombPosition.z] = bombeInstanz;
        }

        player.creatingBomb = false;
    }
}