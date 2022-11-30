using System.IO;

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

    public string toJson()
    {
        // Create the connection
        StringWriter jsonStream = new StringWriter();

        // Use the connection
        jsonStream.Write("{\n");
        jsonStream.Write("\"Name\":\"" + MoveName + "\",");
        jsonStream.Write("\"Type\":\"" + MoveType + "\",");
        jsonStream.Write("\"DType\":\"" + Category + "\",");
        jsonStream.Write("\"DB\":\"" + DamageBase + "\",");
        jsonStream.Write("\"Freq\":\"" + Frequency + "\",");
        jsonStream.Write("\"AC\":\"" + Accuracy + "\",");
        jsonStream.Write("\"Range\":\"" + Range + "\",");
        jsonStream.Write("\"Effects\":\"" + Effects + "\"");
        jsonStream.Write("}");

        string jsonString = jsonStream.ToString();

        // Close the connection
        jsonStream.Close();

        return jsonString;
    }

    public override string ToString()
    {
        // Create the connection
        StringWriter stringStream = new StringWriter();

        // Use the connection
        stringStream.Write("Name: " + MoveName + "\n");
        stringStream.Write("Type: " + MoveType + "\n");
        stringStream.Write("DType: " + Category + "\n");
        stringStream.Write("DB: " + DamageBase + "\n");
        stringStream.Write("Freq: " + Frequency + "\n");
        stringStream.Write("AC: " + Accuracy + "\n");
        stringStream.Write("Range: " + Range + "\n");
        stringStream.Write("Effects: " + Effects);

        string result = stringStream.ToString();

        // Close the connection
        stringStream.Close();

        return result;
    }
}
