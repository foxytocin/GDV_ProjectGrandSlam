using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnerScript : MonoBehaviour {

    GameObject ghost;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void createGhost(Vector3 spawnposition, int id, Color32 playerColor)
    {

        ghost = ObjectPooler.Instance.SpawnFromPool("Ghost", spawnposition, Quaternion.identity);

        switch (id)
        {
            case 0:
                audioManager.playSound("player_dead_0");
                break;

            case 1:
                audioManager.playSound("player_dead_1");
                break;

            case 2:
                audioManager.playSound("player_dead_2");
                break;

            case 3:
                audioManager.playSound("player_dead_3");
                break;

            default: break;
        }

        Color32 ghostColor = playerColor;
        ghost.GetComponent<GhostScript>().startsAnimations(ghostColor);
    }
    
}
