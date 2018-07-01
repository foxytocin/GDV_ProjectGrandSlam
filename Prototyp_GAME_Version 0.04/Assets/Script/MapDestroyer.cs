using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapDestroyer : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public GameObject ExplosionPrefab;
    public int bombPower;

    //Wird nach Zeitablauf der Bombe durch die BombeScript aufgerufen und emfängt die Position der Bombe
    bool shouldExplode = true;

    public void Explode(int x, int z, int bombPower)
    {
        Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
        List<GameObject> KisteToDestory = new List<GameObject>();

        foreach (GameObject go in LevelGenerator.AllGameObjects)
        {
            int xPos = (int)(go.transform.position.x);
            int zPos = (int)(go.transform.position.z);

            if (xPos == x || zPos == z) {
                KisteToDestory.Add(go);
                Debug.Log("Kiste die zerstört werden muss: " +xPos+ " / " +zPos);
            }

            for (int i = 1; i < bombPower + 1; i++) {

                if (x + i == xPos && z == zPos) {
                    Debug.Log("Kiste in +x zerstört: " +xPos+ " / " +zPos);
                    Instantiate(ExplosionPrefab, new Vector3(x + i, 0.5f, z), Quaternion.identity);
                    LevelGenerator.AllGameObjects.Remove(go);
                    Destroy(go);
                }

                if (x - i == xPos && z == zPos)
                {
                    Debug.Log("Kiste in -x zerstört: " + xPos + " / " + zPos);
                    Instantiate(ExplosionPrefab, new Vector3(x - i, 0.5f, z), Quaternion.identity);
                    LevelGenerator.AllGameObjects.Remove(go);
                    Destroy(go);
                }

                if (x == xPos && z + i == zPos)
                {
                    Debug.Log("Kiste in +z zerstört: " + xPos + " / " + zPos);
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z + i), Quaternion.identity);
                    LevelGenerator.AllGameObjects.Remove(go);
                    Destroy(go);
                }

                if (x == xPos && z - i == zPos)
                {
                    Debug.Log("Kiste in -z zerstört: " + xPos + " / " + zPos);
                    Instantiate(ExplosionPrefab, new Vector3(x, 0.5f, z - i), Quaternion.identity);
                    LevelGenerator.AllGameObjects.Remove(go);
                    Destroy(go);
                }

            }

           


            //if ((int)dist <= bombPower)
            //{
            //    Debug.Log("DESTROY " + go.gameObject);
            //    Instantiate(ExplosionPrefab, go.transform.position, Quaternion.identity);
            //    LevelGenerator.AllGameObjects.Remove(go);
            //    Destroy(go);
            //}
        }
    }
}


    //    for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
    //    {
    //        if (shouldExplode)
    //        {
    //            ExplodeCell(x + ausbreitung, z);
    //        }
    //    }
    //    shouldExplode = true;

    //    for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
    //    {
    //        if (shouldExplode)
    //        {
    //            ExplodeCell(x - ausbreitung, z);
    //        }
    //    }
    //    shouldExplode = true;

    //    for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
    //    {
    //        if (shouldExplode)
    //        {
    //            ExplodeCell(x, z - ausbreitung);
    //        }
    //    }
    //    shouldExplode = true;

    //    for (int ausbreitung = 1; ausbreitung < bombPower + 1; ausbreitung++)
    //    {
    //        if (shouldExplode)
    //        {
    //            ExplodeCell(x, z + ausbreitung);
    //        }
    //    }
    //    shouldExplode = true;
    //}


    //void ExplodeCell(int x, int z, GameObject[] allObjects)
    //{

        //foreach(object go in allObjects)
        //{
        //    float dist = Vector3.Distance(new Vector3(x, 0, z), new Vector3(BombxPos, 0, BombzPos));

        //    Debug.Log("ENTFERNUNG " +dist);

        //    if(dist < bombPower) {
        //        Destroy(this);
        //    }
        //} 


        //if (ObjectinCell == null)
        //{
        //    //Die Celle ist ein Gang
        //    Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
        //    shouldExplode = true;

        //} else {

        //    //Die Celle ist eine Bombe, Wand, Kiste oder Icon
        //    if (ObjectinCell.name.Contains("Bombe_"))
        //    {
        //        Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
          
        //        //GameObject thisBombe = World.WorldArray[x, z];
        //        BombeScript thisBombeScript = ObjectinCell.GetComponent<BombeScript>();
        //        thisBombeScript.bombTimer = 0;
        //        thisBombeScript.remoteBomb = false;

        //        shouldExplode = false;
        //    }

        //    if (ObjectinCell.name == "Wand")
        //    {
        //        shouldExplode = false;
        //    }

        //    if (ObjectinCell.name == "Kiste")
        //    {
        //        Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
        //        Destroy(ObjectinCell);
        //        shouldExplode = false;
        //    }

        //    if (ObjectinCell.name.Contains("Item"))
        //    {
        //        Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);
        //        Destroy(ObjectinCell);
        //        shouldExplode = true;
        //    }

        //    if (ObjectinCell.name.Contains("Player_"))
        //    {
        //        Instantiate(ExplosionPrefab, new Vector3(x, 0, z), Quaternion.identity);

        //        PlayerScript thisPlayerScript = ObjectinCell.GetComponent<PlayerScript>();
        //        thisPlayerScript.setLife(-1);
              
        //        //Destroy(ObjectinCell);
        //        shouldExplode = true;
        //    }
        //}
//    }
//}