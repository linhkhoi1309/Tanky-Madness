using System;

public interface IMazePostProcessor
{
    public void Process(MazeGrid grid, Random rng);
}
