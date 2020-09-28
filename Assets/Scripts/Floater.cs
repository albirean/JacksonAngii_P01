using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 15.0f;
    [SerializeField] float amplitude = 0.5f;
    [SerializeField] float frequency = 1f;

    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField] Vector3 tempPosition = new Vector3(0, 0, 0);
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * rotationSpeed, 0f), Space.World);
        tempPosition = offset;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPosition;
    }
}
