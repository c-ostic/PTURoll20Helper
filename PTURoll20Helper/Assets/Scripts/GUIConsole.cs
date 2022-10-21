using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIConsole : MonoBehaviour
{
    public TextMeshProUGUI consoleText;

    public void Log(string message)
    {
        consoleText.text += message + "\n";
    }

    public void Clear()
    {
        consoleText.text = "";
    }
}
