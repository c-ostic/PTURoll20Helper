using System.IO;

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

    public override string ToString()
    {
        // Create the connection
        StringWriter stringStream = new StringWriter();

        // Use the connection
        stringStream.Write("HP: " + Health + "\n");
        stringStream.Write("ATK: " + Attack + "\n");
        stringStream.Write("DEF: " + Defense + "\n");
        stringStream.Write("SPATK: " + SpeAttack + "\n");
        stringStream.Write("SPDEF: " + SpeDefense + "\n");
        stringStream.Write("SPD: " + Speed);

        string result = stringStream.ToString();

        // Close the connection
        stringStream.Close();

        return result;
    }
}