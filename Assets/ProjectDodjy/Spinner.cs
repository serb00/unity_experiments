using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float rotationSpeed = 10;
    public bool rotateX = false;
    private float xRotation = 0;
    public bool rotateY = false;
    private float yRotation = 0;
    public bool rotateZ = false;
    private float zRotation = 0;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    /// <summary>
    /// Rotates the object around the selected axes at a given speed.
    /// </summary>
    private void Rotate()
    {
        if (rotateX) xRotation = rotationSpeed * Time.deltaTime; else xRotation = 0;
        if (rotateY) yRotation = rotationSpeed * Time.deltaTime; else yRotation = 0;
        if (rotateZ) zRotation = rotationSpeed * Time.deltaTime; else zRotation = 0;
        
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
