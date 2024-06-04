using UnityEngine;

public class Player : MonoBehaviour
{
    private TacticsMove tacticsMove;
    private Tactics tactics;

    void Start()
    {
        tactics = GetComponent<Tactics>();
        tacticsMove = GetComponent<TacticsMove>();

        tactics.Init();
        tacticsMove.Init();
    }

    void Update()
    {
        if (!tactics.turn)
        {
            return;
        }

        if (!tactics.isMoving)
        {
            tacticsMove.FindSelectableTiles();
            OnMouseOver();
        }
        else
        {
            tacticsMove.Move();
        }

        tactics.SetStatsText();
    }

    public void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.tag == "Tile")
            {
                Tile tile = raycastHit.collider.GetComponent<Tile>();

                if (tile.selectable)
                {
                    tacticsMove.DisplayPath(tile);
                    if (Input.GetMouseButtonUp(0))
                    {
                        tacticsMove.MoveToTile(tile);
                    }
                }
            }
        }
    }
}
