using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : Character
{
    private Monster monster;

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
            tacticsMove.FindSelectableTiles(true, tacticsBattle.movementPoint);
            HandleMouseOver();
        }
        else
        {
            tacticsMove.Move();
        }
    }

    protected override void ExplorationUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            monster = null;
            Tile tile = PickTile();
            if (tile != null)
            {
                tacticsMove.SetDisplayPath(false);
                tacticsMove.MoveToTile(tile);
            }
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

            if (raycastHit.collider.CompareTag("Monster") && !intoBattle)
            {
                monster = raycastHit.collider.GetComponent<Monster>();
                Tile tile = tacticsMove.GetTargetTile(monster.gameObject);
                return tile;
            }
        }
        return null;
    }

    public IEnumerator OnTriggerEnter(Collider monsterCollider)
    {
        if (monster != null && monster.GetComponent<Collider>().Equals(monsterCollider))
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(monsterCollider.gameObject);

            AsyncOperation asyncLoad = SceneManagerScript.LoadScene(Constante.BATTLE_SCENE);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            this.GoIntoBattle();
            monsterCollider.GetComponent<Monster>().GoIntoBattle();

            BattleManager.Init();
        }
    }
}
