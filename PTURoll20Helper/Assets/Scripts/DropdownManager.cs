using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class DropdownManager : MonoBehaviour
{
    public SearchableDropdown pokemonDropdown;
    public List<SearchableDropdown> moveDropdowns;
    public List<SearchableDropdown> abilityDropdowns;

    private DataController dataController;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();

        // convert the list of names from the data to dropdown option data
        pokemonDropdown.SetOptions(
            dataController.GetPokemonNames().Select(name => new TMP_Dropdown.OptionData(name)));

        foreach (SearchableDropdown dropdown in moveDropdowns)
        {
            dropdown.SetOptions(
                dataController.GetMoveNames().Select(name => new TMP_Dropdown.OptionData(name)));
        }

        foreach (SearchableDropdown dropdown in abilityDropdowns)
        {
            dropdown.SetOptions(
                dataController.GetAbilityNames().Select(name => new TMP_Dropdown.OptionData(name)));
        }
    }
}
