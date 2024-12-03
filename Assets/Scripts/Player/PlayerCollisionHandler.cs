using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    private float cooldownTimer;
    private const float collisionCooldown = 1f;
    private const string hitParam = "Hit";

    private void OnCollisionEnter(Collision collision)
    {
        if(Time.time > cooldownTimer + collisionCooldown)
        {
            animator.SetTrigger(hitParam);
            cooldownTimer = Time.time;
        }
    }
}
