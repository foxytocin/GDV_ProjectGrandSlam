using System.Collections;
using UnityEngine;

public class MapDestroyer : MonoBehaviour
{
    //public PlayerSpawner PlayerSpawner;
    public LevelGenerator levelGenerator;
    public GameObject ExplosionPrefab;
    private IEnumerator coroutinexPositiv;
    private IEnumerator coroutinexNegativ;
    private IEnumerator coroutinezPositiv;
    private IEnumerator coroutinezNegativ;

    //Wird nach Zeitablauf der Bombe durch die BombeScript aufgerufen und emfängt die Position der Bombe
    public void Explode(xzPosition bombPosition, int bombPower, int id)
    {
        Instantiate(ExplosionPrefab, new Vector3(bombPosition.x, 0.5f, bombPosition.z), Quaternion.identity);

        coroutinexPositiv = xPositiv(bombPower, 0.1f, bombPosition, id);
        coroutinexNegativ = xNegativ(bombPower, 0.1f, bombPosition, id);
        coroutinezPositiv = zPositiv(bombPower, 0.1f, bombPosition, id);
        coroutinezNegativ = zNegativ(bombPower, 0.1f, bombPosition, id);

        StartCoroutine(coroutinexPositiv);
        StartCoroutine(coroutinexNegativ);
        StartCoroutine(coroutinezPositiv);
        StartCoroutine(coroutinezNegativ);
    }


    private IEnumerator xPositiv(int bombPower, float waitTime, xzPosition bombPosition, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(bombPosition.x + distanz, bombPosition.z, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator xNegativ(int bombPower, float waitTime, xzPosition bombPosition, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(bombPosition.x - distanz, bombPosition.z, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zPositiv(int bombPower, float waitTime, xzPosition bombPosition, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(bombPosition.x, bombPosition.z + distanz, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zNegativ(int bombPower, float waitTime, xzPosition bombPosition, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(bombPosition.x, bombPosition.z - distanz, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }


    bool ExplodeCell(int x, int z, int id)
    {
        GameObject thisGameObject = levelGenerator.AllGameObjects[x, z];

        if (thisGameObject == null)
        {
            Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
            return true;
        }
        else
        {
            switch (thisGameObject.tag)
            {
                case "Bombe":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    thisGameObject.GetComponent<BombScript>().bombTimer = 0;
                    thisGameObject.GetComponent<BombScript>().remoteBomb = false;
                    return false;

                case "Wand":
                    return false;

                case "Kiste":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Destroy(thisGameObject);
                    return false;

                case "Item":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    Destroy(thisGameObject);
                    return true;

                case "Player":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    thisGameObject.GetComponent<PlayerScript>().dead(thisGameObject.GetComponent<PlayerScript>().getPlayerID());
                    levelGenerator.AllGameObjects[x, z] = null;
                    return true;

                default:
                    break;
            }
        }
        return false;
    }
}