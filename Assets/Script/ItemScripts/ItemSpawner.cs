using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public GameObject Item_Prefab;
    private Vector3 itemPosition;

    public void SpawnItem(int x, int z)
    {
        //Position des Items = Position der zerstörten Kiste.
        LevelGenerator.AllGameObjects[x, z] = Instantiate(Item_Prefab, new Vector3(x, 0.7f, z), Quaternion.identity, transform);
    }

    //Schreibt Item Fähigkeit gewünschtem Player zu
    public void PlayerItem(int id)
    {
        Debug.Log("Player" + id + " ist besser");
    }
}

