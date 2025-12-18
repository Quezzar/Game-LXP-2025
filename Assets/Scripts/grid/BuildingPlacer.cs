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
            Debug.Log("[BuildingPlacer] Mouse click detected");
            TryPlaceBuilding();
        }
    }

    public void EnableBuildMode()
    {
        buildMode = true;
        Debug.Log("[BuildingPlacer] 🏗️ Build mode ENABLED");
    }

    void TryPlaceBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"[BuildingPlacer] Raycast hit: {hit.collider.name} at {hit.point}");

            Vector2Int gridPos = grid.GetGridPosition(hit.point);

            Debug.Log($"[BuildingPlacer] Grid position calculated: ({gridPos.x},{gridPos.y})");

            if (grid.CanPlace(gridPos))
            {
                Vector3 spawnPos = grid.GetWorldPosition(gridPos.x, gridPos.y);
                Instantiate(buildingPrefab, spawnPos, Quaternion.identity);
                grid.Occupy(gridPos);

                Debug.Log("[BuildingPlacer] ✅ Building placed");
            }
            else
            {
                Debug.Log("[BuildingPlacer] ❌ Cannot place building here");
            }
        }
        else
        {
            Debug.Log("[BuildingPlacer] ❌ Raycast did not hit anything");
        }
    }
}
