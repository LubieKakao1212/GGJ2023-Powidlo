using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 up;

    private void Start()
    {
        up = transform.up;
    }

    void Update()
    {
        transform.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime, up);
    }
}
