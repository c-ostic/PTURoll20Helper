using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationController : MonoBehaviour
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

    private Pokemon currentPokemon;

    // Start is called before the first frame update
    void Start()
    {
        // load file data into each of the unparsed dictionaries

        // load species
        foreach(string species in speciesTextData.text.Split("\n"))
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

    private PokemonSpecies ParseSpecies(string species)
    {
        return null;
    }

    private Move ParseMove(string move)
    {
        return null;
    }

    private Ability ParseAbility(string ability)
    {
        return null;
    }

    public void GeneratePokemon()
    {

    }
}
