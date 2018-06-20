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
        Debug.Log("Im Controller!");
    }

    public Controller(int playerID)
    {
        this.playerID = playerID;
        Debug.Log("Im Controller!");
    }
}
        
