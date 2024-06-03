using System.Collections;
using TMPro;
using UnityEngine;

public class Tactics : MonoBehaviour
{
    public Stats stats;
    public bool turn = false;

    public float totalTime;
    public float currentTime;
    public TextMeshProUGUI timerText;

    protected int movementPoint;

    protected void InitTactics()
    {
        stats = GetComponent<Stats>();
        totalTime = stats.totalTime;
        movementPoint = stats.movementPoint;

        TurnManager.AddUnit(this);
    }

    public void BeginTurn()
    {
        turn = true;
        currentTime = totalTime;
        StartCoroutine(StartTimer());
    }

    public void EndTurn()
    {
        turn = false;
        ResetStatsTurn();
        StopCoroutine(StartTimer());
    }

    public void ResetStatsTurn()
    {
        totalTime = stats.totalTime;
        movementPoint = stats.movementPoint;
    }

    private IEnumerator StartTimer()
    {
        while (currentTime > 0)
        {
            // Affiche le temps restant
            timerText.text = currentTime.ToString("F2") + "s";

            // Attend une seconde
            yield return new WaitForSeconds(1f);

            // Diminue le temps courant
            currentTime -= 1f;
        }

        TurnManager.EndTurn();
    }
}
