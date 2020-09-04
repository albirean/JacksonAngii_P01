using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardOverTime : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 0.3f;
    private void Update()
    {
        this.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }
}
