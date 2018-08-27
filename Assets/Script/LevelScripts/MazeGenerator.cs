using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	ObjectPooler objectPooler;
	private int breite = 29;
	private int hoehe = 17;
	public MazeCell[,] Maze;
	public MazeCell current;
	private Stack<MazeCell> backtrack;

	void Awake()
	{
		objectPooler = ObjectPooler.Instance;
		Maze = new MazeCell[breite, hoehe];
		backtrack = new Stack<MazeCell>();
	}

	void Start()
	{
		initializeMaze();
		StartCoroutine(generateMaze());
	}

	public void initializeMaze()
	{
		for(int j = 1; j < hoehe; j+=2)
			for(int i = 2; i < breite; i+=2)
			{
				Maze[i, j] = new MazeCell(i, j);
			}
	}


	public IEnumerator generateMaze()
	{
		bool done = false;
		MazeCell next = null;
		current = Maze[16, 1];
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

		Debug.Log("NeighborsCount: " +neighbors.Count);
		if(neighbors.Count > 0)
		{
			int pick = (int)Random.Range(0f, neighbors.Count);
			Debug.Log("NeighborsPick: " +pick);
			return neighbors[pick];
		} else {
			return null;
		}
	}

	public void drawMaze(int rowOffset)
	{

		for(int j = 0; j < hoehe + 1; j+=2)
			for(int i = 1; i < breite + 1; i+=2)
			{
				objectPooler.SpawnFromPool("Wand", new Vector3(i, 0.5f, j + rowOffset), Quaternion.identity);
			}

		MazeCell current;
		for(int j = 1; j < hoehe; j+=2)
		{
			for(int i = 2; i < breite; i+=2)
			{
				current = Maze[i, j];

				// if(current.visited)
				// 	Instantiate(Visited_Prefab, new Vector3(current.x, 0, current.y), Quaternion.identity);

				for(int w = 0; w < 4; w++)
				{
					if(current.wall[w])
					{
						switch(w)
						{
							case 0: //top
								objectPooler.SpawnFromPool("Wand", new Vector3(current.x, 0.5f, current.y + 1 + rowOffset), Quaternion.identity);
								break;
							case 1: //right
								objectPooler.SpawnFromPool("Wand", new Vector3(current.x + 1 , 0.5f, current.y + rowOffset), Quaternion.identity);
								break;
							case 2: //bottom
								objectPooler.SpawnFromPool("Wand", new Vector3(current.x, 0.5f, current.y - 1 + rowOffset), Quaternion.identity);
								break;
							case 3: //left
								objectPooler.SpawnFromPool("Wand", new Vector3(current.x - 1, 0.5f, current.y + rowOffset), Quaternion.identity);
								break;
						}
					}
				}
			}
		}
	}


}
