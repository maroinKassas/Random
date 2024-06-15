using UnityEngine;

public class CameraExploration : CameraManager
{
    public Transform player;

    public float followSpeed = 5.0f;
    public Vector3 offset = new Vector3(-20f, 17f, -20f);

    private new void Update()
    {
        base.Update();
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(currentPosition, targetPosition, followSpeed * Time.deltaTime);
    }
}
