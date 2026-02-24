using UnityEngine;

public class MazeFactoryTest
{
    //[RuntimeInitializeOnLoadMethod]
    //private static void Run()
    //{
    //    var grid = MazeFactory.Create(
    //        10,
    //        10,
    //        new PrimMazeGenerator(),
    //        new[] { new WallRemovalPostProcessor(3, 0.6f) },
    //        new System.Random(1306)
    //    );
    //    PrintMaze(grid);
    //}

    public static void PrintMaze(MazeGrid grid)
    {
        int width = grid.Width;
        int height = grid.Height;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                sb.Append("+");
                sb.Append(grid[x, y].HasWall(Direction.North) ? "----" : "    ");
            }
            sb.AppendLine("+");

            for (int x = 0; x < width; x++)
            {
                sb.Append(grid[x, y].HasWall(Direction.West) ? "|" : " ");
                sb.Append("    ");
            }

            sb.AppendLine(grid[width - 1, y].HasWall(Direction.East) ? "|" : " ");
        }

        for (int x = 0; x < width; x++)
        {
            sb.Append("+");
            sb.Append(grid[x, 0].HasWall(Direction.South) ? "----" : "    ");
        }
        sb.AppendLine("+");

        System.IO.File.WriteAllText(
            System.IO.Path.Combine("D:/blank.txt"),
            sb.ToString()
        );
    }
}
