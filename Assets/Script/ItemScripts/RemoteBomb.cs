using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : MonoBehaviour {

    public LevelGenerator levelGenerator;
    public int playerID;

    public void remoteBomb(int playerID)
    {
        this.playerID = playerID;
        StartCoroutine(Remote());
    }

    public IEnumerator Remote()
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
                    yield return new WaitForSeconds(bombScript.bombPower * 0.06f + 0.06f);
                }
            }
        }
    }
}