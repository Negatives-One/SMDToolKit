using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;
using System.IO;

public class AgendarEvento : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dataLabel;
    [SerializeField]
    private TMP_Text timeLabel;
    [SerializeField]
    private TMP_InputField nameField;

    [SerializeField]
    Dictionary<int, object> dict;

    [SerializeField]
    private TMP_Text test;

    void Start()
    {

    }

    public void SaveEvent()
    {
        if(nameField.text.Length > 0 && dataLabel.text != "Nenhuma Data Selecionada" && timeLabel.text != "Nenhum Horário Selecionado")
        {
            JSONObject eventJSON = new JSONObject();
            eventJSON.Add("nome", nameField.text);
            eventJSON.Add("data", dataLabel.text);
            eventJSON.Add("hora", timeLabel.text);
            //dict = new Dictionary<int, object>();
            //Dictionary<string, string> evento = new Dictionary<string, string>();
            //evento.Add("nome", nameField.text);
            //evento.Add("data", dataLabel.text);
            //evento.Add("hora", timeLabel.text);
            //dict.Add(0, evento);
            //int key = 1;
            ////((Dictionary<string, int>)dict[key]).Add("100", 100);

            JSONObject events = new JSONObject();
            events.Add("0", eventJSON);

            string path = Application.persistentDataPath + "/Eventos.json";
            File.WriteAllText(path, events.ToString());
        }
    }

    public void Teste()
    {
        dataLabel.text = "datinha";
        timeLabel.text = "horinha";
    }
}
