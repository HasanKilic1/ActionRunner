using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] Transform chunkParent;
    [SerializeField] GameObject checkPointChunk;

    [Header("Level Settings")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] int startingChunksAmount = 10;
    [SerializeField] float scrollSpeed = 8f;
    [SerializeField] float minScrollSpeed = 3f;
    [SerializeField] float maxScrollSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;
    [SerializeField] Transform player;

    List<GameObject> chunks = new List<GameObject>();
    int spawnedChunkCount = 0;
    private void Start()
    {
        SpawnStartingChunks();
    }

    private void Update()
    {
        MoveChunks();            
    }
    public void ChangeScrollSpeed(float speed)
    {
        float newScrollSpeed = scrollSpeed + speed;
        scrollSpeed = Mathf.Clamp(newScrollSpeed , minScrollSpeed, maxScrollSpeed);
        float newGravityZ = Physics.gravity.z - speed;
        newGravityZ = Mathf.Clamp(newGravityZ , minGravityZ, maxGravityZ);  

        Physics.gravity = new Vector3(Physics.gravity.x , Physics.gravity.y , newGravityZ);
        cameraController.ChangeCameraFOV(speed);
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
        spawnedChunkCount++;
        
        float spawnPositionZ = GetSpawnPosZ();
        Vector3 chunkPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        
        GameObject chunkToSpawn = chunkPrefabs[Random.Range(0 , chunkPrefabs.Length)];
        if(spawnedChunkCount % 8 == 0 && spawnedChunkCount != 0)
        {
            chunkToSpawn = checkPointChunk;            
        }

        GameObject newChunk = Instantiate(chunkToSpawn, chunkPos, Quaternion.identity, chunkParent);
        newChunk.GetComponent<Chunk>().Init(this , scoreManager);
        
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
