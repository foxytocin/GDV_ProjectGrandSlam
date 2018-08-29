using UnityEngine;

public class SideSwitchScript : MonoBehaviour
{

    public GameObject offSide;
    public GameObject onSide;

    OverlayMethodenScript overlayMethodenScript;

    private void Start()
    {
        overlayMethodenScript = FindObjectOfType<OverlayMethodenScript>();
    }

    public void onClickThisButton()
    {
        overlayMethodenScript.sideSwitch(offSide, onSide);
    }

}
