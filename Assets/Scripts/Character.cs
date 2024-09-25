using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected bool intoBattle;
    protected TacticsBattle tacticsBattle;
    protected TacticsMove tacticsMove;

    protected virtual void Start()
    {
        tacticsMove = GetComponent<TacticsMove>();
    }

    protected virtual void Update()
    {
        if (intoBattle)
        {
            BattleUpdate();
        }
        else
        {
            ExplorationUpdate();
        }
    }

    protected abstract void BattleUpdate();
    protected abstract void ExplorationUpdate();

    public void GoIntoBattle()
    {
        intoBattle = true;
        tacticsBattle = GetComponent<TacticsBattle>();
        tacticsBattle.InitBattle();

        tacticsMove.SetTacticsBattle(true, tacticsBattle);
    }

    private void OnMouseOver()
    {
        transform.Find("InterfaceCharacter").gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        transform.Find("InterfaceCharacter").gameObject.SetActive(false);
    }
}
