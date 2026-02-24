using System;
using System.Collections.Generic;
using System.Linq;
public class PrimMazeGenerator : IMazeGenerator
{
    public void Generate(MazeGrid grid, Random rng)
    {

        bool[,] visited = new bool[grid.Width, grid.Height];
        List<Vector2i> frontier = new();

        Vector2i start = grid.PickRandomCell(rng);
        visited[start.X, start.Y] = true;

        foreach (var (cell, _) in grid.GetNeighbors(start, neighbor => !visited[neighbor.X, neighbor.Y] && !frontier.Contains(neighbor)))
        {
            frontier.Add(cell);
        }

        while (frontier.Count > 0)
        {
            int index = rng.Next(frontier.Count);
            Vector2i next = frontier[index];
            frontier.RemoveAt(index);

            List<(Vector2i, Direction)> visitedNeighbors = grid.GetNeighbors(next, neighbor => visited[neighbor.X, neighbor.Y]).ToList();

            if (visitedNeighbors.Count == 0)
            {
                continue;
            }

            var (visitedNeighbor, direction) = visitedNeighbors[rng.Next(visitedNeighbors.Count)];
            grid.Connect(next, direction);

            visited[next.X, next.Y] = true;

            foreach (var (cell, _) in grid.GetNeighbors(next, neighbor => !visited[neighbor.X, neighbor.Y] && !frontier.Contains(neighbor)))
            {
                frontier.Add(cell);
            }
        }
    }
}
