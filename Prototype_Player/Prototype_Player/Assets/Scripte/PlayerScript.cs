using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    GameObject body;
    int playerID;
    int life;
    int avaibleBombs;
    int speed;
    int range;

	// Use this for initialization
	public PlayerScript(int playerID)
    {
        this.playerID = playerID;
        body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Player_" + playerID.ToString();
        Debug.Log(body.name);
        life = 3;
        avaibleBombs = 1;
        speed = 1;
        range = 1;
    }

    public void setBomb()
    { 
        Debug.Log("Bombe_Player_"+ playerID.ToString());
        //placeBomb(body.trasform.position, playerID, range)
        //avaible--;
    }

    // Update is called once per frame
    public void  move()
    {
        switch(playerID)
        {
            case 0:
                Debug.Log("move" + playerID.ToString());
                body.transform.Translate(InputManager.OneMainJoystick());
                break;

            case 1:
                Debug.Log("move" + playerID.ToString());
                body.transform.Translate(InputManager.TwoMainJoystick());
                break;

            case 2:
                Debug.Log("move" + playerID.ToString());
                body.transform.Translate(InputManager.ThreeMainJoystick());
                break;

            case 3:
                Debug.Log("move" + playerID.ToString());
                body.transform.Translate(InputManager.FourMainJoystick());
                break;

            default:
                Debug.Log("move_Exeption");
                break;

        }
       
    }

}
