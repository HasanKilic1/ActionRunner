using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] float increaseTime = 5f;
    private const string PlayerTag = "Player";

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == PlayerTag)
        {
            gameManager.IncreaseTimeLeft(increaseTime);
        }
    }
}
