using System.Collections;
using UnityEngine;

public class MapDestroyer : MonoBehaviour
{
    public PlayerSpawner PlayerSpawner;
    public LevelGenerator LevelGenerator;
    public GameObject ExplosionPrefab;
    public GameObject ExplosionPrefab2;
    private IEnumerator coroutinexPositiv;
    private IEnumerator coroutinexNegativ;
    private IEnumerator coroutinezPositiv;
    private IEnumerator coroutinezNegativ;

    //Wird nach Zeitablauf der Bombe durch die BombeScript aufgerufen und emfängt die Position der Bombe

    public void Explode(int x, int z, int bombPower, int id)
    {
        coroutinexPositiv = xPositiv(bombPower, 0.1f, x, z, id);
        coroutinexNegativ = xNegativ(bombPower, 0.1f, x, z, id);
        coroutinezPositiv = zPositiv(bombPower, 0.1f, x, z, id);
        coroutinezNegativ = zNegativ(bombPower, 0.1f, x, z, id);

        StartCoroutine(coroutinexPositiv);
        StartCoroutine(coroutinexNegativ);
        StartCoroutine(coroutinezPositiv);
        StartCoroutine(coroutinezNegativ);
    }


    private IEnumerator xPositiv(int bombPower, float waitTime, int x, int z,int id)
    {
        Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
        Instantiate(ExplosionPrefab2, new Vector3(x, 0.5f, z), Quaternion.identity);
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x + distanz, z, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator xNegativ(int bombPower, float waitTime, int x, int z, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x - distanz, z, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zPositiv(int bombPower, float waitTime, int x, int z, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x, z + distanz, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zNegativ(int bombPower, float waitTime, int x, int z, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x, z - distanz, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }


    bool ExplodeCell(int x, int z, int id)
    {
        GameObject thisGameObject = LevelGenerator.AllGameObjects[x, z];

        if (thisGameObject == null)
        {
            Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
            Instantiate(ExplosionPrefab2, new Vector3(x, 0.5f, z), Quaternion.identity);
            return true;
        }
        else
        {
            switch (thisGameObject.tag)
            {
                case "Bombe":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Instantiate(ExplosionPrefab2, new Vector3(x, 0.5f, z), Quaternion.identity);
                    BombScript thisBombeScript = thisGameObject.GetComponent<BombScript>();
                    //PlayerSpawner.playerList[id].gameObject.GetComponent<PlayerScript>().remoteBombList.Remove(thisGameObject);
                    thisBombeScript.bombTimer = 0;
                    thisBombeScript.remoteBomb = false;
                    return false;

                case "Wand":
                    return false;

                case "Kiste":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Instantiate(ExplosionPrefab2, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Destroy(thisGameObject);
                    return false;

                case "Item":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Instantiate(ExplosionPrefab2, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Destroy(thisGameObject);
                    return true;

                case "Player":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Instantiate(ExplosionPrefab2, new Vector3(x, 0.5f, z), Quaternion.identity);
                    thisGameObject.GetComponent<PlayerScript>().dead(thisGameObject.GetComponent<PlayerScript>().getPlayerID());
                    return true;

                default:
                    break;
            }
        }
        return false;
    }
}