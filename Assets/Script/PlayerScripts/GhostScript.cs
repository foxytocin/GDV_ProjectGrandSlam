using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    public Vector3 spawnPosition;
    public Vector3 destroyPosition;

    private void Start()
    {
        transform.Rotate(-90, 0, 0);
    }
    // Update is called once per frame
    void Update ()
    {
        if (destroyPosition.y == transform.position.y)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Rotate(0, 0, 2);
            transform.position = Vector3.MoveTowards(transform.position, destroyPosition, 1 * Time.deltaTime);
        }
    }
}
