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

    public int movementPoint;
    private Coroutine timerCoroutine;

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
        timerCoroutine = StartCoroutine(StartTimer());
    }

    public void EndTurn()
    {
        turn = false;
        ResetStatsTurn();

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
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
