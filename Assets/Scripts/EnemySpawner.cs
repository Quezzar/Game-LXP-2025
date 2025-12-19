using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Crystal;
    public GameObject EnemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 direction = Random.onUnitSphere;
            direction.y = 0;
            Vector3 offset = direction.normalized * 100f;
            Vector3 spawnPosition = Crystal.transform.position + offset;
            Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(2f);
        }
    }
}
