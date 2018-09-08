using UnityEngine;

public static class InputManager
{
    // Player_One

    // X Werte werden abgefragt
    public static float KeyOneMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_One_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXOneMainHorizontal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_One_MainHorizontal");
        Debug.Log("horizontal: " + r);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4OneMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_One_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float KeyOneMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_One_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXOneMainVertikal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_One_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4OneMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_One_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 KeyOneMainJoystick()
    {
        return new Vector3(KeyOneMainHorizontal(), 0, KeyOneMainVertikal());
    }

    public static Vector3 XBOXOneMainJoystick()
    {
        return new Vector3(XBOXOneMainHorizontal(), 0, XBOXOneMainVertikal());
    }

    public static Vector3 PS4OneMainJoystick()
    {
        return new Vector3(PS4OneMainHorizontal(), 0, PS4OneMainVertikal());
    }

    // Zusatz Tasten 
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
    public static bool OneR1Button()
    {
        if (Input.GetButtonDown("One_R1_Button"))
            return true;

        return false;
    }
    public static bool OneL1Button()
    {
        if (Input.GetButtonDown("One_L1_Button"))
            return true;

        return false;
    }


    //player_Two

    // X Werte werden abgefragt
    public static float KeyTwoMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXTwoMainHorizontal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_Two_MainHorizontal");
        Debug.Log("horizontal2: " + r);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4TwoMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    // Z Werte werden abgefragt
    public static float KeyTwoMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Two_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXTwoMainVertikal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_Two_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4TwoMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_Two_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 KeyTwoMainJoystick()
    {
        return new Vector3(KeyTwoMainHorizontal(), 0, KeyTwoMainVertikal());
    }

    public static Vector3 XBOXTwoMainJoystick()
    {
        return new Vector3(XBOXTwoMainHorizontal(), 0, XBOXTwoMainVertikal());
    }

    public static Vector3 PS4TwoMainJoystick()
    {
        return new Vector3(PS4TwoMainHorizontal(), 0, PS4TwoMainVertikal());
    }

    // Zusatz Tasten
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
    public static bool TwoR1Button()
    {
        if (Input.GetButtonDown("Two_R1_Button"))
            return true;

        return false;
    }
    public static bool TwoL1Button()
    {
        if (Input.GetButtonDown("Two_L1_Button"))
            return true;

        return false;
    }



    //Player_Three

    // X Werte werden abgefragt
    public static float KeyThreeMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Three_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXThreeMainHorizontal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_Three_MainHorizontal");
        Debug.Log("horizontal2: " + r);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4ThreeMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_Three_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    // Z Werte werden abgefragt
    public static float KeyThreeMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Three_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXThreeMainVertikal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_Three_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4ThreeMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_Three_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 KeyThreeMainJoystick()
    {
        return new Vector3(KeyThreeMainHorizontal(), 0, KeyThreeMainVertikal());
    }

    public static Vector3 XBOXThreeMainJoystick()
    {
        return new Vector3(XBOXThreeMainHorizontal(), 0, XBOXThreeMainVertikal());
    }

    public static Vector3 PS4ThreeMainJoystick()
    {
        return new Vector3(PS4ThreeMainHorizontal(), 0, PS4ThreeMainVertikal());
    }

    // Zusatz Tasten
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
    public static bool ThreeR1Button()
    {
        if (Input.GetButtonDown("Three_R1_Button"))
            return true;

        return false;
    }
    public static bool ThreeL1Button()
    {
        if (Input.GetButtonDown("Three_L1_Button"))
            return true;

        return false;
    }


    //Player_Four

    // X Werte werden abgefragt
    public static float KeyFourMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Four_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXFourMainHorizontal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_Four_MainHorizontal");
        Debug.Log("horizontal2: " + r);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4FourMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_Four_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    // Z Werte werden abgefragt
    public static float KeyFourMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("K_Four_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float XBOXFourMainVertikal()
    {
        float r = 0.0f;
        r = Input.GetAxis("XBOX_J_Four_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float PS4FourMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("PS4_J_Four_MainVertikal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Zusammenfügen der X und Z Werten in einen Vector3
    public static Vector3 KeyFourMainJoystick()
    {
        return new Vector3(KeyFourMainHorizontal(), 0, KeyFourMainVertikal());
    }

    public static Vector3 XBOXFourMainJoystick()
    {
        return new Vector3(XBOXFourMainHorizontal(), 0, XBOXFourMainVertikal());
    }

    public static Vector3 PS4FourMainJoystick()
    {
        return new Vector3(PS4FourMainHorizontal(), 0, PS4FourMainVertikal());
    }

    // Zusatz Tasten
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
    public static bool FourR1Button()
    {
        if (Input.GetButtonDown("Four_R1_Button"))
            return true;

        return false;
    }
    public static bool FourL1Button()
    {
        if (Input.GetButtonDown("Four_L1_Button"))
            return true;

        return false;
    }

}