using System.Collections;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{
    private static float zoomSpeed = 2.0f;
    private static float minZoom = 3.0f;
    private static float maxZoom = 5.0f;

    private static Vector3 repositionPosition = new Vector3(-3.0f, 5.0f, -3.0f);
    private static Quaternion repositionRotation = Quaternion.Euler(30.0f, 45.0f, 0.0f);

    private static float moveDuration = 0.75f;

    public Transform target;
    public float distance = 10.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20.0f;
    public float yMaxLimit = 80.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    private bool isMovingCamera = false;

    void Update()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData != 0.0f)
        {
            Camera.main.orthographicSize -= scrollData * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            if (isMovingCamera)
            {
                StopCoroutine(MoveCameraToPosition());
                isMovingCamera = false;
            }

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.SetPositionAndRotation(position, rotation);
        }
        else if (!isMovingCamera)
        {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;

            StartCoroutine(MoveCameraToPosition());
        }
    }

    private IEnumerator MoveCameraToPosition()
    {
        isMovingCamera = true;
        float elapsedTime = 0.0f;
        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < moveDuration)
        {
            float timeDuration = elapsedTime / moveDuration;
            transform.SetPositionAndRotation(
                Vector3.Lerp(startingPosition, repositionPosition, timeDuration),
                Quaternion.Lerp(startingRotation, repositionRotation, timeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.SetPositionAndRotation(repositionPosition, repositionRotation);
        isMovingCamera = false;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
