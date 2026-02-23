using System;

public static class DirectionExtensions
{
    public static readonly Direction[] All =
    {
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West
    };

    public static Vector2i ToOffset(this Direction dir) => dir switch
    {
        Direction.North => new Vector2i(0, 1),
        Direction.East => new Vector2i(1, 0),
        Direction.South => new Vector2i(0, -1),
        Direction.West => new Vector2i(-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null),
    };

    public static Direction Opposite(this Direction dir) => dir switch
    {
        Direction.North => Direction.South,
        Direction.East => Direction.West,
        Direction.South => Direction.North,
        Direction.West => Direction.East,
        _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null),
    };
}