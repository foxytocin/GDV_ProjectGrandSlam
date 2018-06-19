using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    int playerID;
    GameObject body;

    public Controller(int playerID, GameObject body)
    {
        this.playerID = playerID;
        this.body = body;

    }

    void move()
    {
        body.transform.Translate(InputManager.MainJoystick());
    }

	void Update ()
    {
        move();

        if (InputManager.XButton())
            Debug.Log(InputManager.MainJoystick());

	}
}
