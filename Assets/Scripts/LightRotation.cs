using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Rotates the light source in the scene
/// </summary>
public class LightRotation : MonoBehaviour
{
    public float rotationSpeed = 1;
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
