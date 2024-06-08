using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : Character
{
    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
    }

    protected override void BattleUpdate()
    {
        if (!tacticsBattle.turn)
        {
            return;
        }

        if (!tacticsBattle.isMoving)
        {
            tacticsMove.FindSelectableTiles(tacticsBattle.movementPoint);
            HandleMouseOver();
        }
        else
        {
            tacticsMove.Move();
        }

        tacticsBattle.SetStatsText();
    }

    protected override void ExplorationUpdate()
    {
        Tile tile = PickTile();
        if (tile != null)
        {
            tacticsMove.SetDisplayPath(false);
            HandleClick(tile);
        }
        tacticsMove.Move();
    }

    private void HandleMouseOver()
    {
        Tile tile = PickTile();
        if (tile == null || !tile.selectable)
        {
            return;
        }

        tacticsMove.SetDisplayPath(true);
        tacticsMove.PathMove(tile);

        if (Input.GetMouseButtonUp(0))
        {
            tacticsMove.MoveToTile(tile);
        }
    }

    private void HandleClick(Tile tile)
    {
        if (Input.GetMouseButton(0))
        {
            tacticsMove.MoveToTile(tile);
        }
    }

    private Tile PickTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.collider.CompareTag("Tile"))
            {
                Tile tile = raycastHit.collider.GetComponent<Tile>();
                return tile;
            }
        }
        return null;
    }
}
