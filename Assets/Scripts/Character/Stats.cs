using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int healthPoint;
    public int movementPoint;
    public float totalTime;
    public int attackSpeed;
    public int attackDamage;
    public int manaPoint ;
    public int abilityPower;
    public int riskPoint;
    public float criticalStrike;

    public Stats(
        int healthPoint, 
        int movementPoint, 
        float totalTime, 
        int attackSpeed,
        int attackDamage, 
        int manaPoint, 
        int abilityPower, 
        int riskPoint, 
        float criticalStrike)
    {
        this.healthPoint = healthPoint;
        this.movementPoint = movementPoint;
        this.totalTime = totalTime;
        this.attackSpeed = attackSpeed;
        this.attackDamage = attackDamage;
        this.manaPoint = manaPoint;
        this.abilityPower = abilityPower;
        this.riskPoint = riskPoint;
        this.criticalStrike = criticalStrike;
    }
}
