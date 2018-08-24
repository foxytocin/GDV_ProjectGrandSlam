using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour {

    private CameraScroller cameraScroller;
    private DayNightSwitch dayNightSwitch;
    public GameObject lightning_prefab;

    private int tmp = 0;

	// Use this for initialization
	void Start () {

        cameraScroller = FindObjectOfType<CameraScroller>();
        dayNightSwitch = FindObjectOfType<DayNightSwitch>();
    }
	
	// Update is called once per frame
	void Update () {

        if(Random.value > 0.80f && !dayNightSwitch.isDay)
        {
            GameObject lightning = Instantiate(lightning_prefab, new Vector3(Random.Range(-10f, 40f), 0, cameraScroller.rowPosition + Random.Range(0f, 40f)), Quaternion.identity);
        }
    }
}
