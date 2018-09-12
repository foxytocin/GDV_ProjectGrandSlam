using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject SpawnBomb(int bombXPos, float bombYPos, int bombZPos, int playerId, int bombPower, float bombTimer, bool remoteBomb, bool bombrain, Color playerColor)
    {
        // Erlaubt das Legen einer Bombe, wenn das Feld frei (null) ist, oder der Spieler ("Player") selbst sich auf diesem befindet.
        GameObject bombeInstanz = ObjectPooler.Instance.SpawnFromPool("Bombe", new Vector3(bombXPos, bombYPos, bombZPos), Quaternion.identity);
        BombScript thisBombeScript = bombeInstanz.GetComponent<BombScript>();

        // PlayerStats werden auf die gelegte Bombe uebertragen, damit sie ihr individuelles Verhalten bekommt.
        thisBombeScript.bombTimer = bombTimer;
        thisBombeScript.bombOwnerPlayerID = playerId;
        thisBombeScript.bombPower = bombPower;
        thisBombeScript.remoteBomb = remoteBomb;
        thisBombeScript.bombrain = bombrain;
        thisBombeScript.playerColor = playerColor;

        StartCoroutine(thisBombeScript.bombAnimation());

        return bombeInstanz;
    }
}