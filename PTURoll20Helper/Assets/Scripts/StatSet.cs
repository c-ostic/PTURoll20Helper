public struct StatSet
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpeAttack { get; set; }
    public int SpeDefense { get; set; }
    public int Speed { get; set; }

    public StatSet(int hp, int atk, int def, int spAtk, int spDef, int spd)
    {
        Health = hp;
        Attack = atk;
        Defense = def;
        SpeAttack = spAtk;
        SpeDefense = spDef;
        Speed = spd;
    }
}