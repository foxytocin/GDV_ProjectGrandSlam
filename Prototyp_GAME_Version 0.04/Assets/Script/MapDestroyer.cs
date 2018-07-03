using UnityEngine;

public class MapDestroyer : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public GameObject ExplosionPrefab;
    public int bombPower;

    //Wird nach Zeitablauf der Bombe durch die BombeScript aufgerufen und emfängt die Position der Bombe
    bool shouldExplode = true;

    public void Explode(int x, int z, int bombPower)
    {
        //Controlliert benachbarte Zellen auf Bomben, Kisten und Wände
        Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);

        for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
        {
            if (shouldExplode)
            {
                ExplodeCell(x + ausbreitung, z);
            }
        }
        shouldExplode = true;

        for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
        {
            if (shouldExplode)
            {
                ExplodeCell(x - ausbreitung, z);
            }
        }
        shouldExplode = true;

        for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
        {
            if (shouldExplode)
            {
                ExplodeCell(x, z - ausbreitung);
            }
        }
        shouldExplode = true;

        for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
        {
            if (shouldExplode)
            {
                ExplodeCell(x, z + ausbreitung);
            }
        }
        shouldExplode = true;
    }


    void ExplodeCell(int x, int z)
    {
        GameObject thisBomb = LevelGenerator.AllGameObjects[x, z];

        if (thisBomb == null)
        {
            Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
            shouldExplode = true;
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
                    shouldExplode = false;
                    break;

                case "Wand":
                    shouldExplode = false;
                    break;

                case "Kiste":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    Destroy(thisBomb);
                    shouldExplode = false;
                    break;

                case "Item":
                    Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    Destroy(thisBomb);
                    shouldExplode = true;
                    break;

                default:
                    break;
            }
        } 
    }
}