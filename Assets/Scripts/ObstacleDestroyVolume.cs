using UnityEngine;

public class ObstacleDestroyVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
