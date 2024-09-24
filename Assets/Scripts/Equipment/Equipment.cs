using System.Collections.Generic;

public class Equipment
{
    public string EquipmentName { get; private set; }
    public string EquipmentType { get; private set; }
    public Dictionary<string, int> Stats { get; private set; }
    public Spell Spell { get; private set; }

    public Equipment(string equipmentName, string equipmentType, Dictionary<string, int> stats, Spell spell)
    {
        EquipmentName = equipmentName;
        EquipmentType = equipmentType;
        Stats = stats;
        Spell = spell; 
    }
}
