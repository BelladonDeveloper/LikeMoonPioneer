using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsReport : MonoBehaviour
{
    /// <summary>
    /// List with statuses of all buildings
    /// </summary>
    public List<string> statuses;
    public int buildingAmount = 3;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();

        // init list with selected size
        statuses = new List<string>(buildingAmount);
        for (int i = 0; i < buildingAmount; i++)
        {
            statuses.Add("");
        }
        // Getting reports
        ProducedResources.reportToUI += DisplayMessage;
    }

    private void DisplayMessage(int objectNumber, string message)
    {
        // Buildings index starts with 1
        // Array index starts with 0
        // Equate this indexes
        objectNumber--;

        // add message
        statuses[objectNumber] = message;

        // update text
        text.text = "";
        for (int i = 0; i < statuses.Count; i++)
        {
            text.text += i+1 + " - " + statuses[i] + "\n";
        }
    }
}
