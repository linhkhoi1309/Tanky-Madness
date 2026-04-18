public class MazeCell
{
    public Direction Walls { get; private set; } = Direction.North | Direction.East | Direction.South | Direction.West;

    // Counts the number of walls in the cell using Brian Kernighan's algorithm for counting set bits.
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

    // Checks if the cell has a wall in the specified direction.
    public bool HasWall(Direction direction)
    {
        return (Walls & direction) != 0;
    }

    // Removes the wall in the specified direction.
    public void RemoveWall(Direction direction)
    {
        Walls &= ~direction;
    }
}
