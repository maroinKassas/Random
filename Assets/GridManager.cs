using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] 
    private Vector2Int gridSize;
    private readonly Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

     private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        grid.TryGetValue(coordinates, out Node node);
        return node;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.TryGetValue(coordinates, out Node node))
        {
            node.walkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int
        {
            x = Mathf.RoundToInt(position.x / Constante.UNITY_GRID_SIZE),
            y = Mathf.RoundToInt(position.z / Constante.UNITY_GRID_SIZE)
        };

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3
        {
            x = coordinates.x * Constante.UNITY_GRID_SIZE,
            z = coordinates.y * Constante.UNITY_GRID_SIZE
        };

        return position;
    }
    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(x, y);
                grid.Add(cords, new Node(cords));

                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //Vector3 position = new Vector3(cords.x * unitGridSize, 0f, cords.y * unitGridSize);
                //cube.transform.position = position;
                //cube.transform.SetParent(transform);
            }
        }
    }
}