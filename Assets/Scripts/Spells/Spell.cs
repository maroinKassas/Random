using System.Collections;
using UnityEngine;

public class Spell
{
    public string spellName;
    public string description;
    public float castingTime;
    public int damage;
    public int range;

    public Spell(string spellName, string description, float castingTime, int damage, int range)
    {
        this.spellName = spellName;
        this.description = description;
        this.castingTime = castingTime;
        this.damage = damage;
        this.range = range;
    }

   /* private bool CanCast(TacticsBattle tacticsBattle, TacticsBattle tacticsBattleEnemy)
    {
        Tile tile = tacticsBattle.currentTile;
        Tile tileEnemy = tacticsBattle.GetTargetTile(tacticsBattleEnemy.gameObject);


    }*/

    public IEnumerator Cast(TacticsBattle tacticsBattle, TacticsBattle tacticsBattleEnemy)
    {
        if (castingTime > tacticsBattle.totalTime)
        {
            Debug.Log("Le sort " + spellName + " ne peut pas être lancé : temps insuffisant !");
        }
        else
        {
            while (tacticsBattle.isMoving)
            {
                yield return null;
            }

            ApplyEffects(tacticsBattleEnemy);
            tacticsBattle.totalTime -= castingTime;
        }
    }

    protected virtual void ApplyEffects(TacticsBattle tacticsBattleEnemy)
    {
        tacticsBattleEnemy.healthPoint -= damage;
    }
}
