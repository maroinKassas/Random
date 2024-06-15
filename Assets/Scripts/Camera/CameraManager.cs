using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private readonly float ZoomSpeed = 5.0f;
    private readonly float MinZoom = 5.0f;
    private readonly float MaxZoom = 7.0f;

    protected virtual void Update()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData != 0.0f)
        {
            Camera.main.orthographicSize -= scrollData * ZoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, MinZoom, MaxZoom);
        }
    }
}
