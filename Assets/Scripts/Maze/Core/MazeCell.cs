public class MazeCell
{
    public Direction Walls { get; private set; } = Direction.North | Direction.East | Direction.South | Direction.West;

    public int CountWalls()
    {
        uint walls = (uint)Walls;
        int count = 0;

        while (walls != 0)
        {
            walls &= walls - 1;
            count++;
        }

        return count;
    }

    public bool HasWall(Direction direction)
    {
        return (Walls & direction) != 0;
    }

    public void RemoveWall(Direction direction)
    {
        Walls &= ~direction;
    }
}
