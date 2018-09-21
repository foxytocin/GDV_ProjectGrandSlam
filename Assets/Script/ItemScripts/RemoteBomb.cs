using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Zuendet die eigenen Remote-Bomben des Players wenn dieser den Zuenden-Button drueckt
public class RemoteBomb : MonoBehaviour
{

    // Sucht alle Bomben die aktiv im Spielfeld liegen
    // Setzt bei den Bomben mit der richtigen PlayerID (vom Player der den Zuenden-Button gedruckt hat) den
    // Counter = 0 damit die Bombe explodiert sobald im naechsten Schritt die remoteBomb zu einer normalen Bombe gemacht wird (romoteBomb = false)
    public void remoteBomb(int playerID)
    {
        GameObject[] bombArray = GameObject.FindGameObjectsWithTag("Bombe");

        foreach (GameObject go in bombArray)
        {
            if (go != null)
            {
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