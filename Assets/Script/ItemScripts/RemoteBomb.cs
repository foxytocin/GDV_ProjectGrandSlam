using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : MonoBehaviour {

    public void remoteBomb(int playerID)
    {   
        GameObject[] bombArray = GameObject.FindGameObjectsWithTag("Bombe");

        foreach (GameObject go in bombArray)
        {
            if(go != null) {

                BombScript bombScript = go.GetComponent<BombScript>();

                if (bombScript.bombOwnerPlayerID == playerID && bombScript.remoteBomb == true)
                {
                    bombScript.countDown = 0f;
                    bombScript.remoteBomb = false;
                }
            }
        }
    }
}