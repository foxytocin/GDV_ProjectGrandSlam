using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public GameObject Item_Prefab;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void SpawnItem(int x, int z)
    {
        //Position des Items = Position der zerstörten Kiste.
        LevelGenerator.AllGameObjects[x, z] = Instantiate(Item_Prefab, new Vector3(x, 0.7f, z), Quaternion.identity, transform);
        audioManager.playSound("ItemAppears");

    }

}



