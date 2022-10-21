using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GenerationController : MonoBehaviour
{
    public TMP_InputField speciesInput;
    public TMP_InputField nicknameInput;
    public TMP_InputField levelInput;
    public TMP_Dropdown genderDropdown;
    public TMP_Dropdown natureDropdown;
    public TMP_InputField allocatedHPInput;
    public TMP_InputField allocatedATKInput;
    public TMP_InputField allocatedDEFInput;
    public TMP_InputField allocatedSPATKInput;
    public TMP_InputField allocatedSPDEFInput;
    public TMP_InputField allocatedSPDInput;
    public TMP_InputField[] moveInputs;
    public TMP_InputField[] abilityInputs;
    public TextMeshProUGUI jsonTextbox;
    public TMP_InputField filePathInput;
    public Button saveButton;
    public Button copyButton;

    private DataController dataController;
    private GUIConsole guiConsole;
    private Pokemon currentPokemon;

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        guiConsole = FindObjectOfType<GUIConsole>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GeneratePokemon()
    {
        PokemonSpecies species = dataController.ParseSpecies(speciesInput.text);

        // if the pokemon isn't found, don't continue
        if (species == null)
        {
            guiConsole.Log("Pokemon \"" + speciesInput.text + "\" not found");
            return;
        }

        // create the pokemon
        currentPokemon = new Pokemon(species, 
            nicknameInput.text, 
            parseStat(levelInput.text), 
            genderDropdown.options[genderDropdown.value].text, 
            natureDropdown.options[natureDropdown.value].text,
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
                guiConsole.Log("Move " + moveText + " not found");
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
                guiConsole.Log("Ability " + abilityText + " not found");
            }
        }

        // set the json to the current pokemon's json
        jsonTextbox.text = currentPokemon.toJson();

        guiConsole.Log("Generation of \"" + speciesInput.text + "\" complete");
        saveButton.interactable = true;
        copyButton.interactable = true;
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

    public void Save()
    {
        string filename;

        if(!filePathInput.text.Equals(""))
        {
            if (Directory.Exists(filePathInput.text))
            {
                filename = filePathInput.text;
            }
            else
            {
                filename = Application.persistentDataPath;
                guiConsole.Log("\"" + filePathInput.text + "\" not found. Using default path");
            }
        }
        else
        {
            filename = Application.persistentDataPath;
            guiConsole.Log("File path not provided. Using default path");
        }

        filename += "\\" + currentPokemon.Species.Species + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";

        //  open the file or create it for us.
        FileStream fileConnection = new FileStream(filename, FileMode.OpenOrCreate);

        // Create (wrap) another stream.
        StreamWriter sWriter = new StreamWriter(fileConnection);

        // Use connection.
        sWriter.WriteLine(currentPokemon.toJson());

        // Close connection.
        sWriter.Close();

        // Close connection.
        fileConnection.Close();

        guiConsole.Log("JSON File saved at \"" + filename + "\"");
    }

    public void CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = currentPokemon.toJson();
        guiConsole.Log("Copied to clipboard");
    }

    public void SetRandomGender()
    {
        genderDropdown.value = UnityEngine.Random.Range(0, genderDropdown.options.Count);
    }

    public void SetRandomNature()
    {
        natureDropdown.value = UnityEngine.Random.Range(0, natureDropdown.options.Count);
    }
}
