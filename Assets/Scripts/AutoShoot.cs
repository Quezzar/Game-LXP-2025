using UnityEngine;

public class BatimentShooter_NoFirePoint : MonoBehaviour
{
    [Header("Détection")]
    public float detectionRadius = 10f;

    [Header("Tir")]
    public GameObject munitionPrefab;
    public float fireRate = 1f;
    public float munitionSpeed = 1000f;

    private float fireTimer;
    private Transform currentTarget;

    void Start()
    {
        Debug.Log("[Batiment] Script sans FirePoint démarré");
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        DetectEnemy();

        Debug.Log("[Batiment] Target = " + (currentTarget != null ? currentTarget.name : "AUCUNE"));

        if (currentTarget != null && fireTimer >= 1f / fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    void DetectEnemy()
    {
        currentTarget = null;

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider col in hits)
        {
            Debug.Log("[Batiment] Detecté : " + col.name + " | Tag = " + col.tag);

            if (col.CompareTag("Enemy"))
            {
                currentTarget = col.transform;
                break;
            }
        }
    }

    void Fire()
    {
        if (munitionPrefab == null || currentTarget == null)
        {
            Debug.LogError("[Batiment] Tir annulé");
            return;
        }

        Vector3 spawnPos = transform.position + Vector3.up * 1f;
        GameObject munition = Instantiate(munitionPrefab, spawnPos, Quaternion.identity);

        Debug.Log("[Batiment] Munition créée");

        Rigidbody rb = munition.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("[Batiment] Munition sans Rigidbody");
            return;
        }

        Vector3 direction = (currentTarget.position - spawnPos).normalized;
        rb.linearVelocity = direction * munitionSpeed;

        Debug.Log("[Batiment] Tir vers : " + currentTarget.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
