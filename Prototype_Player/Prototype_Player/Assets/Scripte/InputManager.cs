using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    // Player_One

    public static float OneMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_One_MainHorizontal");
        r += Input.GetAxis("K_One_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float OneMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_One_MainVertikal");
        r += Input.GetAxis("K_One_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 OneMainJoystick()
    {
        return new Vector3(OneMainHorizontal(), 0, OneMainVertikal());
    }
    public static bool OneXButton()
    {
        return Input.GetButtonDown("One_X_Button");
    }

    //player_Two
    public static float TwoMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Two_MainHorizontal");
        r += Input.GetAxis("K_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float TwoMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Two_MainVertikal");
        r += Input.GetAxis("K_Two_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 TwoMainJoystick()
    {
        return new Vector3(TwoMainHorizontal(), 0, TwoMainVertikal());
    }
    public static bool TwoXButton()
    {
        return Input.GetButtonDown("Two_X_Button");
    }

    //Player_Three
    public static float ThreeMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Two_MainHorizontal");
        r += Input.GetAxis("K_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float ThreeMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Three_MainVertikal");
        r += Input.GetAxis("K_Three_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 ThreeMainJoystick()
    {
        return new Vector3(ThreeMainHorizontal(), 0, ThreeMainVertikal());
    }
    public static bool ThreeXButton()
    {
        return Input.GetButtonDown("Three_X_Button");
    }

    //Player_Four
    public static float FourMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Two_MainHorizontal");
        r += Input.GetAxis("K_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float FourMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Four_MainVertikal");
        r += Input.GetAxis("K_Four_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 FourMainJoystick()
    {
        return new Vector3(FourMainHorizontal(), 0, FourMainVertikal());
    }
    public static bool FourXButton()
    {
        return Input.GetButtonDown("Four_X_Button");
    }
}
