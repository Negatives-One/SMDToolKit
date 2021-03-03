using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.IO;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dataLabel;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private TMP_Text inputFieldText;
    [SerializeField]
    private TMP_Text eventList;

    private List<int> lista = new List<int>();

    private IDictionary<string, string> flores = new Dictionary<string, string>()
    {
        {"Margarida" , "Amarelo"},
        {"Rosa", "Vermelho"}
    };

    private string nome;

    private string jsonString;

    private void Awake()
    {

    }

    void Start()
    {
        jsonString = File.ReadAllText("Assets/JSON/names.json");
        JSONNode data = JSON.Parse(jsonString);
        foreach (JSONNode name in data["firstNames"])
        {
            //Debug.Log("nombre: " + record["nombre"].Value + "score: " + record["puntos"].AsInt);
            if (name.ToString().Length == 6)
            {
                Debug.Log(name);
            }
        }
        eventList.text = null;
        PrintDict(flores);
    }

    void Update()
    {
        string time = GetTime();
        dataLabel.text = time;
    }

    public void PrintDict(IDictionary<string, string> dict)
    {
        foreach (KeyValuePair<string, string> item in dict)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
    }

    public void SaveText()
    {
        if (!flores.ContainsKey(inputFieldText.text) && inputField.text.Length > 0)
        {
            flores.Add(inputFieldText.text, GetTime());
        }
        inputField.Select();
        inputField.text = string.Empty;

        UpdateList();
    }

    public void UpdateList()
    {
        eventList.text = string.Empty;
        foreach (KeyValuePair<string, string> item in flores)
        {
            eventList.text += item.Key + ": " + item.Value + "\n\n";
        }
    }

    public string GetTime()
    {
        return System.DateTime.UtcNow.ToLocalTime().ToString();
    }
}
