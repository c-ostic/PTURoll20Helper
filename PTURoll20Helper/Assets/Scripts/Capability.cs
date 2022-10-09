public class Capability
{
    private string Name { get; set; }
    private int Value { get; set; }

    public Capability(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public Capability(string name)
    {
        Name = name;
        Value = -1;
    }

    public string toString()
    {
        if (Value == -1)
        {
            return "\"" + Name + "\":true";
        }
        else
        {
            return "\"" + Name + "\":" + Value;
        }
    }
}
