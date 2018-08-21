using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public GameObject Item_Prefab;
    public GameObject ItemObjekt;


    public void SpawnItem(int x, int z)
    {
        //Position des Items = Position der zerstörten Kiste.
        ItemObjekt = LevelGenerator.AllGameObjects[x, z] = Instantiate(Item_Prefab, new Vector3(x, 0.7f, z), Quaternion.identity, transform);

    }






}



