using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingEditing : MonoBehaviour {

    private PostProcessingProfile pp;
    private DepthOfFieldModel.Settings depthSettings;
    private ChromaticAberrationModel.Settings chromAbSettings;
    public float chromaticAberrationStrength;
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
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.035f;
        //RenderSettings.fogStartDistance = 13f;
        //RenderSettings.fogEndDistance = 51f;

        //Initialize ChromaticAberration
        chromaticAberrationStrength = 0f;
        pp.chromaticAberration.enabled = true;
        chromAbSettings = pp.chromaticAberration.settings;
        chromAbSettings.intensity = chromaticAberrationStrength;
        chromAbSettings.spectralTexture = null;
    }

    // Update is called once per frame
    void LateUpdate () {
        setFocusPoint();
        setChromaticAberrationStrength();
    }

    void setFocusPoint()
    {
        float distance = Vector3.Distance(this.transform.position, cameraDirection.target);
        depthSettings.focusDistance = distance;
        pp.depthOfField.settings = depthSettings;
    }

    void setChromaticAberrationStrength()
    {
        chromAbSettings.intensity = chromaticAberrationStrength;
        pp.chromaticAberration.settings = chromAbSettings;
    }
}
