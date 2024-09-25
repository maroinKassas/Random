using System.Collections.Generic;

public class StatsPlayer : Stats
{
    public List<Equipment> Equipment { get; private set; }

    public StatsPlayer() : base("Player", 1, 0, 100, 3, 45, 1, 1, 1, 1, 1, 1)
    {
        //Equipment = equipment;
    }
}
