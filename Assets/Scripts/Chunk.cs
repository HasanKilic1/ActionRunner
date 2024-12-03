using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] float[] lanes = { -2.5f, 0f, 3.7f };
    private void Start()
    {
        SpawnFence();
    }

    void SpawnFence()
    {
        float x = lanes[Random.Range(0, lanes.Length)];  
        Vector3 spawnPosition = new Vector3(x, transform.position.y, transform.position.z);
        Instantiate(fencePrefab , spawnPosition , Quaternion.identity , transform);
    }
}
