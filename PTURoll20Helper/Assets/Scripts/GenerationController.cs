using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GenerationController : MonoBehaviour
{
    public TMP_InputField speciesInput;
    public TMP_InputField allocatedHPInput;
    public TMP_InputField allocatedATKInput;
    public TMP_InputField allocatedDEFInput;
    public TMP_InputField allocatedSPATKInput;
    public TMP_InputField allocatedSPDEFInput;
    public TMP_InputField allocatedSPDInput;
    public TMP_InputField[] moveInputs;
    public TMP_InputField[] abilityInputs;
    public TextMeshProUGUI jsonTextbox;

    private DataController dataController;
    private Pokemon currentPokemon;

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    public void GeneratePokemon()
    {
        PokemonSpecies species = dataController.ParseSpecies(speciesInput.text);

        // if the pokemon isn't found, don't continue
        if (species == null)
        {
            Debug.Log("No pokemon found");
            return;
        }

        // create the pokemon
        currentPokemon = new Pokemon(species, "Amber", 10, "Male", "Cuddly",
            new StatSet(parseStat(allocatedHPInput.text),
                        parseStat(allocatedATKInput.text),
                        parseStat(allocatedDEFInput.text),
                        parseStat(allocatedSPATKInput.text),
                        parseStat(allocatedSPDEFInput.text),
                        parseStat(allocatedSPDInput.text)));

        // load the moves
        foreach (string moveText in moveInputs.Select(input => input.text))
        {
            // if the input field was left empty, ignore it
            if (moveText.Equals(""))
                continue;

            // try to parse the move
            Move move = dataController.ParseMove(moveText);

            if (move != null)
            {
                // if the move came back successfully, add it to the pokemon
                currentPokemon.AddMove(move);
            }
            else
            {
                // otherwise, send a debug message
                Debug.Log("Move " + moveText + " not found");
            }
        }

        // load the abilities
        foreach (string abilityText in abilityInputs.Select(input => input.text))
        {
            // if the input field was left empty, ignore it
            if (abilityText.Equals(""))
                continue;

            // try to parse the ability
            Ability ability = dataController.ParseAbility(abilityText);

            if (ability != null)
            {
                // if the ability came back successfully, add it to the pokemon
                currentPokemon.AddAbility(ability);
            }
            else
            {
                // otherwise, send a debug message
                Debug.Log("Ability " + abilityText + " not found");
            }
        }

        // set the json to the current pokemon's json
        jsonTextbox.text = currentPokemon.toJson();
    }

    private int parseStat(string statText)
    {
        int stat;
        if (Int32.TryParse(statText, out stat))
        {
            return stat;
        }
        else
        {
            return 0;
        }
    }
}
