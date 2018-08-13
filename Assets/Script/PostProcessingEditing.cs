using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingEditing : MonoBehaviour {

    private PostProcessingProfile pp;
    private DepthOfFieldModel.Settings depthSettings;

    private CameraDirection cameraDirection;

    // Use this for initialization
    void Start () {
        cameraDirection = GameObject.Find("Main Camera").GetComponent<CameraDirection>();

        //Change PostProcessing Settings
        pp = GetComponent<PostProcessingBehaviour>().profile;
        pp.bloom.enabled = true;

        depthSettings = pp.depthOfField.settings;
        depthSettings.focalLength = 110;
        depthSettings.aperture = 7f;

        //Initialize Fog
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 23f;
        RenderSettings.fogEndDistance = 31f;
    }

    // Update is called once per frame
    void LateUpdate () {
        setFocusPoint();
    }

    void setFocusPoint()
    {
        float distance = Vector3.Distance(this.transform.position, cameraDirection.target);
        depthSettings.focusDistance = distance;
        pp.depthOfField.settings = depthSettings;
    }
}
