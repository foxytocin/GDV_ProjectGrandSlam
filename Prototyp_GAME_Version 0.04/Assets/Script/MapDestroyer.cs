using System.Collections;
using UnityEngine;

public class MapDestroyer : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public GameObject ExplosionPrefab;
    private IEnumerator coroutinexPositiv;
    private IEnumerator coroutinexNegativ;
    private IEnumerator coroutinezPositiv;
    private IEnumerator coroutinezNegativ;

    //Wird nach Zeitablauf der Bombe durch die BombeScript aufgerufen und emfängt die Position der Bombe

    public void Explode(int x, int z, int bombPower)
    {
        coroutinexPositiv = xPositiv(bombPower, 0.1f, x, z);
        coroutinexNegativ = xNegativ(bombPower, 0.1f, x, z);
        coroutinezPositiv = zPositiv(bombPower, 0.1f, x, z);
        coroutinezNegativ = zNegativ(bombPower, 0.1f, x, z);

        StartCoroutine(coroutinexPositiv);
        StartCoroutine(coroutinexNegativ);
        StartCoroutine(coroutinezPositiv);
        StartCoroutine(coroutinezNegativ);
    }


    private IEnumerator xPositiv(int bombPower, float waitTime, int x, int z)
    {
        Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x + distanz, z))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator xNegativ(int bombPower, float waitTime, int x, int z)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x - distanz, z))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zPositiv(int bombPower, float waitTime, int x, int z)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x, z + distanz))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zNegativ(int bombPower, float waitTime, int x, int z)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell(x, z - distanz))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }


    bool ExplodeCell(int x, int z)
    {
        GameObject thisBomb = LevelGenerator.AllGameObjects[x, z];

        if (thisBomb == null)
        {
            Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
            return true;
        }
        else
        {
            switch (thisBomb.tag)
            {
                case "Bombe":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    BombScript thisBombeScript = thisBomb.GetComponent<BombScript>();
                    thisBombeScript.bombTimer = 0;
                    thisBombeScript.remoteBomb = false;
                    return false;

                case "Wand":
                    return false;

                case "Kiste":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    Destroy(thisBomb);
                    return false;

                case "Item":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    Destroy(thisBomb);
                    return true;

                default:
                    break;
            }
        }
        return false;
    }
}