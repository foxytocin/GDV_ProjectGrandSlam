using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirection : MonoBehaviour {
    private Vector3 target;
    // Use this for initialization
    void Start()
    {
        //Offset at Beginning, currently random -> make dynamic
        transform.position = new Vector3(0f, 0f, -10f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>().CalcCenterPoint(2);
        Vector3 targetPostition = new Vector3(this.transform.position.x, target.y, target.z);
        this.transform.LookAt(targetPostition);

        //Limit lookAt Rotation
        Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 40f, 80f), 0, 0);

    }
}
