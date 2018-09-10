using UnityEngine;

public static class InputManager
{
    // Player_One

    // X Werte werden abgefragt
    public static float OneMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_One_MainHorizontal");
        r += Input.GetAxis("XBOX_J_One_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float OneMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_One_MainVertikal");
        r += Input.GetAxis("XBOX_J_One_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 OneMainJoystick()
    {
        return new Vector3(OneMainHorizontal(), 0, OneMainVertikal());
    }

    // Zusatz Tasten 
    public static bool OneXButton()
    {   
        if(Input.GetButtonDown("XBOX_One_X_Button") || Input.GetButtonDown("K_One_X_Button"))
            return true;

        return false;
    }

    public static bool OneAButton()
    {
        if (Input.GetButtonDown("XBOX_One_A_Button") || Input.GetButtonDown("K_One_A_Button"))
            return true;

        return false;
    }

    public static bool XBOXOneStartButton()
    {
        if (Input.GetButtonDown("XBOX_One_Start_Button"))
            return true;

        return false;
    }


    //player_Two

    // X Werte werden abgefragt
    public static float TwoMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Two_MainHorizontal");
        r += Input.GetAxis("XBOX_J_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float TwoMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Two_MainVertikal");
        r += Input.GetAxis("XBOX_J_Two_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 TwoMainJoystick()
    {
        return new Vector3(TwoMainHorizontal(), 0, TwoMainVertikal());
    }

    // Zusatz Tasten
    public static bool TwoXButton()
    {
        if (Input.GetButtonDown("XBOX_Two_X_Button") || Input.GetButtonDown("K_Two_X_Button"))
            return true;

        return false;
    }

    public static bool TwoAButton()
    {
        if (Input.GetButtonDown("XBOX_Two_A_Button") || Input.GetButtonDown("K_Two_A_Button"))
            return true;

        return false;
    }

    public static bool XBOXTwoStartButton()
    {
        if (Input.GetButtonDown("XBOX_Two_Start_Button"))
            return true;

        return false;
    }


    //Player_Three

    // X Werte werden abgefragt
    public static float ThreeMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Three_MainHorizontal");
        r += Input.GetAxis("XBOX_J_Three_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float ThreeMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Three_MainVertikal");
        r += Input.GetAxis("XBOX_J_Three_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 ThreeMainJoystick()
    {
        return new Vector3(ThreeMainHorizontal(), 0, ThreeMainVertikal());
    }

    // Zusatz Tasten
    public static bool ThreeXButton()
    {
        if (Input.GetButtonDown("XBOX_Three_X_Button") || Input.GetButtonDown("K_Three_X_Button"))
            return true;

        return false;
    }

    public static bool ThreeAButton()
    {
        if (Input.GetButtonDown("XBOX_Three_A_Button") || Input.GetButtonDown("K_Three_A_Button"))
            return true;

        return false;
    }

    public static bool XBOXThreeStartButton()
    {
        if (Input.GetButtonDown("XBOX_Three_Start_Button"))
            return true;

        return false;
    }


    //Player_Four

    // X Werte werden abgefragt
    public static float FourMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Four_MainHorizontal");
        r += Input.GetAxis("XBOX_J_Four_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float FourMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Four_MainVertikal");
        r += Input.GetAxis("XBOX_J_Four_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 FourMainJoystick()
    {
        return new Vector3(FourMainHorizontal(), 0, FourMainVertikal());
    }

    // Zusatz Tasten
    public static bool FourXButton()
    {
        if (Input.GetButtonDown("XBOX_Four_X_Button") || Input.GetButtonDown("K_Four_X_Button"))
            return true;

        return false;
    }

    public static bool FourAButton()
    {
        if (Input.GetButtonDown("XBOX_Four_A_Button") || Input.GetButtonDown("K_Four_A_Button"))
            return true;

        return false;
    }

    public static bool XBOXFourStartButton()
    {
        if (Input.GetButtonDown("XBOX_Four_Start_Button"))
            return true;

        return false;
    }

}