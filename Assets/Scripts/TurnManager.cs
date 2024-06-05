using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static readonly Queue<Tactics> turnQueue = new Queue<Tactics>();

    void Start()
    {
        StartTurn();
    }

    private static void StartTurn()
    {
        Tactics tactics = turnQueue.Peek();
        tactics.BeginTurn();
    }

    public static void EndTurn()
    {
        Tactics tactics = turnQueue.Peek();
        tactics.EndTurn();
    }

    public static void NextTurn()
    {
        Tactics tactics = turnQueue.Dequeue();
        turnQueue.Enqueue(tactics);
        StartTurn();
    }

    public static void AddUnit(Tactics tactics)
    {
        turnQueue.Enqueue(tactics);
    }
}
