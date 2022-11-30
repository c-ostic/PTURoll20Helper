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

    public override string ToString()
    {
        // Create the connection
        StringWriter stringStream = new StringWriter();

        // Use the connection
        stringStream.Write("Name: " + AbilityName + "\n");
        stringStream.Write("Freqrency: " + Frequency + "\n");
        stringStream.Write("Info: " + Info);

        string result = stringStream.ToString();

        // Close the connection
        stringStream.Close();

        return result;
    }
}
