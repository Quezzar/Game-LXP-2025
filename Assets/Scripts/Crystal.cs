using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float health = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            print("Crystal hit by Enemy");
            Destroy(collision.collider.gameObject);
            health -= 10f; 
        }
        
    }
}
