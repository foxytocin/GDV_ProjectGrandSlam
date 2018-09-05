using UnityEngine;

public class SideSwitchScript : MonoBehaviour
{

    public GameObject offSide;
    public GameObject onSide;
    private AudioManager audioManager;

    OverlayMethodenScript overlayMethodenScript;

    private void Start()
    {
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void onClickThisButton()
    {
        audioManager.playSound("buttonclick");
        overlayMethodenScript.sideSwitch(offSide, onSide);
    }

}
