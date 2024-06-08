using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static List<Tile> battleMap = new List<Tile>();
    private static readonly Queue<TacticsBattle> turnQueue = new Queue<TacticsBattle>();

    public static void Init()
    {
        SetBattleMap();
        StartTurn();
    }

    private static void StartTurn()
    {
        TacticsBattle tacticsBattle = turnQueue.Peek();
        tacticsBattle.BeginsHisTurn();
    }

    public static void EndTurn()
    {
        TacticsBattle tacticsBattle = turnQueue.Peek();
        tacticsBattle.EndsHisTurn();
    }

    public static void NextTurn()
    {
        TacticsBattle tacticsBattle = turnQueue.Dequeue();
        turnQueue.Enqueue(tacticsBattle);
        StartTurn();
    }

    public static void AddUnit(TacticsBattle tacticsBattle)
    {
        turnQueue.Enqueue(tacticsBattle);
    }

    private static void SetBattleMap()
    {
        GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tileObject in tileObjects)
        {
            if (tileObject.TryGetComponent<Tile>(out var tile))
            {
                battleMap.Add(tile);
            }
        }
    }

    public static List<Tile> GetBattleMap()
    {
        return battleMap;
    }
}
