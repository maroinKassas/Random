using UnityEngine;

public class Player : MonoBehaviour
{
    private Tactics tactics;
    private TacticsMove tacticsMove;

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

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.collider.CompareTag("Tile"))
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
