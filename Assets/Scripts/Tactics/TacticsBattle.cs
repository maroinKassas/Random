using System.Collections;
using UnityEngine;

public class TacticsBattle : Tactics
{
    public bool turn = false;
    public bool isMoving = false;

    public Stats stats;

    public int healthPoint;
    public float totalTime;
    public int movementPoint;
    public int manaPoint;
    public int riskPoint;

    private Coroutine timerCoroutine;

    public void InitBattle()
    {
        stats = GetComponent<Stats>();
        healthPoint = stats.healthPoint;
        totalTime = stats.totalTime;
        movementPoint = stats.movementPoint;
        manaPoint = stats.manaPoint;
        riskPoint = stats.riskPoint;

        InitBattleMap();
        BattleManager.AddUnit(this);
    }

    public void BeginsHisTurn()
    {
        turn = true;
        timerCoroutine = StartCoroutine(StartTimer());
    }

    public void EndsHisTurn()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        StartCoroutine(WaitToEndMoveToEnd());
    }

    public void ResetStats()
    {
        totalTime = stats.totalTime;
        movementPoint = stats.movementPoint;
        manaPoint = stats.manaPoint;
    }

    private IEnumerator StartTimer()
    {
        while (totalTime > 0)
        {
            yield return new WaitForSeconds(1f);
            totalTime -= 1f;
        }

        EndsHisTurn();
    }

    private IEnumerator WaitToEndMoveToEnd()
    {
        while (isMoving)
        {
            yield return null;
        }

        turn = false;
        ResetStats();

        BattleManager.NextTurn();
    }
}
