using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GridManager grid;
    public GameObject buildingPrefab;

    private bool buildMode = false;

    void Update()
    {
        if (!buildMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceBuilding();
        }
    }

    public void EnableBuildMode()
    {
        buildMode = true;
    }

    void TryPlaceBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Ground")))
        {
            Vector2Int gridPos = grid.GetGridPosition(hit.point);

            if (grid.CanPlace(gridPos))
            {
                Vector3 spawnPos = grid.GetWorldPosition(gridPos.x, gridPos.y);
                Instantiate(buildingPrefab, spawnPos, Quaternion.identity);
                grid.Occupy(gridPos);
            }
        }
    }
}
