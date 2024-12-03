using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obstacleSpawnPeriod;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;
    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(obstacleSpawnPeriod);
            GameObject randomObstacle = obstaclePrefabs[Random.Range(0 , obstaclePrefabs.Length)];
            Vector3 spawnPosition = transform.position;
            spawnPosition.x = Random.Range(-spawnWidth, spawnWidth);
            Instantiate(randomObstacle, spawnPosition, Random.rotation , obstacleParent);
        }
    }
}
