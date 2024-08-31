using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NPC : Character
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
            GameObject enemy = FindNearestEnemy();
            MoveToEnemy(enemy);
            tacticsBattle.EndsHisTurn();
        }
        else
        {
            tacticsMove.Move();
        }
    }

    protected override void ExplorationUpdate()
    {
        // TODO
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearestEnemy = null;
        float distanceNearest = Constante.DISTANCE_COMBAT_MAX;

        foreach (GameObject enemy in enemies)
        {
            float distance = GetDistanceEnemy(enemy);

            if (distance < distanceNearest)
            {
                nearestEnemy = enemy;
                distanceNearest = distance;
            }
        }

        return nearestEnemy;
    }

    private void MoveToEnemy(GameObject enemy)
    {
        if (GetDistanceEnemy(enemy) > 1)
        {
            Tile tileEnemy = tacticsMove.GetTargetTile(enemy);
            tileEnemy.FindNeighbors();
            tacticsMove.FindSelectableTiles(false, Constante.DISTANCE_COMBAT_MAX);
            Tile tileNearest = GetTileNearest(tileEnemy);
            tacticsMove.MoveToTile(tileNearest);
        }
    }

    private Tile GetTileNearest(Tile tileEnemy)
    {
        if (tileEnemy.adjacencyList.Count == 0)
        {
            return null;
        }

        Tile tileNearest = null;
        Tile tileAdjacency = null;

        float shortestDistance = Constante.DISTANCE_COMBAT_MAX;

        foreach (Tile tile in tileEnemy.adjacencyList)
        {
            if (tile.distance > 0 && tile.distance < shortestDistance)
            {
                tileAdjacency = tile;
                shortestDistance = tile.distance;
            }
        }

        tacticsMove.PathMove(tileAdjacency);
        tacticsMove.FindSelectableTiles(false, tacticsBattle.movementPoint);

        if (tacticsMove.path != null && tacticsMove.path.Count > 0)
        {
            tileNearest = tacticsMove.path.OrderByDescending(tile => tile.distance).FirstOrDefault();
        }

        return tileNearest;
    }

    private float GetDistanceEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }

    private void OnMouseOver()
    {
        transform.Find("CanvasMonster").gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        transform.Find("CanvasMonster").gameObject.SetActive(false);
    }
}
