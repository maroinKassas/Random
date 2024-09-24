public class PunchSpell : Spell
{

    public PunchSpell() : base("Punch", "Give a good punch", 5.0f, 5, 1)
    {
    }

    protected override void ApplyEffects(TacticsBattle tacticsBattle)
    {
        base.ApplyEffects(tacticsBattle);
    }
}