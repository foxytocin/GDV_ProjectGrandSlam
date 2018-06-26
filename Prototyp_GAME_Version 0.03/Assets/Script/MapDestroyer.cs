﻿using System.Collections;
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
        GameObject objectInCell = World.WorldArray[x, z];
        
        if (objectInCell == null)
        {
            //Die Celle ist ein Gang
            Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
            shouldExplode = true;

        } else {

            //Die Celle ist eine Bombe, Wand, Kiste oder Icon
            if (objectInCell.name.Contains("Bombe_"))
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
          
                //GameObject thisBombe = World.WorldArray[x, z];
                BombeScript thisBombeScript = objectInCell.GetComponent<BombeScript>();
                thisBombeScript.bombTimer = 0;
                thisBombeScript.remoteBomb = false;

                shouldExplode = false;
            }

            if (objectInCell.name == "Wand")
            {
                shouldExplode = false;
            }

            if (objectInCell.name == "Kiste")
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                Destroy(objectInCell);
                shouldExplode = false;
            }

            if (objectInCell.name.Contains("Item"))
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
                Destroy(objectInCell);
                shouldExplode = true;
            }

            if (objectInCell.name.Contains("Player_"))
            {
                Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);

                PlayerScript thisPlayerScript = objectInCell.GetComponent<PlayerScript>();
                thisPlayerScript.setLife(-1);
              
                //Destroy(ObjectinCell);
                shouldExplode = true;
            }
        }
    }
}