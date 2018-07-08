using UnityEngine;

public class BombSpawner : MonoBehaviour

{
    public LevelGenerator LevelGenerator;
    public GameObject Bomb_Prefab;
    public PlayerSpawner PlayerSpawner;
    private Vector3 bombPosition;

    public void SpawnBomb(Vector3 position, int id)
    {
        //Position der Bombe = Position des Spielers. Damit die Bombe direkt in die Mitte einer Zelle snappt, werden die x- und z-Achsen gerundet.
        bombPosition = position;
        bombPosition.x = Mathf.RoundToInt(bombPosition.x);
        bombPosition.y -= 0.1f;
        bombPosition.z = Mathf.RoundToInt(bombPosition.z);

        PlayerScript player = PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>();

        //Erlaubt das Legen einer Bombe, wenn das Feld frei (null) ist, oder der Spieler ("Player") selbst sich auf diesem befindet.
        if(LevelGenerator.AllGameObjects[(int)bombPosition.x, (int)bombPosition.z] == null || LevelGenerator.AllGameObjects[(int)bombPosition.x, (int)bombPosition.z].gameObject.tag == "Player")
        {
            player.setAvaibleBomb(-1);

            GameObject bombeInstanz = Instantiate(Bomb_Prefab, bombPosition, Quaternion.identity);
            BombScript thisBombeScript = bombeInstanz.GetComponent<BombScript>();

            //Uebertraegt die PlayerStats auf die gelegte Bombe, damit sie ihr individuelles Verhalten bekommt.
            thisBombeScript.bombTimer = player.getbombTimer();
            thisBombeScript.bombOwnerPlayerID = id;
            thisBombeScript.bombPower = player.getRange();
            thisBombeScript.remoteBomb = player.getRemoteBomb();
            thisBombeScript.tag = "Bombe";

            //Traegt die gelegte Bombe im AllGameObject-Array ein, damit die Interaktion mit anderen GameObjecten moeglich ist.
            LevelGenerator.AllGameObjects[(int)bombPosition.x, (int)bombPosition.z] = bombeInstanz;
        }

        //Zum Abschluss der Pruefung ob eine Bombe gelegt werden darf oder nach erfolgreichem Legen der Bombe, wird das erneute bombemlegen wieder erlaubt.
        player.creatingBomb = false;
    }
}