using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeedChangeOnHit = -2f;
    private float cooldownTimer;
    private const float collisionCooldown = 1f;
    private const string hitParam = "Hit";

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Time.time > cooldownTimer + collisionCooldown)
        {
            levelGenerator.ChangeScrollSpeed(moveSpeedChangeOnHit);
            animator.SetTrigger(hitParam);
            cooldownTimer = Time.time;
        }
    }
}
