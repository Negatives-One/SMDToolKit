using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;
using System.IO;
using System;
using System.Globalization;
using UnityEngine.UI;

public class AgendarEvento : MonoBehaviour
{
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
        if (File.Exists(Application.persistentDataPath + "/Eventos.json"))
        {
            SaveWithRead();
        }
        else
        {
            SaveNoRead();
        }
        GameManager.Instance.UpdateCount();
        //DateTime dataHoraOk = GameManager.Instance.stringToDateTime(GameManager.Instance.LoadEventProperty(GameManager.Instance.NumeroEventos - 1, "data"), GameManager.Instance.LoadEventProperty(GameManager.Instance.NumeroEventos - 1, "hora"));
        //string dataHora = GameManager.Instance.LoadEventProperty(0, "data") + " " + GameManager.Instance.LoadEventProperty(0, "hora");
        //DateTime dataHoraOK = DateTime.ParseExact(dataHora, "yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //GameManager.Instance.Notify(GameManager.Instance.LoadEventProperty(GameManager.Instance.NumeroEventos - 1, "nome"), string.Empty, dataHoraOk);
    }

    private void SaveWithRead()
    {
        JSONObject eventJSON = new JSONObject();
        eventJSON.Add("simples", true);
        eventJSON.Add("nome", "Tarefa" + (GameManager.Instance.NumeroEventos + 1).ToString());
        eventJSON.Add("dataInicial", "");
        eventJSON.Add("horaInicial", "");
        eventJSON.Add("dataFinal", "");
        eventJSON.Add("horaFinal", "");
        eventJSON.Add("prioridade", "");
        eventJSON.Add("categoria", "");
        eventJSON.Add("descricao", "");
        eventJSON.Add("lembrete", "");
        eventJSON.Add("repeticao", "");
        eventJSON.Add("concluido", false);

        int eventIndex = GameManager.Instance.NumeroEventos;
        string path = Application.persistentDataPath + "/Eventos.json";
        string jsonString = File.ReadAllText(path);
        JSONObject file = (JSONObject)JSONObject.Parse(jsonString);
        file.Add(eventIndex.ToString(), eventJSON);

        File.WriteAllText(path, file.ToString());
    } 
    private void SaveNoRead()
    {
        JSONObject eventJSON = new JSONObject();
        eventJSON.Add("simples", true);
        eventJSON.Add("nome", "Tarefa" + (GameManager.Instance.NumeroEventos + 1).ToString());
        eventJSON.Add("dataInicial", "");//DateTime.Today.ToString("yyyy/MM/dd"));
        eventJSON.Add("horaInicial", "");//DateTime.Today.ToString("HH:mm"));
        eventJSON.Add("dataFinal", "");// DateTime.Today.ToString("yyyy/MM/dd"));
        eventJSON.Add("horaFinal", "");// DateTime.Today.ToString("HH:mm"));
        eventJSON.Add("prioridade", "");
        eventJSON.Add("categoria", "");
        eventJSON.Add("descricao", "");
        eventJSON.Add("lembrete", "");
        eventJSON.Add("repeticao", "");
        eventJSON.Add("concluido", false);
        //eventJSON.Add("simples", true);
        //eventJSON.Add("nome", nameFieldU.text);
        //eventJSON.Add("dataInicial", dataLabelU.text);
        //eventJSON.Add("horaInicial", timeLabelU.text);
        //eventJSON.Add("dataFinal", "null");
        //eventJSON.Add("horaFinal", "null");
        //eventJSON.Add("prioridade", "null");
        //eventJSON.Add("categoria", "null");
        //eventJSON.Add("descricao", "null");
        //eventJSON.Add("lembrete", lembrete.isOn);
        //eventJSON.Add("repeticao", lembrete.isOn);
        //eventJSON.Add("concluido", false);

        int eventIndex = GameManager.Instance.NumeroEventos;
        string path = Application.persistentDataPath + "/Eventos.json";

        JSONObject file = new JSONObject();
        file.Add(eventIndex.ToString(), eventJSON);

        //string path = Application.persistentDataPath + "/Eventos.json";
        File.WriteAllText(path, file.ToString());
            //nameFieldU.Select();
            //nameFieldU.text = string.Empty;        
    }

    public void DeleteSave()
    {
        GameManager.Instance.DeleteSave();
    }
}
