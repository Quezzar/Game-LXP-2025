using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [Header("References")]
    public GridManager grid;
    public GameObject Gold;

    [Header("Building Prefabs")]
    public GameObject turretPrefab;
    // public GameObject housePrefab; // Futur ajout

    private GameObject currentPrefab; 
    private int currentCost;          
    private bool buildMode = false;
    private bool destroyMode = false; // Nouveau mode

    public void Start()
    {
        Gold = GameObject.FindWithTag("Gold");
    }

    void Update()
    {
        // 1. Touche Échap pour tout annuler
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelAllModes();
        }

        // 2. Logique du mode Construction
        if (buildMode)
        {
            if (Input.GetMouseButtonDown(0)) TryPlaceBuilding();
        }
        
        // 3. Logique du mode Destruction
        if (destroyMode)
        {
            if (Input.GetMouseButtonDown(0)) TryDestroyBuilding();
        }
    }

    // ================= FONCTIONS UI =================

    public void BuildTurret()
    {
        currentPrefab = turretPrefab;
        currentCost = 10; 
        EnterBuildMode();
    }

    /* // FONCTION GÉNÉRIQUE POUR FUTUR BÂTIMENT
    public void BuildNomDuBatiment()
    {
        currentPrefab = nomDuPrefab;
        currentCost = 100; 
        EnterBuildMode();
    }
    */

    public void EnableDestroyMode()
    {
        CancelAllModes(); // On quitte la construction si on était dedans
        destroyMode = true;
        grid.ShowGridVisuals();
        Debug.Log("[BuildingPlacer] 🔨 Mode Destruction activé (Clic gauche pour détruire)");
    }

    // ================= LOGIQUE INTERNE =================

    private void EnterBuildMode()
    {
        destroyMode = false; // On quitte la destruction
        buildMode = true;
        grid.ShowGridVisuals();
    }

    private void CancelAllModes()
    {
        buildMode = false;
        destroyMode = false;
        currentPrefab = null;
        grid.HideGridVisuals();
        Debug.Log("[BuildingPlacer] 🛑 Modes annulés");
    }

    public void TryPlaceBuilding()
    {
        GoldCounter wallet = Gold.GetComponent<GoldCounter>();
        if (wallet.GoldCount < currentCost)
        {
            Debug.Log("[BuildingPlacer] ❌ Or insuffisant");
            CancelAllModes();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector2Int gridPos = grid.GetGridPosition(hit.point);

            if (grid.CanPlace(gridPos))
            {
                wallet.GoldCount -= currentCost;
                Vector3 spawnPos = grid.GetWorldPosition(gridPos.x, gridPos.y);
                
                // Calcul hauteur
                float h = currentPrefab.GetComponent<Renderer>().bounds.size.y;
                spawnPos.y = h / 2f;

                // On instancie
                GameObject newBuilding = Instantiate(currentPrefab, spawnPos, Quaternion.identity);
                
                // OPTIONNEL : On peut stocker le coût dans un script sur le bâtiment pour le remboursement
                // newBuilding.AddComponent<BuildingData>().cost = currentCost;

                grid.Occupy(gridPos);
                CancelAllModes();
            }
        }
    }

    void TryDestroyBuilding()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit hit))
    {
        // On vérifie si l'objet a le script BuildingData
        BuildingData data = hit.collider.GetComponent<BuildingData>();

        if (data != null)
        {
            // 1. Calcul du remboursement basé sur le prix réel du bâtiment
            int refund = Mathf.FloorToInt(data.price / 2f);
            Gold.GetComponent<GoldCounter>().GoldCount += refund;

            // 2. Libérer la grille
            Vector2Int gridPos = grid.GetGridPosition(hit.point);
            grid.Free(gridPos);

            // 3. Destruction
            Destroy(hit.transform.gameObject);
            CancelAllModes();
        }
    }
}
}