using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    public Vector3 spawnPosition;
    public Vector3 destroyPosition;

    Color32 color;
    Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
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
            if (color.a >= 5)
                color.a -= 5;
            else
                GetComponent<Light>().enabled = false;

            transform.Rotate(0, 0, 2);
            transform.position = Vector3.MoveTowards(transform.position, destroyPosition, 1 * Time.deltaTime);
            material.color = color;
        }
    }

    public void setColor (Color32 color)
    {
        this.color = color;
    }
}
