using System;

public class WallRemovalPostProcessor : IMazePostProcessor
{
    private readonly int _minWallCount;
    private readonly float _removalChance;

    public WallRemovalPostProcessor(int minWallCount = 3, float removalChance = 0.6f)
    {
        _minWallCount = minWallCount;
        _removalChance = removalChance;
    }

    public void Process(MazeGrid grid, Random rng)
    {
        foreach (var (position, cell) in grid.Cells)
        {
            if (cell.CountWalls() < _minWallCount)
                continue;

            foreach (var direction in DirectionExtensions.All)
            {
                if (!cell.HasWall(direction))
                    continue;

                if (!grid.IsInBounds(position + direction.ToOffset()))
                    continue;

                if (rng.NextDouble() > _removalChance)
                    continue;

                grid.Connect(position, direction);
            }
        }
    }
}
