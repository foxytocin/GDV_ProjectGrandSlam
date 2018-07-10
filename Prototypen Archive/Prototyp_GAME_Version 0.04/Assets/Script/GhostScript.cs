using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    public Vector3 spawnPosition;
    public Vector3 destroyPosition;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (destroyPosition == transform.position)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destroyPosition, 1 * Time.deltaTime);
        }
    }
}
