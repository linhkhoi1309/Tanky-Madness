using System;
using System.Collections.Generic;

public class MazeGrid
{
    public int Width { get; }
    public int Height { get; }

    private readonly MazeCell[,] _cells;
    public MazeCell this[int x, int y] => _cells[x, y];

    public IEnumerable<(Vector2i pos, MazeCell cell)> Cells
    {
        get
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    yield return (new Vector2i(x, y), _cells[x, y]);
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

    public Vector2i PickRandomCell(Random rng)
    {
        return new Vector2i(rng.Next(0, Width), rng.Next(0, Height));
    }

    public bool IsInBounds(Vector2i cell)
    {
        return cell.X >= 0 && cell.X < Width && cell.Y >= 0 && cell.Y < Height;
    }

    public IEnumerable<(Vector2i position, Direction direction)> GetNeighbors(Vector2i cell, Func<Vector2i, bool> condition)
    {
        foreach (var direction in DirectionExtensions.All)
        {
            var offset = direction.ToOffset();
            Vector2i neighbor = cell + offset;

            if (!IsInBounds(neighbor) || !condition(neighbor))
                continue;

            yield return (neighbor, direction);
        }
    }

    public void Connect(Vector2i from, Direction direction)
    {
        var offset = direction.ToOffset();
        var to = from + offset;

        _cells[from.X, from.Y].RemoveWall(direction);
        _cells[to.X, to.Y].RemoveWall(direction.Opposite());
    }
}
