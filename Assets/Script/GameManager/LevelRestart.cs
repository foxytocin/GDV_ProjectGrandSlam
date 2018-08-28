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


	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("r"))
		{
			StartCoroutine( eraseCurrentWorld());
		}
		
	}

	public IEnumerator eraseCurrentWorld()
	{
		int destroyScrollerPos = destroyScroller.dummyPos;

		for(int i = 0; i < 70; i++)
		{
			levelGenerator.cleanLine(destroyScrollerPos + i);
		}

		yield return new WaitForSecondsRealtime(4f);
		recreateWorld();
	}

	public void recreateWorld()
	{
		//playerSpawner.createPlayer();
		cameraScroller.restartCameraScroller();
		destroyScroller.restartDestroyScroller();
		levelGenerator.restartLevel();
	}

}
