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


	// UPDATE KANN SPÄTER AUS DEM CODE
	void Update () {

		if(Input.GetKeyDown("r"))
		{
			Time.timeScale = 1f;
			StartCoroutine( eraseCurrentWorld());
		}
		
	}

	public void levelRestart()
	{
		Time.timeScale = 1f;
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
		//playerSpawner.createPlayer();
		cameraScroller.restartCameraScroller();
		destroyScroller.restartDestroyScroller();
		levelGenerator.restartLevel();
	}

}
