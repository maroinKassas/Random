using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool intoBattle;
    protected TacticsBattle tacticsBattle;
    protected TacticsMove tacticsMove;

    protected virtual void Start()
    {
        if (intoBattle)
        {
            tacticsBattle = GetComponent<TacticsBattle>();
            tacticsBattle.InitBattle();
        }

        tacticsMove = GetComponent<TacticsMove>();
        tacticsMove.InitMovement(intoBattle, tacticsBattle);
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

    public void SetIntoBattle(bool intoBattle)
    {
        this.intoBattle = intoBattle;
    }
}
