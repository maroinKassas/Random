using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            float currentSpeed = transform.position.y > 0 ? Constante.SUN_ROTATION_SPEED : Constante.SUN_ROTATION_SPEED * 5;

            transform.RotateAround(target.position, Vector3.forward + Vector3.right, currentSpeed * Time.deltaTime);
        }
    }
}
