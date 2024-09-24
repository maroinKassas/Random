using UnityEngine;

public class Node
{
    public Vector2Int cords;
    public bool walkable;
    public bool selectable;
    public bool blocked;
    public bool path;
    public Node connectTo;

    public Node(Vector2Int cords)
    {
        this.cords = cords;
        this.walkable = false;
        this.selectable = false;
        this.blocked = false;
        this.path = false;
        this.connectTo = null;
    }
}
