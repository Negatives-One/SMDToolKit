using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tarefa : MonoBehaviour
{
    public int index;

    public TMP_Text nomeTarefa;
    public TMP_Text timeReference;

    public Agenda manager;

    void Start()
    {
        if(bool.Parse(GameManager.Instance.LoadEventProperty(index, "concluido")))
        {
            nomeTarefa.fontStyle = FontStyles.Strikethrough;
            transform.GetChild(0).gameObject.GetComponent<Toggle>().isOn = bool.Parse(GameManager.Instance.LoadEventProperty(index, "concluido"));
        }
        transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(OpenTask);
    }

    public void SetName(string str)
    {
        nomeTarefa.text = str;
    }

    public void SetTimeReference(string str)
    {
        timeReference.text = str;
    }

    public void Concluir()
    {
        JSONObject evento = GameManager.Instance.LoadEvent(index);
        evento["concluido"] = !bool.Parse(evento["concluido"]);
        GameManager.Instance.UpdateEvent(index, evento);
        manager.ShowTarefas();
    }

    public void OpenTask()
    {
        manager.ShowTask(index);
    }
}
