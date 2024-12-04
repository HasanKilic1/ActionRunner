using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpParticle;
    [SerializeField] float zoomDuration = 0.3f;
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] float minFOV, maxFOV;
    [SerializeField] float zoomSpeedModifier = 5f;
    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();    
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(LerpCameraFOV(speedAmount));

        if(speedAmount > 0f) speedUpParticle.Play();
    }

    IEnumerator LerpCameraFOV(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = startFOV + speedAmount * zoomSpeedModifier;
        targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / zoomDuration);

            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
