using System.Collections;
using TMPro;
using UnityEngine;

public class Tactics : MonoBehaviour
{
    public bool isMoving = false;
    public bool turn = false;

    public Stats stats;

    public int healthPoint;
    public float totalTime;
    public int movementPoint;
    public int manaPoint;
    public int riskPoint;

    public TextMeshProUGUI textHealthPoint;
    public TextMeshProUGUI textRiskPoint;
    public TextMeshProUGUI textMovementPoint;
    public TextMeshProUGUI textTimer;

    private Coroutine timerCoroutine;

    public void Start()
    {
        stats = GetComponent<Stats>();
        healthPoint = stats.healthPoint;
        totalTime = stats.totalTime;
        movementPoint = stats.movementPoint;
        manaPoint = stats.manaPoint;
        riskPoint = stats.riskPoint;
    }

    public void Init()
    {
        TurnManager.AddUnit(this);
    }

    public void BeginTurn()
    {
        turn = true;
        timerCoroutine = StartCoroutine(StartTimer());
    }

    public void EndTurn()
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
            textTimer.text = totalTime.ToString("F2") + "s";
            yield return new WaitForSeconds(1f);
            totalTime -= 1f;
        }

        EndTurn();
    }

    public void SetStatsText()
    {
        textHealthPoint.text = healthPoint.ToString();
        textRiskPoint.text = riskPoint.ToString();
        textMovementPoint.text = movementPoint.ToString();
    }

    public IEnumerator WaitToEndMoveToEnd()
    {
        while (isMoving)
        {
            yield return null;
        }

        turn = false;
        ResetStats();

        TurnManager.NextTurn();
    }
}
