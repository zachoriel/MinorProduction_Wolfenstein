using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform mainCamera;

    public float shakeDuration = 0f;

    public float shakeMagnitude = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = mainCamera.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            mainCamera.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            mainCamera.localPosition = originalPos;
        }
    }
}