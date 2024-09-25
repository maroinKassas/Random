using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : Character
{
    private NPC monster;
    private LayerMask tileLayerMask;
    private LayerMask monsterLayerMask;

    private new void Start()
    {
        base.Start();
        tileLayerMask = LayerMask.GetMask("Tile");
        monsterLayerMask = LayerMask.GetMask("Monster");
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

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, tileLayerMask | monsterLayerMask))
        {
            int objectLayer = raycastHit.collider.gameObject.layer;

            if ((tileLayerMask & (1 << objectLayer)) != 0)
            {
                return raycastHit.collider.GetComponent<Tile>();
            }

            if ((monsterLayerMask & (1 << objectLayer)) != 0 && !intoBattle)
            {
                monster = raycastHit.collider.GetComponent<NPC>();
                return tacticsMove.GetTargetTile(monster.gameObject);
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

            AsyncOperation asyncLoad = SceneManagerScript.LoadScene(Constante.COMBAT_SCENE);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            GoIntoBattle();
            monsterCollider.GetComponent<NPC>().GoIntoBattle();

            BattleManager.Init();
        }
    }
}
