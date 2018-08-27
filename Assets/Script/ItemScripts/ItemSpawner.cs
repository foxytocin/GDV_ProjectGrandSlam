using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public GameObject Item_Prefab;
    private GameObject ItemObjekt;
    public AudioClip ItemAppears;


    public void SpawnItem(int x, int z)
    {
        //Position des Items = Position der zerstörten Kiste.
        ItemObjekt = Instantiate(Item_Prefab, new Vector3(x, 0.7f, z), Quaternion.identity, transform);
        LevelGenerator.AllGameObjects[x, z] = ItemObjekt;
        FindObjectOfType<AudioManager>().playSound("ItemAppears");

    }

}



