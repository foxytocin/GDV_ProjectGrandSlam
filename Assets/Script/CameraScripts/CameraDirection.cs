using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraDirection : MonoBehaviour {
    private Vector3 target;
    private CameraMovement cm;   

    private float degreesPerSecond = 300f;

    private PostProcessingProfile pp;
    DepthOfFieldModel.Settings depthSettings;

    void Start()
    {
        //Offset at Beginning, currently random -> make dynamic
        transform.position = new Vector3(0f, 4f, -10f);        

        cm = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>();
        
        //Change PostProcessing Settings
        pp = GetComponent<PostProcessingBehaviour>().profile;
        pp.bloom.enabled = true;
        
        depthSettings = pp.depthOfField.settings;
        depthSettings.focalLength = 110;
        depthSettings.aperture = 7f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Working but trying to smooth it        
        target = cm.centerPoint;
        //Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(this.transform.position.x, 0, target.z), 4f * Time.deltaTime);

        Vector3 dirFromMeToTarget = target - transform.position;
        //dirFromMeToTarget.x = 0f;
        //dirFromMeToTarget.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dirFromMeToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * (degreesPerSecond / 360f));

        // Limit lookAt Rotation
        if(Camera.main.transform.localEulerAngles.y < 10)
        {
            Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 40f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 0f, 9f), 0);
        } else if (Camera.main.transform.localEulerAngles.y > 11)
        {
            Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 40f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 350f, 361f), 0);
        }
        //Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 40f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, maxRotation.x, maxRotation.y), 0);
        //Camera.main.transform.rotation.q

        setFocusPoint();
    }

    void setFocusPoint()
    {
        float distance = Vector3.Distance(this.transform.position, target);        
        depthSettings.focusDistance = distance;
        pp.depthOfField.settings = depthSettings;
    }
}