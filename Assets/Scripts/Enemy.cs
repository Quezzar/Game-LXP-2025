using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Crystal;
    public GameObject Gold;
    public int maxHealth = 3;
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Crystal = GameObject.FindWithTag("Crystal");
        Gold = GameObject.FindWithTag("Gold");
        agent.SetDestination(Crystal.transform.position);
        currentHealth = maxHealth;
        //Debug.Log("[Enemy] PV initiaux = " + currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Munition"))
        {
            currentHealth--;

            //Debug.Log("[Enemy] Touché ! PV restants = " + currentHealth);

            if (currentHealth <= 0)
            {
                Debug.Log("[Enemy] Détruit");
                Destroy(this.gameObject);
                Gold.GetComponent<GoldCounter>().GoldCount += 2;
            }
        }
    }
}
