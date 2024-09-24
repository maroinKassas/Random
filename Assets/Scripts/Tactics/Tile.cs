using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool current = false;
    public bool selectable = false;
    public bool hover = false;

    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    public List<Tile> adjacencyList = new List<Tile>();

    void Update()
    {
        if (hover)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        adjacencyList.Clear();

        current = false;
        selectable = false;
        hover = false;

        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbors()
    {
        Reset();
        CheckTile(Vector3.forward);
        CheckTile(-Vector3.forward);
        CheckTile(Vector3.right);
        CheckTile(-Vector3.right);
    }

    public void CheckTile(Vector3 direction)
    {
        Vector3 halfExtents = new Vector3(0.25f, 1.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            if (item.TryGetComponent<Tile>(out var tile))
            {
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out _, 1))
                {
                    adjacencyList.Add(tile);
                }
            }
        }
    }
}
