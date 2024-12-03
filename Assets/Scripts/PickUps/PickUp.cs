using UnityEngine;

public class PickUp : MonoBehaviour
{
    const string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {

        }
    }
}
