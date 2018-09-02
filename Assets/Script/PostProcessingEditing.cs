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

    private RulesScript rules;

    // Use this for initialization
    void Start () {
        cameraDirection = FindObjectOfType<CameraDirection>();
        rules = FindObjectOfType<RulesScript>();

        //Change PostProcessing Settings
        pp = GetComponent<PostProcessingBehaviour>().profile;
        pp.bloom.enabled = true;
  
        depthSettings = pp.depthOfField.settings;
        depthSettings.focalLength = 170;
        depthSettings.aperture = 6f;

        //Initialize Fog
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear; //ExponentialSquared;
        //RenderSettings.fogDensity = 0.035f;
        RenderSettings.fogStartDistance = 35f;
        RenderSettings.fogEndDistance = 55f;

        //Initialize ChromaticAberration
        chromaticAberrationStrength = 0f;
        pp.chromaticAberration.enabled = true;
        chromAbSettings = pp.chromaticAberration.settings;
        chromAbSettings.intensity = chromaticAberrationStrength;
        chromAbSettings.spectralTexture = null;
    }

    // Update is called once per frame
    void Update () {
        setFocusPoint();
        setChromaticAberrationStrength();
    }

    void setFocusPoint()
    {
        float distance;
        if (rules.resultScreen.activeSelf)
        {
            distance = Vector3.Distance(this.transform.position, cameraDirection.target) - 0.6f;
        }
        else
        {
            distance = Vector3.Distance(this.transform.position, cameraDirection.target);
        }
        depthSettings.focusDistance = distance;
        pp.depthOfField.settings = depthSettings;
    }

    void setChromaticAberrationStrength()
    {
        chromAbSettings.intensity = chromaticAberrationStrength;
        pp.chromaticAberration.settings = chromAbSettings;
    }
}
