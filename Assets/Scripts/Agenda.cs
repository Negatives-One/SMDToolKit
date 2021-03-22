using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;
using System;

public class Agenda : MonoBehaviour
{
    [SerializeField]
    private GameObject DataButton;
    [SerializeField]
    private GameObject DuracaoButton;

    [SerializeField]
    private TMP_Dropdown filterDropdown;

    [SerializeField]
    private GameObject tarefa;
    [SerializeField]
    private GameObject contentTarefa;

    private string path;

    private void Start()
    {
        path = Application.persistentDataPath + "/Eventos.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEvent()
    {

    }

    public void ShowTarefas()
    {
        if(contentTarefa.transform.childCount > 0)
        {
            for(int i = 0; i < contentTarefa.transform.childCount; i++)
            {
                Destroy(contentTarefa.transform.GetChild(i).gameObject);
            }
        }
        List<int> aplicaveis = new List<int>();
        switch (filterDropdown.value)
        {
            case 0://hoje
                for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                {
                    if(DateTime.Today == DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "data")))
                    {
                        aplicaveis.Add(i);
                    }
                }
                break;
            case 1://amanhã
                for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                {
                    if (DateTime.Today.AddDays(1).Date == DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "data")))
                    {
                        aplicaveis.Add(i);
                    }
                }
                break;
            case 2://semana
                for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                {
                    DateTime jsonDate = DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "data"));
                    if ((DateTime.Today.AddDays(7).Date - jsonDate).Days < 0 || (DateTime.Today.AddDays(8).Date - jsonDate).Days > 7)
                    {
                        Debug.Log("Not This Week");
                    }
                    else
                    {
                        aplicaveis.Add(i);
                    }
                }
                break;
            case 3://Entrada
                for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                {
                    aplicaveis.Add(i);
                }
                break;
            default:
                Debug.Log("Valor Incorreto");
                break;
        }

        for(int i = 0; i < aplicaveis.Count; i++)
        {
            GameObject tarefaInstance = Instantiate(tarefa, contentTarefa.transform.position, Quaternion.identity, contentTarefa.transform);

            tarefaInstance.GetComponent<Tarefa>().nomeTarefa = tarefaInstance.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            tarefaInstance.GetComponent<Tarefa>().timeReference = tarefaInstance.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();

            tarefaInstance.GetComponent<Tarefa>().SetName(GameManager.Instance.LoadEventProperty(aplicaveis[i], "nome"));
            if(filterDropdown.value == 0)
            {
                tarefaInstance.GetComponent<Tarefa>().SetTimeReference(GameManager.Instance.LoadEventProperty(aplicaveis[i], "hora"));
            }
            else
            {
                tarefaInstance.GetComponent<Tarefa>().SetTimeReference(GameManager.Instance.LoadEventProperty(aplicaveis[i], "data"));
            }
        }
    }

    public void DataClick()
    {
        Color32 color = new Color32(152, 128, 215, 255);
        DataButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = color;
        DataButton.transform.GetChild(1).gameObject.SetActive(true);
        DuracaoButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.gray;
        DuracaoButton.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void DuracaoClick()
    {
        Color32 color = new Color32(152, 128, 215, 255);
        DataButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.gray;
        DataButton.transform.GetChild(1).gameObject.SetActive(false);
        DuracaoButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = color;
        DuracaoButton.transform.GetChild(1).gameObject.SetActive(true);
    }
}
