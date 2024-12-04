using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticle;
    [SerializeField] AudioSource audioSource;

    [SerializeField] float shakeModifier = 10f;
    private CinemachineImpulseSource impulseSource;
    private float impulseCooldown = 1f;
    private float impulseTimer;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > impulseTimer)
        {
            FireCameraImpulse();
            CollisionFX(collision);
        }
    }

    private void FireCameraImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1);
        impulseSource.GenerateImpulse(shakeIntensity);
        impulseTimer = Time.time + impulseCooldown;
    }

    private void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticle.transform.position = contactPoint.point;
        collisionParticle.Play();
        audioSource.Play();
    }
}
