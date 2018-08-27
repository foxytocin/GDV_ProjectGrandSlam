using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	ObjectPooler objectPooler;
	const int breite = 29;
	const int hoehe = 18;
	private MazeCell[,] Maze;
	private MazeCell current;
	private int[,] mazeDataMap;

	private Stack<MazeCell> backtrack;
	public string[][] mazeLevelData;
	public bool mazeCalculated;

	void Awake()
	{
		mazeCalculated = false;
		objectPooler = ObjectPooler.Instance;
		Maze = new MazeCell[breite, hoehe];
		backtrack = new Stack<MazeCell>();
	}

	void Start()
	{
		initializeMaze();
		StartCoroutine(generateMaze());
	}

	public void generateNewMaze()
	{
		mazeCalculated = false;
		initializeMaze();
		StartCoroutine(generateMaze());
	}

	public void initializeMaze()
	{
		for(int j = 1; j < hoehe; j+=2)
			for(int i = 1; i < breite; i+=2)
			{
				Maze[i, j] = new MazeCell(i, j);
			}
	}

	public IEnumerator generateMaze()
	{
		bool done = false;
		MazeCell next = null;
		current = Maze[15, 1];
		current.visited = true;
		
		while(!done)
		{
			next = checkNeighbors(current);

			if(next != null)
			{
				backtrack.Push(current);
				removeWalls(current, next);
				current = next;
				current.visited = true;

			} else if(backtrack.Count > 0){
				
				current = backtrack.Pop();
			} else {
				done = true;
			}
			yield return null;
		}

		generateMazeBinaerMap();
		generateMazeLevelData();
		StopAllCoroutines();
	}

	private void removeWalls(MazeCell current, MazeCell next)
	{
		int x = current.x - next.x;
		if(x == 2) {
			current.wall[3] = false;
			next.wall[1] = false;
		} else if(x == -2) {
			current.wall[1] = false;
			next.wall[3] = false;
		}

		int y = current.y - next.y;
		if(y == 2) {
			current.wall[2] = false;
			next.wall[0] = false;
		} else if(y == -2) {
			current.wall[0] = false;
			next.wall[2] = false;
		}

		if(current.y == 1) {
			current.wall[2] = false;
		} else if(current.y == breite - 2) {
			current.wall[0] = false;
		}

	}

	private MazeCell checkNeighbors(MazeCell MazeCell)
	{
		List<MazeCell> neighbors = new List<MazeCell>();

		MazeCell top = null;
		MazeCell right = null;
		MazeCell bottom = null;
		MazeCell left = null;

		if(current.y + 2 < hoehe)
			top = Maze[current.x, current.y + 2];

		if(current.x + 2 < breite)
			right = Maze[current.x + 2, current.y];
		
		if(current.y - 2 > 0)
			bottom = Maze[current.x , current.y - 2];

		if(current.x - 2 > 0)
			left = Maze[current.x - 2, current.y];


		if(top != null && !top.visited) {
			neighbors.Add(top);
		}
		if(right != null && !right.visited) {
			neighbors.Add(right);
		}
		if(bottom != null && !bottom.visited) {
			neighbors.Add(bottom);
		}
		if(left != null && !left.visited) {
			neighbors.Add(left);
		}

		//Debug.Log("NeighborsCount: " +neighbors.Count);
		if(neighbors.Count > 0)
		{
			int pick = (int)Random.Range(0f, neighbors.Count);
			//Debug.Log("NeighborsPick: " +pick);
			return neighbors[pick];
		} else {
			return null;
		}
	}


	public void generateMazeBinaerMap()
	{	
		MazeCell current;
        mazeDataMap = new int[breite + 1, hoehe + 1];

        for (int j = 0; j < hoehe; j++)
        {
			 for (int i = 0; i < breite; i++)
        	{
				current = Maze[i, j];

				if(current == null)
				{
					mazeDataMap[i, j] = 0;

				} else if(current.visited) {

					mazeDataMap[i, j] = 1;

					for(int w = 0; w < 4; w++)
					{
						if(current.wall[w])
						{
							// Generiere eine Wand
							switch(w)
							{
								case 0: //top
									mazeDataMap[i, j + 1] = 2;
									break;
								case 1: //right
									mazeDataMap[i + 1, j] = 2;
									break;
								case 2: //bottom
									mazeDataMap[i, j - 1] = 2;
									break;
								case 3: //left
									mazeDataMap[i - 1, j] = 2;
									break;
							}
						} else {

							// Generiere einen Gang
							switch(w)
							{
								case 0: //top
									mazeDataMap[i, j + 1] = 1;
									break;
								case 1: //right
									mazeDataMap[i + 1, j] = 1;
									break;
								case 2: //bottom
									mazeDataMap[i, j - 1] = 1;
									break;
								case 3: //left
									mazeDataMap[i - 1, j] = 1;
									break;
							}
						}
					}
				}
       		}
		}

		// DEBUG:
		// for (int j = 0; j < hoehe; j++)
		// 	for (int i = 0; i < breite; i++)
		// 		Debug.Log("mazeDataMap: Wert: " +mazeDataMap[i,j]+ " Pos : " +i+ " / " +j);
	}


	public void generateMazeLevelData()
	{
		int current;
        mazeLevelData = new string[hoehe][];

        for (int j = 0; j < hoehe; j++)
        {
			string[] mazeLine = new string[breite + 2];

			for (int i = 0; i < breite; i++)
        	{
				current = mazeDataMap[i,j];

				if(current == 0)
				{
					mazeLine[i + 1] = "x";
				}

				if(current == 1)
				{
					mazeLine[i + 1] = "o";
				}

				if(current == 2)
				{
					mazeLine[i + 1] = "x";
				}
       		}
			
			mazeLevelData[j] = mazeLine;
        }

		mazeCalculated = true;
		Debug.Log("MAZE READY");
	}


}
