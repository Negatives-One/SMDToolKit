using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;
using System.IO;
using System;
using System.Globalization;

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

    private void Update()
    {
    }

    public void UpdateNotifications()
    {
        GameManager.Instance.UpdateNotifications();
    }

    public void SaveEvent()
    {
        if(File.Exists(Application.persistentDataPath + "/Eventos.json")){
            if (nameField.text.Length > 0 && dataLabel.text != "Nenhuma Data Selecionada" && timeLabel.text != "Nenhum Horário Selecionado")
            {
                JSONObject eventJSON = new JSONObject();
                eventJSON.Add("nome", nameField.text);
                eventJSON.Add("data", dataLabel.text);
                eventJSON.Add("hora", timeLabel.text);
                int eventIndex = GameManager.Instance.NumeroEventos;
                string path = Application.persistentDataPath + "/Eventos.json";
                string jsonString = File.ReadAllText(path);
                JSONObject file = (JSONObject)JSON.Parse(jsonString);
                file.Add(eventIndex.ToString(), eventJSON);

                //string path = Application.persistentDataPath + "/Eventos.json";
                File.WriteAllText(path, file.ToString());
                GameManager.Instance.UpdateCount();
            }
        }
        else
        {
            if (nameField.text.Length > 0 && dataLabel.text != "Nenhuma Data Selecionada" && timeLabel.text != "Nenhum Horário Selecionado")
            {
                JSONObject eventJSON = new JSONObject();
                eventJSON.Add("nome", nameField.text);
                eventJSON.Add("data", dataLabel.text);
                eventJSON.Add("hora", timeLabel.text);
                JSONObject events = new JSONObject();
                events.Add("0", eventJSON);

                string path = Application.persistentDataPath + "/Eventos.json";
                File.WriteAllText(path, events.ToString());
                GameManager.Instance.UpdateCount();
            }
        }
        DateTime dataHoraOk = GameManager.Instance.stringToDateTime(GameManager.Instance.LoadEventProperty(GameManager.Instance.NumeroEventos - 1, "data"), GameManager.Instance.LoadEventProperty(GameManager.Instance.NumeroEventos - 1, "hora"));
        //string dataHora = GameManager.Instance.LoadEventProperty(0, "data") + " " + GameManager.Instance.LoadEventProperty(0, "hora");
        //DateTime dataHoraOK = DateTime.ParseExact(dataHora, "yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        GameManager.Instance.Notify(GameManager.Instance.LoadEventProperty(GameManager.Instance.NumeroEventos - 1, "nome"), string.Empty, dataHoraOk);
    }

    public void Teste()
    {
        dataLabel.text = DateTime.Now.ToString("yyyy/MM/dd");
        timeLabel.text = DateTime.Now.ToString("HH:mm");
    }

    public void DeleteSave()
    {
        GameManager.Instance.DeleteSave();
    }

    public void Notify()
    {
        GameManager.Instance.Notify("Oi","Tchau", DateTime.Now);
    }
}
