using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyInstance;
    public GameObject[] enemies;
    public GameObject spawner;
    
    private float minSpawnDelay = 1f;
    private float maxSpawnDelay = 2f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private System.Collections.IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));

            enemyInstance = UnityEngine.Object.Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], 
            spawner.transform.position, 
            spawner.transform.rotation);
        }
    }
}
