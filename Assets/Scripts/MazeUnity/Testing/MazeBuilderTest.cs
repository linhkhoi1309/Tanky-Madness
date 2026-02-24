using UnityEngine;

public class MazeBuilderTest : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private int seed = 1306;
    [SerializeField] private Color backgroundColor;

    void Start()
    {
        var mazeBuilder = GetComponent<MazeBuilder>();

        var grid = MazeFactory.Create(
            size.x, size.y,
            new PrimMazeGenerator(),
            new[] { new BraidPostProcessor(3, 0.6f) },
            new System.Random(seed)
        );

        mazeBuilder.Build(grid);

        MazeFactoryTest.PrintMaze(grid);

        GameObject camObj = new("Camera");

        camObj.transform.SetParent(this.transform);

        Camera newCam = camObj.AddComponent<Camera>();

        newCam.orthographic = true;
        newCam.orthographicSize = 5f;
        newCam.backgroundColor = backgroundColor;

        camObj.transform.position = new Vector3(grid.Width / 2f, grid.Height / 2f, -10);

    }
}
