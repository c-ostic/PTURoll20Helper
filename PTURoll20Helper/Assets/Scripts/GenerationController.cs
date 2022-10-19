using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerationController : MonoBehaviour
{
    public TextMeshProUGUI jsonTextbox;

    private DataController dataController;
    private Pokemon currentPokemon;

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    public void GeneratePokemon()
    {
        
        currentPokemon = new Pokemon(dataController.ParseSpecies("Eevee"), "Amber", 10, "Male", "Cuddly",
            new StatSet(5, 5, 5, 5, 5, 5));

        currentPokemon.AddMove(dataController.ParseMove("Tackle"));
        currentPokemon.AddMove(dataController.ParseMove("Bite"));

        currentPokemon.AddAbility(dataController.ParseAbility("Adaptability"));

        jsonTextbox.text = currentPokemon.toJson();
        
    }
}
