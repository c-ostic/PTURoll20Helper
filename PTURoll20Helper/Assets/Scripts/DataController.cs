using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        // load file data into each of the unparsed dictionaries

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
        // if the species has already been parsed, return it
        if (speciesData.ContainsKey(speciesName))
            return speciesData.GetValueOrDefault(speciesName);
        // or if the species isn't in the data at all, return null
        else if (!speciesDataUnparsed.ContainsKey(speciesName))
            return null;
        // else the species hasn't been parsed yet

        // regex to match only commas outside of quotes
        // courtesy of stackoverflow https://stackoverflow.com/questions/632475/regex-to-pick-characters-outside-of-pair-of-quotes
        string[] data = speciesDataUnparsed.GetValueOrDefault(speciesName).Split("(,)(?=(?:[^\"]|\"[^\"]*\")*$)");

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

        return species;
    }

    public Move ParseMove(string move)
    {
        return null;
    }

    public Ability ParseAbility(string ability)
    {
        return null;
    }
}
