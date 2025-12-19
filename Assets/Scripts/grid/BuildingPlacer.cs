using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [Header("References")]
    public GridManager grid;
    public GameObject buildingPrefab;

    private bool buildMode = false;

    void Update()
    {
        if (!buildMode)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceBuilding();
        }
    }

    public void EnableBuildMode()
    {
        buildMode = true;
        grid.ShowGridVisuals();

        Debug.Log("[BuildingPlacer] 🏗️ Build mode ENABLED");
    }

    void DisableBuildMode()
    {
        buildMode = false;
        grid.HideGridVisuals();

        Debug.Log("[BuildingPlacer] 🛑 Build mode DISABLED");
    }

    void TryPlaceBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector2Int gridPos = grid.GetGridPosition(hit.point);

            if (grid.CanPlace(gridPos))
            {
                Vector3 spawnPos = grid.GetWorldPosition(gridPos.x, gridPos.y);

                // 🔹 Correction hauteur (pivot centré)
                float buildingHeight = buildingPrefab
                    .GetComponent<Renderer>()
                    .bounds.size.y;

                spawnPos.y = buildingHeight / 2f;

                Instantiate(buildingPrefab, spawnPos, Quaternion.identity);
                grid.Occupy(gridPos);

                DisableBuildMode();
            }
        }
    }
}
