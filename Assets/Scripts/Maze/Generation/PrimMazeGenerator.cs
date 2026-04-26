using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
public class PrimMazeGenerator : IMazeGenerator
{
    public void Generate(MazeGrid grid, Random rng)
    {

        bool[,] visited = new bool[grid.Width, grid.Height];
        List<Vector2Int> frontier = new();

        Vector2Int start = grid.PickRandomCell(rng);
        visited[start.x, start.y] = true;

        foreach (var (cell, _) in grid.GetNeighbors(start, neighbor => !visited[neighbor.x, neighbor.y] && !frontier.Contains(neighbor)))
        {
            frontier.Add(cell);
        }

        while (frontier.Count > 0)
        {
            int index = rng.Next(frontier.Count);
            Vector2Int next = frontier[index];
            frontier.RemoveAt(index);

            List<(Vector2Int, Direction)> visitedNeighbors = grid.GetNeighbors(next, neighbor => visited[neighbor.x, neighbor.y]).ToList();

            if (visitedNeighbors.Count == 0)
            {
                continue;
            }

            var (visitedNeighbor, direction) = visitedNeighbors[rng.Next(visitedNeighbors.Count)];
            grid.Connect(next, direction);

            visited[next.x, next.y] = true;

            foreach (var (cell, _) in grid.GetNeighbors(next, neighbor => !visited[neighbor.x, neighbor.y] && !frontier.Contains(neighbor)))
            {
                frontier.Add(cell);
            }
        }
    }
}
