using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : MonoBehaviour {

    public LevelGenerator levelGenerator;

    public void remoteBomb(int playerID) {

        GameObject[] bombArray = GameObject.FindGameObjectsWithTag("Bombe");

        for (int i = 0; i < bombArray.Length; i++)
        {
            if (bombArray[i].GetComponent<BombScript>().bombOwnerPlayerID == playerID && bombArray[i].GetComponent<BombScript>().remoteBomb == true)
            {
                bombArray[i].GetComponent<BombScript>().countDown = 0f;
                bombArray[i].GetComponent<BombScript>().remoteBomb = false;
            }
        }
        

        
    }
}