using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapDestroyer : MonoBehaviour
{
    public WorldScript World;
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
        GameObject ObjectinCell = World.WorldArray[x, z];
        
        if (ObjectinCell == null)
        {
            //Die Celle ist ein Gang
            Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
            shouldExplode = true;

        } else {

            //Die Celle ist eine Bombe, Wand, Kiste oder Icon
            if (ObjectinCell.name.Contains("Bombe_"))
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
          
                //GameObject thisBombe = World.WorldArray[x, z];
                BombeScript thisBombeScript = ObjectinCell.GetComponent<BombeScript>();
                thisBombeScript.bombTimer = 0;
                thisBombeScript.remoteBomb = false;

                shouldExplode = false;
            }

            if (ObjectinCell.name == "Wand")
            {
                shouldExplode = false;
            }

            if (ObjectinCell.name == "Kiste")
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                Destroy(ObjectinCell);
                shouldExplode = false;
            }

            if (ObjectinCell.name.Contains("Item"))
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                Destroy(ObjectinCell);
                shouldExplode = true;
            }

            if (ObjectinCell.name.Contains("Player_"))
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);

                PlayerScript thisPlayerScript = ObjectinCell.GetComponent<PlayerScript>();
                thisPlayerScript.setLife(-1);
              
                //Destroy(ObjectinCell);
                shouldExplode = true;
            }
        }
    }
}