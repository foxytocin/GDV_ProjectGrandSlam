using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour {

    private CameraScroller cameraScroller;
    public GameObject lightning_prefab;

    private int tmp = 0;

	// Use this for initialization
	void Start () {

        cameraScroller = FindObjectOfType<CameraScroller>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if(cameraScroller.rowPosition > tmp*2)
        {
            tmp = cameraScroller.rowPosition;
            GameObject lightning = Instantiate(lightning_prefab, new Vector3(0, 0, cameraScroller.rowPosition), Quaternion.identity);
        }
    }
}
