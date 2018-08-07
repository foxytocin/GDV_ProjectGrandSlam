using UnityEngine;

public static class InputManager
{
    // Player_One

    // X Werte werden abgefragt
    public static float OneMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_One_MainHorizontal");
        r += Input.GetAxis("K_One_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float OneMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_One_MainVertikal");
        r += Input.GetAxis("K_One_MainVertikal");
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
    public static float TwoMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Two_MainHorizontal");
        r += Input.GetAxis("K_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float TwoMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Two_MainVertikal");
        r += Input.GetAxis("K_Two_MainVertikal");
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
    public static float ThreeMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Three_MainHorizontal");
        r += Input.GetAxis("K_Three_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float ThreeMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Three_MainVertikal");
        r += Input.GetAxis("K_Three_MainVertikal");
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
    public static float FourMainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Two_MainHorizontal");
        r += Input.GetAxis("K_Two_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // Z Werte werden abgefragt
    public static float FourMainVertikal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_Four_MainVertikal");
        r += Input.GetAxis("K_Four_MainVertikal");
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