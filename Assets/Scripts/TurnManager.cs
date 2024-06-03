using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static readonly Dictionary<string, List<Tactics>> units = new Dictionary<string, List<Tactics>>();
    private static readonly Queue<string> turnKey = new Queue<string>();
    private static readonly Queue<Tactics> turnTeam = new Queue<Tactics>();

    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    private static void InitTeamTurnQueue()
    {
        List<Tactics> tacticsList = units[turnKey.Peek()];

        foreach (Tactics tactics in tacticsList)
        {
            turnTeam.Enqueue(tactics);
        }

        StartTurn();
    }

    private static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        Tactics tactics = turnTeam.Dequeue();
        tactics.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string tacticsName = turnKey.Dequeue();
            turnKey.Enqueue(tacticsName);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(Tactics tactics)
    {
        List<Tactics> tacticsList;

        if (!units.ContainsKey(tactics.tag))
        {
            tacticsList = new List<Tactics>();
            units[tactics.tag] = tacticsList;

            if (!turnKey.Contains(tactics.tag))
            {
                turnKey.Enqueue(tactics.tag);
            }
        }
        else
        {
            tacticsList = units[tactics.tag];
        }

        tacticsList.Add(tactics);
    }
}
