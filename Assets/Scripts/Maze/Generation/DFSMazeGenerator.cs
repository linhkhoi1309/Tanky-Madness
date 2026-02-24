using System;
using System.Linq;
using System.Collections.Generic;

public class DFSMazeGenerator : IMazeGenerator
{
    public void Generate(MazeGrid grid, Random rng)
    {
        bool[,] visited = new bool[grid.Width, grid.Height];
        Stack<Vector2i> stack = new();

        Vector2i start = grid.PickRandomCell(rng);
        visited[start.X, start.Y] = true;
        stack.Push(start);

        while (stack.Count > 0)
        {
            Vector2i current = stack.Peek();

            List<(Vector2i, Direction)> unvisitedNeighbors = grid.GetNeighbors(current, neighbor => !visited[neighbor.X, neighbor.Y]).ToList();

            if (unvisitedNeighbors.Count == 0)
            {
                stack.Pop();
                continue;
            }

            var (next, direction) = unvisitedNeighbors[rng.Next(unvisitedNeighbors.Count)];

            grid.Connect(current, direction);

            visited[next.X, next.Y] = true;
            stack.Push(next);
        }
    }
}
