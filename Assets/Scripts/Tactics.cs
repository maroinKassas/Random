using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tactics : MonoBehaviour
{
    private static readonly List<Tile> battleMap = new List<Tile>();
    public List<Tile> selectableTiles = new List<Tile>();
    public Tile currentTile;

    public void InitBattleMap()
    {
        GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tileObject in tileObjects)
        {
            if (tileObject.TryGetComponent<Tile>(out var tile))
            {
                battleMap.Add(tile);
            }
        }
    }

    public Tile GetTargetTile(GameObject target)
    {
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out RaycastHit raycastHit, 1))
        {
            tile = raycastHit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    protected void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        if (currentTile != null)
        {
            currentTile.current = true;
            currentTile.distance = 0;
        }
    }

    protected void ComputeAdjacencyLists()
    {
        foreach (Tile tile in battleMap)
        {
            tile.FindNeighbors();
        }
    }

    public void FindSelectableTiles(bool selectable, float distancePoint)
    {
        selectableTiles.Clear();

        ComputeAdjacencyLists();
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        
        while (process.Count > 0)
        {
            Tile tileProcess = process.Dequeue();

            selectableTiles.Add(tileProcess);
            tileProcess.selectable = selectable;

            if (tileProcess.distance < distancePoint)
            {
                foreach (Tile tile in tileProcess.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = tileProcess;
                        tile.visited = true;
                        tile.distance = 1 + tileProcess.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }
}
