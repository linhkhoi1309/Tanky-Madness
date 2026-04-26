using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MazeGrid
{
    public int Width { get; }
    public int Height { get; }

    private readonly MazeCell[,] _cells;
    public MazeCell this[int x, int y] => _cells[x, y];

    public IEnumerable<(Vector2Int pos, MazeCell cell)> Cells
    {
        get
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    yield return (new Vector2Int(x, y), _cells[x, y]);
                }
            }
        }
    }

    public MazeGrid(int width, int height)
    {
        Width = width;
        Height = height;

        _cells = new MazeCell[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _cells[x, y] = new MazeCell();
            }
        }
    }

    public Vector2Int PickRandomCell(Random rng)
    {
        return new Vector2Int(rng.Next(0, Width), rng.Next(0, Height));
    }

    public bool IsInBounds(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < Width && cell.y >= 0 && cell.y < Height;
    }

    public IEnumerable<(Vector2Int position, Direction direction)> GetNeighbors(Vector2Int cell, Func<Vector2Int, bool> condition)
    {
        foreach (var direction in DirectionExtensions.All)
        {
            var offset = direction.ToOffset();
            Vector2Int neighbor = cell + offset;

            if (!IsInBounds(neighbor) || !condition(neighbor))
                continue;

            yield return (neighbor, direction);
        }
    }

    public void Connect(Vector2Int from, Direction direction)
    {
        var offset = direction.ToOffset();
        var to = from + offset;

        _cells[from.x, from.y].RemoveWall(direction);
        _cells[to.x, to.y].RemoveWall(direction.Opposite());
    }
}
