using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public int x = 10;

    void Update()
    {
        float currentSpeed = transform.position.y > 0 ? Constante.SUN_ROTATION_SPEED * x : Constante.SUN_ROTATION_SPEED * 5 * x;
        transform.RotateAround(Vector3.zero, Vector3.forward + Vector3.right, currentSpeed * Time.deltaTime);
    }
}
