using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataController : MonoBehaviour
{
    public TextAsset speciesTextData;
    public TextAsset movesTextData;
    public TextAsset abilitiesTextData;

    // each set of data has two dictionaries
    // most of the data wont be needed, so only parse the data when it needs to be accessed, otherwise, keep it as a string
    private Dictionary<string, string> speciesDataUnparsed = new Dictionary<string, string>();
    private Dictionary<string, PokemonSpecies> speciesData = new Dictionary<string, PokemonSpecies>();

    private Dictionary<string, string> moveDataUnparsed = new Dictionary<string, string>();
    private Dictionary<string, Move> moveData = new Dictionary<string, Move>();

    private Dictionary<string, string> abilityDataUnparsed = new Dictionary<string, string>();
    private Dictionary<string, Ability> abilityData = new Dictionary<string, Ability>();

    // regex to match only commas outside of quotes
    // courtesy of stackoverflow https://stackoverflow.com/questions/632475/regex-to-pick-characters-outside-of-pair-of-quotes
    private Regex regex = new Regex(",(?=(?:[^\"]|\"[^\"]*\")*$)");

    // Start is called before the first frame update
    void Start()
    {
        // load file data into each of the unparsed dictionaries

        // regex to match only newlines outside of quotes
        // courtesy of stackoverflow https://stackoverflow.com/questions/632475/regex-to-pick-characters-outside-of-pair-of-quotes

        // load species
        foreach (string species in speciesTextData.text.Split("\n"))
        {
            // the species name is the first value in the row
            string speciesName = species.Substring(0, species.IndexOf(","));
            speciesDataUnparsed.Add(speciesName, species);
        }

        // load moves
        foreach (string move in movesTextData.text.Split("\n"))
        {
            // the move name is the first value in the row
            string moveName = move.Substring(0, move.IndexOf(","));
            moveDataUnparsed.Add(moveName, move);
        }

        
        // load abilities
        foreach (string ability in abilitiesTextData.text.Split("\n"))
        {
            // the species name is the first value in the row
            string abilityName = ability.Substring(0, ability.IndexOf(","));
            abilityDataUnparsed.Add(abilityName, ability);
        }
    }

    public PokemonSpecies ParseSpecies(string speciesName)
    {
        // check for null and empty
        if (speciesName == null || speciesName.Equals(""))
            return null;
        // if the species has already been parsed, return it
        if (speciesData.ContainsKey(speciesName))
            return speciesData[speciesName];
        // or if the species isn't in the data at all, return null
        else if (!speciesDataUnparsed.ContainsKey(speciesName))
            return null;
        // else the species hasn't been parsed yet

        // split the data and trim off any stray quotation marks
        string[] data = regex.Split(speciesDataUnparsed[speciesName])
            .Select(dataPoint => dataPoint.Trim().Trim('\"')).ToArray();

        // get the base stats
        int health = int.Parse(data[1]);
        int attack = int.Parse(data[2]);
        int defense = int.Parse(data[3]);
        int specialAttack = int.Parse(data[4]);
        int specialDefense = int.Parse(data[5]);
        int speed = int.Parse(data[6]);
        StatSet baseStats = new StatSet(health, attack, defense, specialAttack, specialDefense, speed);

        // get the types
        Type type1 = (Type)Enum.Parse(typeof(Type), data[7].ToUpper());
        Type type2 = (Type)Enum.Parse(typeof(Type), data[8].ToUpper());

        // create the species object
        PokemonSpecies species = new PokemonSpecies(speciesName, baseStats, type1, type2);

        species.AddCapability(new Capability("Overland", int.Parse(data[9])));
        species.AddCapability(new Capability("Sky", int.Parse(data[10])));
        species.AddCapability(new Capability("Swim", int.Parse(data[11])));
        species.AddCapability(new Capability("Levitate", int.Parse(data[12])));
        species.AddCapability(new Capability("Burrow", int.Parse(data[13])));
        species.AddCapability(new Capability("HJ", int.Parse(data[14])));
        species.AddCapability(new Capability("LJ", int.Parse(data[15])));
        species.AddCapability(new Capability("Power", int.Parse(data[16])));

        // TODO: data[17] contains data for NatureWalk, which will be ignored for the time being

        // the remaining capabilities (0 or more) can be either a single string, or a string with a digit
        int i = 18;
        while (i < data.Length && !data[i].Equals("-"))
        {
            string capability = data[i];
            char endChar = capability[capability.Length - 1];

            // if the ending character is a digit, then create the capability with a digit
            if (char.IsDigit(endChar))
            {
                species.AddCapability(new Capability(capability.Substring(0, capability.Length - 1),
                        int.Parse(endChar + "")));
            }
            else
            {
                // otherwise, create the capability with only the string
                species.AddCapability(new Capability(capability.Substring(0, capability.Length)));
            }

            i++;
        }

        speciesData.Add(speciesName, species);

        return species;
    }

    public Move ParseMove(string moveName)
    {
        // check for null and empty
        if (moveName == null || moveName.Equals(""))
            return null;
        // if the species has already been parsed, return it
        if (moveData.ContainsKey(moveName))
            return moveData[moveName];
        // or if the species isn't in the data at all, return null
        else if (!moveDataUnparsed.ContainsKey(moveName))
            return null;
        // else the species hasn't been parsed yet

        // split the data and trim off any stray quotation marks
        string[] data = regex.Split(moveDataUnparsed[moveName])
            .Select(dataPoint => dataPoint.Trim().Trim('\"')).ToArray();

        // parse the data from the string array
        string category = data[1];
        Type type = (Type)Enum.Parse(typeof(Type), data[2].ToUpper());
        string damageBase = data[3];
        string frequency = data[4];
        string accuracy = data[5];
        string range = data[6];
        string effects = data[7];

        // create the move object
        Move move = new Move(moveName, category, type, damageBase, frequency, accuracy, range, effects);

        moveData.Add(moveName, move);

        return move;
    }

    public Ability ParseAbility(string abilityName)
    {
        // check for null and empty
        if (abilityName == null || abilityName.Equals(""))
            return null;
        // if the species has already been parsed, return it
        if (abilityData.ContainsKey(abilityName))
            return abilityData[abilityName];
        // or if the species isn't in the data at all, return null
        else if (!abilityDataUnparsed.ContainsKey(abilityName))
            return null;
        // else the species hasn't been parsed yet

        // split the data and trim off any stray quotation marks
        string[] data = regex.Split(abilityDataUnparsed[abilityName])
            .Select(dataPoint => dataPoint.Trim().Trim('\"')).ToArray();

        // parse the data from the string array
        string frequency = data[1];

        // the way the data I have is formatted, data[2] always has just the info to the ability,
        // but abilities could have triggers, targets, and keywords.
        // if an ability does have one of those, it is listed in data[6]
        // if an ability does NOT have any of those, data[6] is a pure copy of data[2]
        string effect = data[6];
        string effectAdd = "";

        if (effect.StartsWith("Trigger|Keywords|Target"))
            effectAdd = data[2];

        Ability ability = new Ability(abilityName, frequency, effectAdd + " " + effect);

        abilityData.Add(abilityName, ability);

        return ability;
    }
}
