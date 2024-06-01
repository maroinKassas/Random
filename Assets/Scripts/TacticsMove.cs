using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    public List<Tile> selectableTiles = new List<Tile>();
    public List<Tile> tiles = new List<Tile>(); // Liste de toutes les tuiles au début du jeu

    public Stack<Tile> path = new Stack<Tile>();
    public Tile currentTile;

    public bool moving = false;
    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 10;

    public Vector3 velocity = new Vector3();
    public Vector3 heading = new Vector3();

    public float halfHeight = 0;

    protected void Init()
    {
        // Récupère toutes les tuiles au début du jeu
        GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tileObject in tileObjects)
        {
            tiles.Add(tileObject.GetComponent<Tile>());
        }

        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit raycastHit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out raycastHit, 1))
        {
            tile = raycastHit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists()
    {
        foreach (Tile tile in tiles)
        {
            tile.FindNeighbors(jumpHeight);
        }
    }

    public void FindSelectableTiles()
    {
        selectableTiles.Clear(); // Réinitialise la liste des tuiles sélectionnables

        ComputeAdjacencyLists();
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = null;

        while (process.Count > 0)
        {
            Tile tileProcess = process.Dequeue();

            selectableTiles.Add(tileProcess);
            tileProcess.selectable = true;

            if (tileProcess.distance < move)
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

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move()
    {
        if (path.Count > 0)
        {
            Tile tile = path.Peek();
            Vector3 target = tile.transform.position;

            // Calculate the unit's position on top of the target tile
            target.y += halfHeight + tile.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                // Tile center reached
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
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

    public void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    public void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }
}
