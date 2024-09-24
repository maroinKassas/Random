using TMPro;
using UnityEngine;

public class UIManager_Player : MonoBehaviour
{
    private TacticsBattle tacticsBattle;

    public TextMeshProUGUI textHealthPoint;
    public TextMeshProUGUI textRiskPoint;
    public TextMeshProUGUI textMovementPoint;
    public TextMeshProUGUI textTimer;

    public void Start()
    {
        tacticsBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<TacticsBattle>();
    }

    public void Update()
    {
        textHealthPoint.text = tacticsBattle.healthPoint.ToString();
        textRiskPoint.text = tacticsBattle.riskPoint.ToString();
        textMovementPoint.text = tacticsBattle.movementPoint.ToString();
        textTimer.text = tacticsBattle.totalTime.ToString("F2") + "s";
    }
}
