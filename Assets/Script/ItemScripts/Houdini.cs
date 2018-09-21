using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Laesst den Player durch Kisten hindurchrennen, waehrend diese automatisch explodieren
public class Houdini : MonoBehaviour
{

    private LevelGenerator LevelGenerator;
    public GameObject KistenteilePrefab;

    void Awake()
    {
        LevelGenerator = FindObjectOfType<LevelGenerator>();
    }

    // Hat der Player das Houdini-Item gesamelt wird diese Funktion bei jeder Bewegung aufrufen und ueberprueft die umliegenden Felder auf Kisten
    public void callHoudini(int xPos, int zPos)
    {
        explode(xPos, zPos + 1);
        explode(xPos, zPos - 1);
        explode(xPos + 1, zPos);
        explode(xPos - 1, zPos);
    }

    // Wir eine Kiste gefunden, wird diese deaktiviert und die Kistenzerstoerung instanziert
    void explode(int xPos, int zPos)
    {
        if (LevelGenerator.AllGameObjects[xPos, zPos] != null)
        {
            GameObject go = LevelGenerator.AllGameObjects[xPos, zPos].gameObject;

            switch (go.tag)
            {
                case "Kiste":
                    Instantiate(KistenteilePrefab, new Vector3(xPos, 0.5f, zPos), Quaternion.identity, transform);
                    LevelGenerator.AllGameObjects[xPos, zPos] = null;
                    go.SetActive(false);
                    break;

            }
        }
    }
}