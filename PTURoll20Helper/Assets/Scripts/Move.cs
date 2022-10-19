using UnityEngine;

[System.Serializable]
public class Move 
{
    public string MoveName { get; private set; }
    public string Category { get; private set; }
    public Type MoveType { get; private set; }
    public string DamageBase { get; private set; }
    public string Frequency { get; private set; }
    public string Accuracy { get; private set; }
    public string Range { get; private set; }
    public string Effects { get; private set; }

    public Move(string moveName, string category, Type type, string damageBase, string frequency,
        string accuracy, string range, string effects)
    {
        MoveName = moveName;
        Category = category;
        MoveType = type;
        DamageBase = damageBase;
        Frequency = frequency;
        Accuracy = accuracy;
        Range = range;
        Effects = effects;
    }
}
