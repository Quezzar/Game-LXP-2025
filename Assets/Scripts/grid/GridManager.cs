using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 100;
    public int height = 100;
    public float cellSize = 1f;

    [Header("Visual Settings")]
    public Material freeMaterial;
    public Material occupiedMaterial;
    public float visualHeight = 0.01f;

    private bool[,] occupied;
    private GameObject[,] visuals;

    private int offsetX;
    private int offsetZ;

    void Awake()
    {
        offsetX = width / 2;
        offsetZ = height / 2;

        occupied = new bool[width, height];
        visuals = new GameObject[width, height];

        CreateVisualGrid();
        HideGridVisuals();

        Debug.Log("[GridManager] Grid initialized (centered & fixed)");
    }

    // ================= GRID LOGIC =================

    public Vector3 GetWorldPosition(int x, int z)
    {
        // 🔹 POSITION AU CENTRE DE LA CASE
        float worldX = (x - offsetX + 0.5f) * cellSize;
        float worldZ = (z - offsetZ + 0.5f) * cellSize;

        return new Vector3(worldX, 0f, worldZ);
    }

    public Vector2Int GetGridPosition(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize + offsetX);
        int z = Mathf.FloorToInt(worldPos.z / cellSize + offsetZ);

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
        UpdateVisual(pos);
    }

    // ================= VISUAL GRID =================

    void CreateVisualGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cell.name = $"Cell_{x}_{z}";
                cell.transform.parent = transform;

                Vector3 pos = GetWorldPosition(x, z);
                pos.y = visualHeight;

                cell.transform.position = pos;
                cell.transform.rotation = Quaternion.Euler(90, 0, 0);
                cell.transform.localScale = Vector3.one * cellSize;

                Destroy(cell.GetComponent<Collider>());

                cell.GetComponent<MeshRenderer>().material = freeMaterial;

                visuals[x, z] = cell;
            }
        }
    }

    public void ShowGridVisuals()
    {
        foreach (GameObject cell in visuals)
            cell.SetActive(true);
    }

    public void HideGridVisuals()
    {
        foreach (GameObject cell in visuals)
            cell.SetActive(false);
    }

    void UpdateVisual(Vector2Int pos)
    {
        visuals[pos.x, pos.y]
            .GetComponent<MeshRenderer>()
            .material = occupiedMaterial;
    }

    public void Free(Vector2Int pos)
{
    if (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height)
    {
        occupied[pos.x, pos.y] = false;
        UpdateVisual(pos); // Remet la couleur "Libre"
    }
}
}
