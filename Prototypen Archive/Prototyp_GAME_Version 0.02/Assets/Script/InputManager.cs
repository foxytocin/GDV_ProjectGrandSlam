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
        return new Vector3(Mathf.Round(OneMainHorizontal()), 0, Mathf.Round(OneMainVertikal()));
    }
    public static bool OneXButton()
    {   
        if(Input.GetButtonDown("One_X_Button") || Input.GetButtonDown("Mac_One_X_Button"))
            return true;

        return false;
    }
    public static bool OneAButton()
    {
        if (Input.GetButtonDown("One_A_Button"))
            return true;

        return false;
    }
    public static bool OneStartButton()
    {
        if (Input.GetButtonDown("One_Start_Button"))
            return true;

        return false;
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
        if (Input.GetButtonDown("Two_X_Button") || Input.GetButtonDown("Mac_Two_X_Button"))
            return true;

        return false;
    }
    public static bool TwoAButton()
    {
        if (Input.GetButtonDown("Two_A_Button"))
            return true;

        return false;
    }
    public static bool TwoStartButton()
    {
        if (Input.GetButtonDown("Two_Start_Button"))
            return true;

        return false;
    }



    //Player_Three
    public static float ThreeMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Three_MainHorizontal");
        r += Input.GetAxis("K_Three_MainHorizontal");
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
        if (Input.GetButtonDown("Three_X_Button") || Input.GetButtonDown("Mac_Three_X_Button"))
            return true;

        return false;
    }
    public static bool ThreeAButton()
    {
        if (Input.GetButtonDown("Three_A_Button"))
            return true;

        return false;
    }
    public static bool ThreeStartButton()
    {
        if (Input.GetButtonDown("Three_Start_Button"))
            return true;

        return false;
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
        if (Input.GetButtonDown("Four_X_Button") || Input.GetButtonDown("Mac_Four_X_Button"))
            return true;

        return false;
    }
    public static bool FourAButton()
    {
        if (Input.GetButtonDown("Four_A_Button"))
            return true;

        return false;
    }
    public static bool FourStartButton()
    {
        if (Input.GetButtonDown("Four_Start_Button"))
            return true;

        return false;
    }

}
