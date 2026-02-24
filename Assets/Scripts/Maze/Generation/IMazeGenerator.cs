using System;

public interface IMazeGenerator
{
    public void Generate(MazeGrid grid, Random rng);
}
