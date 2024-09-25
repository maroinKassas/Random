using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static readonly Queue<TacticsBattle> turnQueue = new Queue<TacticsBattle>();

    private static readonly float posY = 1.0f;

    public static readonly List<Vector3> BattlePositionsMonster = new List<Vector3>
    {
        new Vector3(9, posY, 9),
        new Vector3(9, posY, 8),
        new Vector3(9, posY, 7),
        new Vector3(9, posY, 6),
        new Vector3(9, posY, 5),
        new Vector3(9, posY, 4),
        new Vector3(9, posY, 3),
        new Vector3(9, posY, 2),
        new Vector3(9, posY, 1),
        new Vector3(9, posY, 0),
        new Vector3(8, posY, 9),
        new Vector3(8, posY, 8),
        new Vector3(8, posY, 7),
        new Vector3(8, posY, 6),
        new Vector3(8, posY, 5),
        new Vector3(8, posY, 4),
        new Vector3(8, posY, 3),
        new Vector3(8, posY, 2),
        new Vector3(8, posY, 1),
        new Vector3(8, posY, 0)
    };

    public static readonly List<Vector3> BattlePositionsPlayer = new List<Vector3>
    {
        new Vector3(1, posY, 9),
        new Vector3(1, posY, 8),
        new Vector3(1, posY, 7),
        new Vector3(1, posY, 6),
        new Vector3(1, posY, 5),
        new Vector3(1, posY, 4),
        new Vector3(1, posY, 3),
        new Vector3(1, posY, 2),
        new Vector3(1, posY, 1),
        new Vector3(1, posY, 0),
        new Vector3(0, posY, 9),
        new Vector3(0, posY, 8),
        new Vector3(0, posY, 7),
        new Vector3(0, posY, 6),
        new Vector3(0, posY, 5),
        new Vector3(0, posY, 4),
        new Vector3(0, posY, 3),
        new Vector3(0, posY, 2),
        new Vector3(0, posY, 1),
        new Vector3(0, posY, 0)
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

    public static void GiveUp()
    {
        SceneManagerScript.LoadScene(Constante.EXPLORATION_SCENE);

        foreach (TacticsBattle tacticsBattle in turnQueue)
        {
            DestroyImmediate(tacticsBattle.gameObject);
        }  

        turnQueue.Clear();
    }
}
