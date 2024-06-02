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
    public int move = 500;
    public float jumpHeight = 2;
    public float moveSpeed = 10;

    public Vector3 velocity = new Vector3();
    public Vector3 heading = new Vector3();

    public float halfHeight = 0;

    public bool fallingDown = false;
    public bool jumpingUp = false;
    public bool movingEdge = false;
    public float jumpVelocity = 4.5f;

    public Vector3 jumpTarget;

    protected void Init()
    {
        // Récupère toutes les tuiles au début du jeu
        GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tileObject in tileObjects)
        {
            Tile tile = tileObject.GetComponent<Tile>();
            if (tile != null)
            {
                tiles.Add(tile);
            }
        }

        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    private void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        if (currentTile != null)
        {
            currentTile.current = true;
            currentTile.distance = 0; // Initialise la distance de la tuile actuelle
        }
    }

    private Tile GetTargetTile(GameObject target)
    {
        RaycastHit raycastHit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out raycastHit, 1))
        {
            tile = raycastHit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    private void ComputeAdjacencyLists()
    {
        foreach (Tile tile in tiles)
        {
            tile.FindNeighbors(jumpHeight);
        }
    }

    public void FindSelectableTiles()
    {
        // Réinitialise la liste des tuiles sélectionnables
        selectableTiles.Clear();

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

    public void DisplayPath(Tile tile)
    {
        CheckMove(tile);
    }

    public void MoveToTile(Tile tile)
    {
        moving = true;
        CheckMove(tile);
    }

    private void CheckMove(Tile tile)
    {
        path.Clear();

        Tile next = tile;
        while (next != null)
        {
            next.hover = true;
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move()
    {
        if (path.Count <= 0)
        {
            RemoveSelectableTiles();
            moving = false;
            return;
        }

        Tile tile = path.Peek();
        Vector3 target = tile.transform.position;

        // Calcule la position de l'unité au-dessus de la tuile cible
        target.y += halfHeight + tile.GetComponent<Collider>().bounds.extents.y;

        if (Vector3.Distance(transform.position, target) >= 0.01f * moveSpeed)
        {
            if (transform.position.y != target.y)
            {
                Jump(target);
            }
            else
            {
                CalculateHeading(target);
                SetHorizontalVelocity();
            }

            // Locomotion
            transform.forward = heading;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            // Centre de la tuile atteint
            transform.position = target;
            path.Pop();

            if (!tile.current)
            {
                move--;
            }
        }
    }

    private void RemoveSelectableTiles()
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

    private void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        if (heading.magnitude > 0.01f) // Vérification que heading n'est pas presque nul
        {
            heading.Normalize();
        }
        else
        {
            heading = Vector3.zero;
        }
    }

    private void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    private void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    private void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 position = transform.position;
            position.y = target.y;
            transform.position = position;

            velocity = new Vector3();
        }
    }

    private void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    private void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }

    private void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }
}
