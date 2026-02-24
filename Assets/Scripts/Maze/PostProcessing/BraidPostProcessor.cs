using System;
using System.Collections.Generic;

public class BraidPostProcessor : IMazePostProcessor
{
    private readonly int _minWallThreshold;
    private readonly float _removalChance;
    private readonly int _minRemovalPerCell;
    private readonly int _maxRemovalPerCell;

    public BraidPostProcessor(int minWallThreshold = 3, float removalChance = 0.6f, int minRemovalPerCell = 1, int maxRemovalPerCell = 3)
    {
        _minWallThreshold = minWallThreshold;
        _removalChance = (float)Math.Clamp(removalChance, 0.0, 1.0);
        _minRemovalPerCell = minRemovalPerCell;
        _maxRemovalPerCell = maxRemovalPerCell;
    }

    public void Process(MazeGrid grid, Random rng)
    {
        List<Direction> candidates = new(4);

        foreach (var (position, cell) in grid.Cells)
        {
            if (cell.CountWalls() < _minWallThreshold)
                continue;

            candidates.Clear();
            int removed = 0;

            foreach (var direction in DirectionExtensions.All)
            {
                if (!cell.HasWall(direction))
                    continue;

                if (!grid.IsInBounds(position + direction.ToOffset()))
                    continue;

                candidates.Add(direction);
            }

            Shuffle(candidates, rng);

            foreach (var direction in candidates)
            {
                if (removed >= _maxRemovalPerCell)
                    break;
                if (removed < _minRemovalPerCell || rng.NextDouble() < _removalChance)
                {
                    grid.Connect(position, direction);
                    removed++;
                }
            }
        }
    }

    private void Shuffle<T>(IList<T> list, Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
