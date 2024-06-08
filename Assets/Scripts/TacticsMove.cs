using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : Tactics
{
    // Fields
    private bool intoBattle = false;
    public TacticsBattle tacticsBattle;

    public Stack<Tile> path = new Stack<Tile>();

    public float moveSpeed = 5f;
    public float jumpVelocity = 5.0f;

    public Vector3 velocity = Vector3.zero;
    public Vector3 heading = Vector3.zero;

    public float halfHeight = 0;

    public bool fallingDown = false;
    public bool jumpingUp = false;
    public bool movingEdge = false;

    public Vector3 jumpTarget;

    private bool displayPath = false;

    public void InitMovement(bool intoBattle, TacticsBattle tacticsBattle)
    {
        this.intoBattle = intoBattle;
        this.tacticsBattle = tacticsBattle;

        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

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
        Vector3 target = tile.transform.position;
        target.y += halfHeight + tile.GetComponent<Collider>().bounds.extents.y;

        if (Vector3.Distance(transform.position, target) >= 0.01f * moveSpeed)
        {
            CalculateHeading(target);

            if (transform.position.y != target.y)
            {
                Jump(target);
            }
            else
            {
                SetHorizontalVelocity();
            }

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

            velocity = Vector3.zero;
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
        movingEdge = false;

        if (Vector3.Distance(transform.position, jumpTarget) >= 0.01f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }

    private void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;

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
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }
}
