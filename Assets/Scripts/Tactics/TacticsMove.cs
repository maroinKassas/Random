using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : Tactics
{
    private bool intoBattle = false;
    public TacticsBattle tacticsBattle;

    public Stack<Tile> path = new Stack<Tile>();

    public float moveSpeed = 5f;

    public Vector3 velocity = Vector3.zero;
    public Vector3 heading = Vector3.zero;

    public bool fallingDown = false;
    public bool jumpingUp = false;
    public bool movingEdge = false;

    public Vector3 jumpTarget;

    private bool displayPath = false;

    public void MoveToTile(Tile tile)
    {
        if (intoBattle)
        {
            tacticsBattle.isMoving = true;
        }

        PathMove(tile);
    }

    public void Move()
    {
        if (path.Count <= 0)
        {
            RemoveSelectableTiles();
            if (intoBattle)
            {
                tacticsBattle.isMoving = false;
            }
            return;
        }

        Tile tile = path.Peek();

        if (tile == null)
        {
            return;
        }

        Vector3 target = tile.transform.position;
        target.y = transform.position.y;

        if (Vector3.Distance(transform.position, target) >= 0.01f * moveSpeed)
        {
            CalculateHeading(target);
            SetHorizontalVelocity();

            transform.forward = heading;
            transform.position += velocity * Time.deltaTime;
            LockRotationXAndZ();
        }
        else
        {
            transform.position = target;
            path.Pop();

            if (!tile.current && intoBattle)
            {
                tacticsBattle.movementPoint--;
            }
        }
    }

    public void PathMove(Tile tile)
    {
        path.Clear();

        Tile next = tile;
        while (next != null)
        {
            next.hover = displayPath;
            path.Push(next);
            next = next.parent;
        }
    }

    public void SetDisplayPath(bool displayPath)
    {
        this.displayPath = displayPath;
    }

    private void LockRotationXAndZ()
    {
        Quaternion rotation = transform.rotation;
        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }

    private void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        if (heading.magnitude > 0.01f)
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

    public void SetTacticsBattle(bool intoBattle, TacticsBattle tacticsBattle)
    {
        this.intoBattle = intoBattle;
        this.tacticsBattle = tacticsBattle;
    }
}
