public class Ability
{
    public string AbilityName { get; private set; }
    public string Frequency { get; private set; }
    public string Info { get; private set; }

    public Ability(string abilityName, string freq, string info)
    {
        AbilityName = abilityName;
        Frequency = freq;
        Info = info;
    }
}
