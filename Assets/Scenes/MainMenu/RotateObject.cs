using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool randomizeRotation = false;
    [SerializeField] private Vector2 randomRotationRange;
    [SerializeField] private bool randomizeScale = false;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float scale = 1;

    private void Start()
    {
        if(randomizeRotation)
        {
            rotation.x = UnityEngine.Random.Range(randomRotationRange.x, randomRotationRange.y);
            rotation.y = UnityEngine.Random.Range(randomRotationRange.x, randomRotationRange.y);
            rotation.z = UnityEngine.Random.Range(randomRotationRange.x, randomRotationRange.y);
        }
        if(randomizeScale)
        {
            scale = UnityEngine.Random.Range(0.75f, 1.2f);
            transform.localScale += new Vector3(scale, scale, scale);
        }
    }
    private void Update()
    {
        transform.Rotate(rotation.x * Time.deltaTime, rotation.y * Time.deltaTime, rotation.z * Time.deltaTime);
    }
}
