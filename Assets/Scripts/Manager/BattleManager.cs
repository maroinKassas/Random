using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static readonly Queue<TacticsBattle> turnQueue = new Queue<TacticsBattle>();

    public static readonly List<Vector3> BattlePositionsMonster = new List<Vector3>
    {
        new Vector3(6, 0.75f, 6),
        new Vector3(6, 0.75f, 5),
        new Vector3(6, 0.75f, 4),
        new Vector3(6, 0.75f, 3),
        new Vector3(6, 0.75f, 2),
        new Vector3(6, 0.75f, 1),
        new Vector3(6, 0.75f, 0),
        new Vector3(5, 0.75f, 6),
        new Vector3(5, 0.75f, 5),
        new Vector3(5, 0.75f, 4),
        new Vector3(5, 0.75f, 3),
        new Vector3(5, 0.75f, 2),
        new Vector3(5, 0.75f, 1),
        new Vector3(5, 0.75f, 0)
    };

    public static readonly List<Vector3> BattlePositionsPlayer = new List<Vector3>
    {
        new Vector3(0, 0.75f, 6),
        new Vector3(0, 0.75f, 5),
        new Vector3(0, 0.75f, 4),
        new Vector3(0, 0.75f, 3),
        new Vector3(0, 0.75f, 2),
        new Vector3(0, 0.75f, 1),
        new Vector3(0, 0.75f, 0),
        new Vector3(1, 0.75f, 6),
        new Vector3(1, 0.75f, 5),
        new Vector3(1, 0.75f, 4),
        new Vector3(1, 0.75f, 3),
        new Vector3(1, 0.75f, 2),
        new Vector3(1, 0.75f, 1),
        new Vector3(1, 0.75f, 0)
    };

    public static void Init()
    {
        foreach (TacticsBattle tacticsBattle in turnQueue)
        {
            if (tacticsBattle.CompareTag("Monster"))
            {
                tacticsBattle.transform.position = RandomPostion(BattlePositionsMonster);
            }

            if (tacticsBattle.CompareTag("Player"))
            {
                tacticsBattle.transform.position = RandomPostion(BattlePositionsPlayer);
            }
        }
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

    private static Vector3 RandomPostion(List<Vector3> battlePostion)
    {
        int randomIndex = Random.Range(0, battlePostion.Count);
        Vector3 randomPosition = battlePostion[randomIndex];
        battlePostion.RemoveAt(randomIndex);

        return randomPosition;
    }
}
