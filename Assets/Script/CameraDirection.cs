using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraDirection : MonoBehaviour {
    private Vector3 target;
    private CameraMovement cm;

    private PostProcessingProfile pp;

    //private GameObject target;
    public float smooth = 0.3F;
    public float distance = 5.0F;
    private float yVelocity = 0.0F;

    private float degreesPerSecond = 300f;

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
        depthSettings.focalLength = 120;
        depthSettings.aperture = 8f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Working but trying to smooth it        
        target = cm.centerPoint;
        //Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(this.transform.position.x, 0, target.z), 4f * Time.deltaTime);

        //Debug.Log(target);


        Vector3 dirFromMeToTarget = target - transform.position;
        //dirFromMeToTarget.x = 0f;
        //dirFromMeToTarget.y = 0f;
        //Debug.Log(dirFromMeToTarget);
        Quaternion lookRot = Quaternion.LookRotation(dirFromMeToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * (degreesPerSecond / 360f));

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position - targetPosition), Time.deltaTime * 2f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition), 2f * Time.deltaTime);
        //this.transform.LookAt(targetPosition);

        // Limit lookAt Rotation
        Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 40f, 80f), Camera.main.transform.localEulerAngles.y, 0);

        // Smooth Try
        /*
        //target = cm.centerPoint;
        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.transform.position.y, ref yVelocity, smooth);
        Vector3 position = target.transform.position;
        //position += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);
        transform.position = position;
        transform.LookAt(target.transform);
        */
        setFocusPoint();
    }

    void setFocusPoint()
    {
        float distance = Vector3.Distance(this.transform.position, target);        
        depthSettings.focusDistance = distance;
        pp.depthOfField.settings = depthSettings;
    }
}