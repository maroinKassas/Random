using UnityEngine;

public class Utils
{
    public static Vector2Int GetCurrentCoordinates(GameObject gameObject)
    {
        return new Vector2Int((int) gameObject.transform.position.x, (int) gameObject.transform.position.z) / Constante.UNITY_GRID_SIZE;
    }

    public static Tile GetTargetTile(GameObject target)
    {
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out RaycastHit raycastHit, 1))
        {
            tile = raycastHit.collider.GetComponent<Tile>();
        }

        return tile;
    }
}
