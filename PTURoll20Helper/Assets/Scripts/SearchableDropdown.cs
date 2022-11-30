using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SearchableDropdown : MonoBehaviour
{
    public TMP_InputField inputField;

    private TMP_Dropdown dropdown;
    private IEnumerable<TMP_Dropdown.OptionData> allOptions;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        dropdown.onValueChanged.AddListener(SelectOption);
        inputField.onValueChanged.AddListener(SearchOptions);
    }

    // Used by the Dropdown manager to set the list of this dropdown
    public void SetOptions(IEnumerable<TMP_Dropdown.OptionData> options)
    {
        allOptions = options;
    }

    // For when typing in the input field, adjust the current list of options
    public void SearchOptions(string searchString)
    {
        dropdown.ClearOptions();

        dropdown.AddOptions(new List<string>() { "<-- Select -->" });

        if (!searchString.Equals(""))
        {
            dropdown.AddOptions(allOptions.Where(option => 
                option.text.StartsWith(searchString, System.StringComparison.InvariantCultureIgnoreCase)).ToList());
        }

        dropdown.Hide();
        dropdown.Show();
        inputField.Select();
    }

    // If the user selects an option, fill the input field with the option
    public void SelectOption(int optionNum)
    {
        inputField.text = dropdown.options[optionNum].text;
        inputField.MoveToEndOfLine(false, false);
        inputField.onEndEdit.Invoke(inputField.text);
    }
}
