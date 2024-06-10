using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.CompareTag("Player"))
            {
                yield break;
            }

            DontDestroyOnLoad(other.gameObject);
            DontDestroyOnLoad(this.gameObject);

            AsyncOperation asyncLoad = SceneManagerScript.LoadScene(Constante.BATTLE_SCENE);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            this.GoIntoBattle();
            other.GetComponent<Character>().GoIntoBattle();

            BattleManager.Init();
        }
    }
}
