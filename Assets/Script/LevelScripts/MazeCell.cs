using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CellObject fuer den MazeGenerator
public class MazeCell
{
    public bool visited = false;
    public List<bool> wall;
    public int x;
    public int y;

    public MazeCell(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.wall = new List<bool>() { true, true, true, true }; // Reihenfolge: top, right, bottom, left
    }
}