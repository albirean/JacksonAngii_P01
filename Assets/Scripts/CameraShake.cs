using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float shakeDuration = 0f;

    [SerializeField] float shakeAmount = 0.7f;
    [SerializeField] float decreaseFactor = 1.0f;

    Vector3 originalPos;

    public bool shaketrue = false;

    private void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void OnEnable()
    {
        originalPos = cameraTransform.localPosition;
    }

    void Update()
    {

        if (shaketrue)
        {
            if (shakeDuration > 0)
            {
                cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }

            else
            {
                shakeDuration = 1f;
                cameraTransform.localPosition = originalPos;
                shaketrue = false;
            }
        }
        
    }

    public void shakeCamera()
    {
        shaketrue = true;
    }
}
