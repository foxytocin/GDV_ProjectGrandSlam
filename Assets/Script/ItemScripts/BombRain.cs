using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRain : MonoBehaviour
{

    private CameraScroller cameraScroller;
    private LevelGenerator levelGenerator;
    private GameManager gameManager;
    public bool bombenregen = false;
    private BombSpawner bombSpawner;
    public Color32 bombraincolor;
    public int PlayerID;
    public float bombYPos = 10f;


    void Awake()
    {
        bombSpawner = FindObjectOfType<BombSpawner>();
        bombraincolor = new Color32(0, 0, 0, 0);
        cameraScroller = FindObjectOfType<CameraScroller>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {

        if (Random.value > 0.95f && bombenregen == true && gameManager.gameStatePlay)
        {
            Vector3Int bombPos = new Vector3Int((int)Random.Range(2f, 29f), 0, cameraScroller.rowPosition + (int)Random.Range(0f, 50f));
            StartCoroutine(checkWorld(bombPos));

        }
    }


    private IEnumerator checkWorld(Vector3Int bombPos)
    {
        int x = bombPos.x;
        int z = bombPos.z;

        GameObject go;
        GameObject bombego;

        if (levelGenerator.SecondaryGameObjects1[x, z] != null)
        {
            if (levelGenerator.SecondaryGameObjects1[x, z].gameObject.CompareTag("Boden") && levelGenerator.AllGameObjects[x, z] == null)
            {
                bombego = bombSpawner.SpawnBomb(bombPos.x, bombYPos, bombPos.z, 5, 1, 3, false, true, bombraincolor);
            }

            if (levelGenerator.AllGameObjects[x, z] != null)
            {
                go = levelGenerator.AllGameObjects[x, z].gameObject;

                switch (go.tag)
                {
                    case "Kiste":
                        bombego = bombSpawner.SpawnBomb(bombPos.x, bombYPos, bombPos.z, 5, 1, 3, false, true, bombraincolor);

                        break;

                    case "Item":
                        bombego = bombSpawner.SpawnBomb(bombPos.x, bombYPos, bombPos.z, 5, 1, 3, false, true, bombraincolor);

                        break;

                    case "Player":
                        bombego = bombSpawner.SpawnBomb(bombPos.x, bombYPos, bombPos.z, 5, 1, 3, false, true, bombraincolor);
                        break;

                    case "Wand":

                        break;

                    case "Bombe":

                        break;

                    default:
                        break;
                }
            }
        }
        yield return null;
    }



}
