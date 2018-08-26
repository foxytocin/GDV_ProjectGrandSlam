using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnerScript : MonoBehaviour {

    public GameObject Ghost_Prefab;
    GameObject ghost;
    Vector3 destroyPosition;
    byte transparent = 200;

    public AudioSource audioSource;
    public AudioClip audioScream0;
    public AudioClip audioScream1;
    public AudioClip audioScream2;
    public AudioClip audioScream3;

    private int randomScream;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void createGhost(Vector3 spawnposition, int id, Color32 playerColor)
    {
        Debug.Log("vorm Laden");
        ghost = ObjectPooler.Instance.SpawnFromPool("Ghost", spawnposition, Quaternion.identity);
        Debug.Log("nachm Laden");
        switch (id)
        {
            case 0:
                audioSource.PlayOneShot(audioScream0, 0.9f);              
                break;

            case 1:
                audioSource.PlayOneShot(audioScream1, 0.8f);
                break;

            case 2:
                audioSource.PlayOneShot(audioScream2, 0.5f);
                break;

            case 3:
                audioSource.PlayOneShot(audioScream3, 0.5f);
                break;

            default: break;
        }

        Color32 ghostColor = playerColor;
        ghostColor.a = transparent;
        ghost.GetComponent<Renderer>().material.color = ghostColor;

        ghostColor.a = 1;
        ghost.GetComponent<Light>().color = ghostColor;

        destroyPosition = spawnposition;
        destroyPosition.y += 5;

        GhostScript tmp = ghost.GetComponent<GhostScript>();
        StartCoroutine(tmp.animationGhost(playerColor, spawnposition));
        
        //tmp.setColor(ghost.GetComponent<Renderer>().material.color);
    }
    
        

}
