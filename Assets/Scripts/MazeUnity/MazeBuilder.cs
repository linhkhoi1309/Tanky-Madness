using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer floorRenderer;

    [SerializeField]
    GameObject wallPrefab;

    [SerializeField]
    GameObject wallContainer;

    public void Build(MazeGrid grid)
    {
        floorRenderer.size = new Vector2(grid.Width, grid.Height);
        floorRenderer.transform.position = new Vector3(grid.Width / 2f, grid.Height / 2f, 0);

        foreach (var (position, cell) in grid.Cells)
        {
            if (cell.HasWall(Direction.South))
            {
                Vector3 pos = new(position.X + 0.5f, position.Y, 0);
                GameObject wall = Instantiate(wallPrefab, pos, Quaternion.identity, wallContainer.transform);
                wall.name = $"V_Wall_{position.X}_{position.Y}";
            }

            if (cell.HasWall(Direction.West))
            {
                Vector3 pos = new(position.X, position.Y + 0.5f, 0);
                Quaternion rot = Quaternion.Euler(0, 0, 90f);
                GameObject wall = Instantiate(wallPrefab, pos, rot, wallContainer.transform);
                wall.name = $"H_Wall_{position.X}_{position.Y}";
            }

            if (position.X == grid.Width - 1 && cell.HasWall(Direction.East))
            {
                Vector3 pos = new(position.X + 1f, position.Y + 0.5f, 0);
                Quaternion rot = Quaternion.Euler(0, 0, 90f);
                GameObject wall = Instantiate(wallPrefab, pos, rot, wallContainer.transform);
                wall.name = $"H_Wall_{position.X}_E";
            }

            if (position.Y == grid.Height - 1 && cell.HasWall(Direction.North))
            {
                Vector3 pos = new(position.X + 0.5f, position.Y + 1f, 0);
                GameObject wall = Instantiate(wallPrefab, pos, Quaternion.identity, wallContainer.transform);
                wall.name = $"V_Wall_{position.X}_N";
            }
        }
    }
}
