using UnityEngine;

public class Munition : MonoBehaviour
{

        public float lifeTime = 3f;
        

     void Start()
    {
        // Auto-destruction après 3 secondes
        Destroy(gameObject, lifeTime);
        Debug.Log("[Munition] Créée, auto-destruction dans " + lifeTime + "s");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // détruit l'Enemy
            Destroy(gameObject); // détruit la Munition
        }
    }
}
