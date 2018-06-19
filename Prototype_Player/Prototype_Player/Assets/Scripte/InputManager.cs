using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainHorizontal");
        r += Input.GetAxis("K_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainVertikal");
        r += Input.GetAxis("K_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertikal());
    }
    public static bool XButton()
    {
        return Input.GetButton("X_Button");
    }

}
