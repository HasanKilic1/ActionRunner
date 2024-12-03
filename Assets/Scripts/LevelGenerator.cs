using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] int startingChunksAmount = 10;
    [SerializeField] float scrollSpeed = 8f;
    [SerializeField] Transform player;

    List<GameObject> chunks = new List<GameObject>();
    private void Start()
    {
        SpawnStartingChunks();
    }

    private void Update()
    {
        MoveChunks();            
    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = GetSpawnPosZ();
        Vector3 chunkPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject newChunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }

    private float GetSpawnPosZ()
    {
        float spawnPositionZ;
        if (chunks.Count == 0) spawnPositionZ = transform.position.z;
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }
        return spawnPositionZ;
    }

    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.position += scrollSpeed * Time.deltaTime * Vector3.back;

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
