using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : MonoBehaviour {

    public PlayerScript PlayerScript;

    public void remoteBomb(int playerID) {
        

        foreach (GameObject bomb in PlayerScript.GetComponent<PlayerScript>().remoteBombList) {
            
            //Debug.Log("Bombe mit der ID: " +bomb.GetComponent<BombScript>().bombOwnerPlayerID);
            //if(bomb.GetComponent<BombScript>().bombOwnerPlayerID == playerID) 
            PlayerScript.GetComponent<PlayerScript>().remoteBombList.RemoveAt(PlayerScript.GetComponent<PlayerScript>().remoteBombList.IndexOf(bomb));
            bomb.GetComponent<BombScript>().bombTimer = 0;
            bomb.GetComponent<BombScript>().remoteBomb = false; 
            //}
        }
    }
}