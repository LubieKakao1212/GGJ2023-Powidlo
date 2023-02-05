using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilbord : MonoBehaviour
{
    private void Update()
    {
        var cam = Camera.main;
        transform.rotation = Quaternion.LookRotation(cam.transform.forward, cam.transform.up);
    }
}
