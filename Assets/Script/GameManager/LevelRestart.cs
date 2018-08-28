using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestart : MonoBehaviour {


	private LevelGenerator levelGenerator;
	private DestroyScroller destroyScroller;
	private CameraScroller cameraScroller;
	private PlayerSpawner playerSpawner;

	void Awake()
	{
		levelGenerator = FindObjectOfType<LevelGenerator>();
		destroyScroller = FindObjectOfType<DestroyScroller>();
		cameraScroller = FindObjectOfType<CameraScroller>();
		playerSpawner = FindObjectOfType<PlayerSpawner>();
	}

	public void levelRestart()
	{
        Time.timeScale = 1f;

        for (int i = playerSpawner.players - 1; i >= 0; i--)
        {
            Color32 tmpPlayerColor = playerSpawner.playerList[i].GetComponent<Renderer>().material.color;

            for (int j = tmpPlayerColor.a; j > 0; j--)
            {
                tmpPlayerColor.a--;
            }

            Destroy(playerSpawner.playerList[i]);
        }

        StartCoroutine(eraseCurrentWorld());
	}

	private IEnumerator eraseCurrentWorld()
	{
		int destroyScrollerPos = destroyScroller.dummyPos;

		for(int i = 0; i < 70; i++)
		{
			levelGenerator.cleanLine(destroyScrollerPos + i);
		}

		yield return new WaitForSecondsRealtime(4f);
		StopAllCoroutines();
		recreateWorld();
	}

	private void recreateWorld()
	{
		cameraScroller.restartCameraScroller();
		destroyScroller.restartDestroyScroller();
		levelGenerator.restartLevel();
		playerSpawner.createPlayers();
	}

}
