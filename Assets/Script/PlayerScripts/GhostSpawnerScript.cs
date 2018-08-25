using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnerScript : MonoBehaviour {

    public GameObject Ghost_Prefab;
    GameObject ghost;
    Vector3 destroyPosition;

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

    public void createGhost(Vector3 spawnposition, int id)
    {

        ghost = Instantiate(Ghost_Prefab, spawnposition, Quaternion.identity, transform);

        switch (id)
        {
            case 0:
                audioSource.PlayOneShot(audioScream0, 0.9f);
                ghost.GetComponent<Renderer>().material.color = new Color32(236, 77, 19, 200);
                ghost.GetComponent<Light>().color = new Color32(236, 77, 19, 1);
                break;

            case 1:
                audioSource.PlayOneShot(audioScream1, 0.8f);
                ghost.GetComponent<Renderer>().material.color = new Color32(82, 203, 16, 200);
                ghost.GetComponent<Light>().color = new Color32(82, 203, 16, 1);
                break;

            case 2:
                audioSource.PlayOneShot(audioScream2, 0.5f);
                ghost.GetComponent<Renderer>().material.color = new Color32(17, 170, 212, 200);
                ghost.GetComponent<Light>().color = new Color32(17, 170, 212, 1);
                break;

            case 3:
                audioSource.PlayOneShot(audioScream3, 0.5f);
                ghost.GetComponent<Renderer>().material.color = new Color32(226, 195, 18, 200);
                ghost.GetComponent<Light>().color = new Color32(226, 195, 18, 1);
                break;

            default: break;
        }

        

        destroyPosition = spawnposition;
        destroyPosition.y += 3;

        GhostScript tmp = ghost.GetComponent<GhostScript>();
        tmp.destroyPosition = this.destroyPosition;
        tmp.spawnPosition = spawnposition;

    }
    
        

}
