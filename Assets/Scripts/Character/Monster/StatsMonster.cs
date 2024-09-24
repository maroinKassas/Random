using System.Collections.Generic;
using UnityEngine;

public class StatsMonster : Stats
{
    private new void Start()
    {
        base.Start();
        spells.Add(new DistancePunchSpell());
    }

    public StatsMonster() : base("Monster", 1, 0, 150, 5, 30, 1, 15, 15, 1, 1, 1)
    {
    }
}
