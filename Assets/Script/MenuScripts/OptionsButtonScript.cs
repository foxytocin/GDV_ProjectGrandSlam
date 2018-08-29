using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonScript : MonoBehaviour
{

    public Slider fxSlider;
    public Slider musicSlider;

    public GameObject offSide;
    public GameObject onSide;

    OverlayMethodenScript overlayMethodenScript;

    private void Start()
    {
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
    }

    public void onClickOptionsButton()
    {
        overlayMethodenScript.sideSwitch(offSide, onSide);
        overlayMethodenScript.updateSoundOptions(fxSlider, musicSlider);
    }

}
