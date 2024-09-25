using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string nameCharacter;
    public int level;
    public int experiencePoint;
    public int healthPoint;
    public int movementPoint;
    public float totalTime;
    public float attackSpeed;
    public int attackDamage;
    public int abilityPower;
    public int riskPoint;
    public int criticalStrike;
    public int criticalDamage;

    protected List<Spell> spells;

    protected virtual void Start()
    {
        spells = new List<Spell>
        {
            new PunchSpell()
        };
    }

    public Stats(
        string nameCharacter,
        int level,
        int experiencePoint,
        int healthPoint,
        int movementPoint,
        float totalTime,
        float attackSpeed,
        int attackDamage,
        int abilityPower,
        int riskPoint,
        int criticalStrike,
        int criticalDamage)
    {
        this.nameCharacter = nameCharacter;
        this.level = level;
        this.experiencePoint = experiencePoint;
        this.healthPoint = healthPoint;
        this.movementPoint = movementPoint;
        this.totalTime = totalTime;
        this.attackSpeed = attackSpeed;
        this.attackDamage = attackDamage;
        this.abilityPower = abilityPower;
        this.riskPoint = riskPoint;
        this.criticalStrike = criticalStrike;
        this.criticalDamage = criticalDamage;
    }

    public List<Spell> GetSpells() 
    { 
        return spells; 
    }
}
