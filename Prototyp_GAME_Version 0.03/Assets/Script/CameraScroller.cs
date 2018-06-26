using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour {

    public WorldScript WorldScript;
    public int rowPosition;
    public int altePosition;
    private bool create; 

	// Use this for initialization
	void Start () {
        altePosition = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 0.5f * Time.deltaTime);

	}

    private void LateUpdate()
    {
        rowPosition = (int)Mathf.Round(transform.position.z) + 32;

        if (rowPosition > altePosition)
        {
            altePosition = rowPosition;
            WorldScript.createWorld(rowPosition);
            Debug.Log("GERADE REIHE NR " + rowPosition);
        }
    }
}