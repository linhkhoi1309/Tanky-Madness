using System;
using System.Collections.Generic;
using System.Linq;
//using UnityEngine;

public static class MazeFactory
{
    public static MazeGrid Create(int width, int height, IMazeGenerator generator, IEnumerable<IMazePostProcessor> postProcessors = null, Random rng = null)
    {
        postProcessors ??= Enumerable.Empty<IMazePostProcessor>();
        rng ??= new Random();

        MazeGrid grid = new MazeGrid(width, height);
        generator.Generate(grid, rng);

        foreach (var postProcessor in postProcessors)
        {
            postProcessor.Process(grid, rng);
        }

        return grid;
    }
}
