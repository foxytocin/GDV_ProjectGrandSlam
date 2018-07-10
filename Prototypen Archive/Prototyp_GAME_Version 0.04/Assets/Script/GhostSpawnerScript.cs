using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnerScript : MonoBehaviour {

    public GameObject Ghost_Prefab;
    GameObject ghost;
    Vector3 destroyPosition;

    public void createGhost(Vector3 spawnposition)
    {
        ghost = Instantiate(Ghost_Prefab, spawnposition, Quaternion.identity);

        destroyPosition = spawnposition;
        destroyPosition.y += 3;

        GhostScript tmp = ghost.GetComponent<GhostScript>();
        tmp.destroyPosition = this.destroyPosition;
        tmp.spawnPosition = spawnposition;

    }
    
        

}
