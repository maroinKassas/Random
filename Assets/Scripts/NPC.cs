using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NPC : Character
{
    private GameObject enemy;
    private Tile tileEnemy;
    private Tile tileNearest;

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
            FindNearestEnemy();
            tileEnemy = tacticsMove.GetTargetTile(enemy);
            tileEnemy.FindNeighbors(tacticsMove.heightMax);
            tacticsMove.FindSelectableTiles(Constante.DISTANCE_COMBAT_MAX);
            CalculatePath();
            tacticsMove.MoveToTile(tileNearest);

            tacticsBattle.EndsHisTurn();
        }
        else
        {
            tacticsMove.Move();
        }

        tacticsBattle.SetStatsText();
    }

    protected override void ExplorationUpdate()
    {
        // TODO
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearestEnemy = null;
        float distanceNearest = Constante.DISTANCE_COMBAT_MAX;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < distanceNearest)
            {
                nearestEnemy = enemy;
                distanceNearest = distance;
            }
        }

        this.enemy = nearestEnemy;
    }

    private void CalculatePath()
    {
        if (tileEnemy.adjacencyList.Count == 0)
        {
            return;
        }

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
        tacticsMove.FindSelectableTiles(tacticsBattle.movementPoint);

        if (tacticsMove.path != null && tacticsMove.path.Count > 0)
        {
            tileNearest = tacticsMove.path.OrderByDescending(tile => tile.distance).FirstOrDefault();
        }
    }
}
