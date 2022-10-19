using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public PokemonSpecies Species { get; private set; }
    public string Nickname { get; private set; }
    public int Level { get; private set; }
    public string Gender { get; private set; }
    public string Nature { get; private set; }
    public StatSet AllocatedStats { get; private set; }

    private List<Move> moveSet = new List<Move>();
    private List<Ability> abilities = new List<Ability>();

    public Pokemon(PokemonSpecies species, string nickname, int level, string gender, string nature, StatSet allocated)
    {
        Species = species;
        Nickname = nickname;
        Level = level;
        Gender = gender;
        Nature = nature;
        AllocatedStats = allocated;
    }

    public void AddMove(Move move)
    {
        moveSet.Add(move);
    }

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public string toJson()
    {
        // Create the connection
        StringWriter jsonStream = new StringWriter();

        // Use the connection
        jsonStream.Write("{\n");
        jsonStream.Write("\"species\":\"" + Species.Species + "\",\n");
        jsonStream.Write("\"nickname\":\"" + Nickname + "\",\n");
        jsonStream.Write("\"type1\":\"" + Species.Type1 + "\",\n");
        jsonStream.Write("\"type2\":\"" + Species.Type2 + "\",\n");
        jsonStream.Write("level:" + Level + ",\n");
        jsonStream.Write("\"gender\":\"" + Gender + "\",\n");
        jsonStream.Write("\"nature\":\"" + Nature + "\",\n");
        jsonStream.Write("\"base_HP\":" + Species.BaseStats.Health + ",\n");
        jsonStream.Write("\"base_ATK\":" + Species.BaseStats.Attack + ",\n");
        jsonStream.Write("\"base_DEF\":" + Species.BaseStats.Defense + ",\n");
        jsonStream.Write("\"base_SPATK\":" + Species.BaseStats.SpeAttack + ",\n");
        jsonStream.Write("\"base_SPDEF\":" + Species.BaseStats.SpeDefense + ",\n");
        jsonStream.Write("\"base_SPEED\":" + Species.BaseStats.Speed + ",\n");
        jsonStream.Write("\"HP\":" + AllocatedStats.Health + ",\n");
        jsonStream.Write("\"ATK\":" + AllocatedStats.Attack + ",\n");
        jsonStream.Write("\"DEF\":" + AllocatedStats.Defense + ",\n");
        jsonStream.Write("\"SPATK\":" + AllocatedStats.SpeAttack + ",\n");
        jsonStream.Write("\"SPDEF\":" + AllocatedStats.SpeDefense + ",\n");
        jsonStream.Write("\"SPEED\":" + AllocatedStats.Speed + ",\n");

        // write the capabilities
        jsonStream.Write("\"Capabilities\": {\n");
        foreach(Capability c in Species.Capabilities)
        {
            jsonStream.Write(c.toString() + ",\n");
        }
        jsonStream.Write("},\n");

        // write the moves
        for(int i = 0;i < moveSet.Count;i++)
        {
            jsonStream.Write("\"Move" + (i + 1) + "\":\n");
            jsonStream.Write(JsonUtility.ToJson(moveSet[i]) + ",\n");
        }

        // write the abilities
        for (int i = 0; i < abilities.Count; i++)
        {
            jsonStream.Write("\"Ability" + (i + 1) + "\":\n");
            jsonStream.Write(JsonUtility.ToJson(abilities[i]) + ",\n");
        }

        // end it off with something that goes with every pokemon (and allows me to end off without a trailing comma)
        jsonStream.Write("\"CharType\":0\n}");

        string jsonString = jsonStream.ToString();

        // Close the connection
        jsonStream.Close();

        return jsonString;
    }
}
