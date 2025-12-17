using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public float cellSize = 1f;

    private bool[,] occupied;

    void Awake()
    {
        occupied = new bool[width, height];
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x * cellSize, 0, z * cellSize);
    }

    public Vector2Int GetGridPosition(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int z = Mathf.FloorToInt(worldPos.y / cellSize);
        return new Vector2Int(x, z);
    }

    public bool CanPlace(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
            return false;

        return !occupied[pos.x, pos.y];
    }

    public void Occupy(Vector2Int pos)
    {
        occupied[pos.x, pos.y] = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = GetWorldPosition(x, z);
                Gizmos.DrawWireCube(
                    pos + new Vector3(cellSize / 2, 0, cellSize / 2),
                    new Vector3(cellSize, 0.01f, cellSize)
                );
            }
        }
    }
}
