using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnerScript : MonoBehaviour {

    public GameObject Ghost_Prefab;
    GameObject ghost;
    byte transparent = 200;

    public AudioSource audioSource;
    public AudioClip audioScream0;
    public AudioClip audioScream1;
    public AudioClip audioScream2;
    public AudioClip audioScream3;
    private AudioManager audioManager;

    private int randomScream;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        ghostColor.a = transparent;
        ghost.GetComponent<GhostScript>().startsAnimations(ghostColor);

        ghostColor.a = 1;
        ghost.GetComponent<Light>().color = ghostColor;

    }
    
        

}
