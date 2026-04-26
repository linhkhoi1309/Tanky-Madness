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
                Vector3 pos = new(position.x + 0.5f, position.y, 0);
                GameObject wall = Instantiate(wallPrefab, pos, Quaternion.identity, wallContainer.transform);
                wall.name = $"V_Wall_{position.x}_{position.y}";
            }

            if (cell.HasWall(Direction.West))
            {
                Vector3 pos = new(position.x, position.y + 0.5f, 0);
                Quaternion rot = Quaternion.Euler(0, 0, 90f);
                GameObject wall = Instantiate(wallPrefab, pos, rot, wallContainer.transform);
                wall.name = $"H_Wall_{position.x}_{position.y}";
            }

            if (position.x == grid.Width - 1 && cell.HasWall(Direction.East))
            {
                Vector3 pos = new(position.x + 1f, position.y + 0.5f, 0);
                Quaternion rot = Quaternion.Euler(0, 0, 90f);
                GameObject wall = Instantiate(wallPrefab, pos, rot, wallContainer.transform);
                wall.name = $"H_Wall_{position.x}_E";
            }

            if (position.y == grid.Height - 1 && cell.HasWall(Direction.North))
            {
                Vector3 pos = new(position.x + 0.5f, position.y + 1f, 0);
                GameObject wall = Instantiate(wallPrefab, pos, Quaternion.identity, wallContainer.transform);
                wall.name = $"V_Wall_{position.x}_N";
            }
        }
    }
}
