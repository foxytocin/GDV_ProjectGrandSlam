using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirection : MonoBehaviour {
    private Vector3 target;
    private CameraMovement cm;
    // Use this for initialization
    void Start()
    {
        //Offset at Beginning, currently random -> make dynamic
        transform.position = new Vector3(0f, 4f, -10f);        

        cm = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {        
        target = cm.centerPoint;
        Vector3 targetPostition = Vector3.Lerp(transform.position, new Vector3(this.transform.position.x, 0, target.z), 4f * Time.deltaTime);
        this.transform.LookAt(targetPostition);

        //Limit lookAt Rotation
        Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 40f, 80f), 0, 0);

    }
}
