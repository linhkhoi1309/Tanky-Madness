using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DFSMazeGenerator : IMazeGenerator
{
    public void Generate(MazeGrid grid, Random rng)
    {
        bool[,] visited = new bool[grid.Width, grid.Height];
        Stack<Vector2Int> stack = new();

        Vector2Int start = grid.PickRandomCell(rng);
        visited[start.x, start.y] = true;
        stack.Push(start);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();

            List<(Vector2Int, Direction)> unvisitedNeighbors = grid.GetNeighbors(current, neighbor => !visited[neighbor.x, neighbor.y]).ToList();

            if (unvisitedNeighbors.Count == 0)
            {
                stack.Pop();
                continue;
            }

            var (next, direction) = unvisitedNeighbors[rng.Next(unvisitedNeighbors.Count)];

            grid.Connect(current, direction);

            visited[next.x, next.y] = true;
            stack.Push(next);
        }
    }
}
