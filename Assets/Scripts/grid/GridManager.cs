using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    private bool[,] occupied;

    private int offsetX;
    private int offsetZ;

    void Awake()
    {
        offsetX = width / 2;
        offsetZ = height / 2;

        occupied = new bool[width, height];

        Debug.Log($"[GridManager] Init grid {width}x{height}, offsetX={offsetX}, offsetZ={offsetZ}");
    }

    // Grille -> Monde
    public Vector3 GetWorldPosition(int x, int z)
    {
        Vector3 worldPos = new Vector3(
            (x - offsetX) * cellSize,
            0,
            (z - offsetZ) * cellSize
        );

        Debug.Log($"[GridManager] Grid → World | grid({x},{z}) → world{worldPos}");
        return worldPos;
    }

    // Monde -> Grille
    public Vector2Int GetGridPosition(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize + offsetX);
        int z = Mathf.FloorToInt(worldPos.z / cellSize + offsetZ);

        Vector2Int gridPos = new Vector2Int(x, z);

        Debug.Log($"[GridManager] World → Grid | world{worldPos} → grid({x},{z})");
        return gridPos;
    }

    public bool CanPlace(Vector2Int pos)
    {
        Debug.Log($"[GridManager] CanPlace check grid({pos.x},{pos.y})");

        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
        {
            Debug.LogWarning("[GridManager] ❌ Out of bounds");
            return false;
        }

        bool free = !occupied[pos.x, pos.y];
        Debug.Log(free ? "[GridManager] ✅ Cell free" : "[GridManager] ❌ Cell occupied");
        return free;
    }

    public void Occupy(Vector2Int pos)
    {
        Debug.Log($"[GridManager] Occupy grid({pos.x},{pos.y})");
        occupied[pos.x, pos.y] = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        int ox = width / 2;
        int oz = height / 2;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(
                    (x - ox) * cellSize,
                    0,
                    (z - oz) * cellSize
                );

                Gizmos.DrawWireCube(
                    pos + new Vector3(cellSize / 2, 0, cellSize / 2),
                    new Vector3(cellSize, 0.01f, cellSize)
                );
            }
        }
    }
}
