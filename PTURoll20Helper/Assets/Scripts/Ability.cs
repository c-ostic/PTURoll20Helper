using System.IO;

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

    public string toJson()
    {
        // Create the connection
        StringWriter jsonStream = new StringWriter();

        // Use the connection
        jsonStream.Write("{\n");
        jsonStream.Write("\"Name\":\"" + AbilityName + "\",");
        jsonStream.Write("\"Freq\":\"" + Frequency + "\",");
        jsonStream.Write("\"Info\":\"" + Info + "\"");
        jsonStream.Write("}");

        string jsonString = jsonStream.ToString();

        // Close the connection
        jsonStream.Close();

        return jsonString;
    }
}
