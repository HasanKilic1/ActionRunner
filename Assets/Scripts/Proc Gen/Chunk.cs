using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = .3f;
    [SerializeField] float coinSpawnChance = .5f;
    [SerializeField] float coinSeparationLength = 2f;
    [SerializeField] float[] lanes = { -2.5f, 0f, 3.7f };
    List<int> availableLaneIndexes = new List<int> { 0, 1, 2 };
    
    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    public void Init(LevelGenerator levelGenerator , ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLaneIndexes.Count <= 0) break;
            int selectedLaneIndex = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLaneIndex], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, transform);
        }
    }

    void SpawnApple()
    {
        if(availableLaneIndexes.Count <= 0) return;
        if(Random.value > appleSpawnChance) return;

        int selectedLaneIndex = SelectLane();
        Vector3 spawnPosition = new Vector3(lanes[selectedLaneIndex], transform.position.y, transform.position.z);
        Apple apple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Apple>();
        apple.Init(levelGenerator);
    }

    void SpawnCoins()
    {
        if (availableLaneIndexes.Count <= 0) return;
        if (Random.value > coinSpawnChance) return;

        int coinCount = Random.Range(1, 6);
        int selectedLaneIndex = SelectLane();

        float topOfChunkZPos = transform.position.z + (coinSeparationLength * 2f) ;

        for (int i = 0; i < coinCount; i++)
        {
            float spawnPosZ = topOfChunkZPos - i * coinSeparationLength;
            Vector3 spawnPosition = new Vector3(lanes[selectedLaneIndex], transform.position.y, spawnPosZ);
            Coin coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Coin>();
            coin.Init(scoreManager);
        }
    }

    private int SelectLane()
    {
        int randomFenceIndex = Random.Range(0, availableLaneIndexes.Count);
        int selectedLaneIndex = availableLaneIndexes[randomFenceIndex];
        availableLaneIndexes.RemoveAt(randomFenceIndex);
        return selectedLaneIndex;
    }
}
