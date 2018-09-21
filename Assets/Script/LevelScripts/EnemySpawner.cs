using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private LevelGenerator levelGenerator;

    void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    public void spawnEnemy(int xPos, int zPos)
    {
        checkWorld(xPos, zPos);
    }

    private void checkWorld(int xPos, int zPos)
    {
        // Checkt ob die Stelle wo der Enemy gespawnt werden soll ein Stueck Boden ist
        if (levelGenerator.SecondaryGameObjects1[xPos, zPos] != null)
        {
            if (levelGenerator.SecondaryGameObjects1[xPos, zPos].gameObject.CompareTag("Boden") && levelGenerator.AllGameObjects[xPos, zPos] == null)
            {
                // Prueft ob der Enemy in mindestens eine Richtung gehen kann
                if (checkSurr(xPos + 1, zPos) ||
                    checkSurr(xPos - 1, zPos) ||
                    checkSurr(xPos, zPos + 1) ||
                    checkSurr(xPos, zPos - 1))
                {
                    Instantiate(enemyPrefab, new Vector3(xPos, 0.43f, zPos), Quaternion.identity);
                }
            }
        }
    }

    // Preuft die Umfelder des Spawnpunktes. Gibt es keine Ausweg, wird kein Enemy gespawnt (z.B. Zwischen Kisten eingesperrt)
    private bool checkSurr(int xPos, int zPos)
    {
        if (levelGenerator.SecondaryGameObjects1[xPos, zPos] != null)
        {
            if (levelGenerator.SecondaryGameObjects1[xPos, zPos].gameObject.CompareTag("Boden") && levelGenerator.AllGameObjects[xPos, zPos] == null)
            {
                return true;
            }
        }
        return false;
    }

}
