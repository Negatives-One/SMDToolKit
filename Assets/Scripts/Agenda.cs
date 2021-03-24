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


    [SerializeField]
    private GameObject taskPanel;
    [SerializeField]
    private Toggle taskToggle;
    [SerializeField]
    private TMP_InputField taskName;
    [SerializeField]
    private TMP_InputField taskDescription;

    [SerializeField]
    private GameObject prioridades;
    [SerializeField]
    private GameObject categorias;

    private int actualTaskIndex;

    private string path;


    [SerializeField]
    private GameObject dataButton;
    [SerializeField]
    private GameObject duracaoButton;

    [SerializeField]
    private TMP_Text dataLabelU;
    [SerializeField]
    private TMP_Text timeLabelU;

    [SerializeField]
    private TMP_Text dataLabelMI;
    [SerializeField]
    private TMP_Text timeLabelMI;
    [SerializeField]
    private TMP_Text dataLabelMF;
    [SerializeField]
    private TMP_Text timeLabelMF;

    [SerializeField]
    private Toggle lembrete;
    [SerializeField]
    private Toggle repeticao;

    private void Start()
    {
        path = Application.persistentDataPath + "/Eventos.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTarefas()
    {
        if (GameManager.Instance.NumeroEventos > 0)
        {

            if (contentTarefa.transform.childCount > 0)
            {
                for (int i = 0; i < contentTarefa.transform.childCount; i++)
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
                        if (bool.Parse(GameManager.Instance.LoadEventProperty(i, "simples")))
                        {
                            if (DateTime.Today == DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataInicial")))
                            {
                                if (!bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                                {
                                    aplicaveis.Add(i);
                                }
                            }
                        }
                        else //multiplo
                        {
                            if(DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataInicial")) <= DateTime.Today && DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataFinal")) >= DateTime.Today)
                            {
                                if (!bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                                {
                                    aplicaveis.Add(i);
                                }
                            }
                        }
                    }
                    break;
                case 1://amanhã
                    for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                    {
                        if (bool.Parse(GameManager.Instance.LoadEventProperty(i, "simples")))
                        {
                            if (DateTime.Today.AddDays(1).Date == DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataInicial")))
                            {
                                if (!bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                                {
                                    aplicaveis.Add(i);
                                }
                            }
                        }
                        else //multiplo
                        {
                            if (DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataInicial")) <= DateTime.Today.AddDays(1).Date && DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataFinal")) >= DateTime.Today.AddDays(1).Date)
                            {
                                if (!bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                                {
                                    aplicaveis.Add(i);
                                }
                            }
                        }
                    }
                    break;
                case 2://semana
                    for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                    {
                        if (bool.Parse(GameManager.Instance.LoadEventProperty(i, "simples")))
                        {
                            DateTime jsonDate = DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataInicial"));
                            if (!((DateTime.Today.AddDays(7).Date - jsonDate).Days <= 0 || (DateTime.Today.AddDays(7).Date - jsonDate).Days > 7))
                            {
                                if (!bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                                {
                                    aplicaveis.Add(i);
                                }
                            }
                        }
                        else //multiplo
                        {
                            DateTime StartParameter = DateTime.Today;
                            DateTime EndParameter = DateTime.Today.AddDays(7).Date;

                            DateTime JSONStart = DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataInicial"));
                            DateTime JSONEnd = DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataFinal"));

                            bool overlap = StartParameter < JSONEnd && JSONStart < EndParameter;
                            if (overlap)
                            {
                                if (!bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                                {
                                    aplicaveis.Add(i);
                                }
                            }
                            //if (DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataInicial")) <= DateTime.Today.AddDays(1).Date && DateTime.Parse(GameManager.Instance.LoadEventProperty(i, "dataFinal")) >= DateTime.Today.AddDays(1).Date)
                            //{
                            //    aplicaveis.Add(i);
                            //}
                        }
                    }
                    break;
                case 3://Entrada
                    for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                    {
                        if (!bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                        {
                            aplicaveis.Add(i);
                        }
                    }
                    break;
                case 4://Concluido
                    for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
                    {
                        if (bool.Parse(GameManager.Instance.LoadEventProperty(i, "concluido")))
                        {
                            aplicaveis.Add(i);
                        }
                    }
                    break;
                default:
                    Debug.Log("Valor Incorreto");
                    break;
            }

            for (int i = 0; i < aplicaveis.Count; i++)
            {
                GameObject tarefaInstance = Instantiate(tarefa, contentTarefa.transform.position, Quaternion.identity, contentTarefa.transform);

                tarefaInstance.GetComponent<Tarefa>().manager = this;

                tarefaInstance.GetComponent<Tarefa>().index = aplicaveis[i];
                tarefaInstance.GetComponent<Tarefa>().nomeTarefa = tarefaInstance.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
                tarefaInstance.GetComponent<Tarefa>().timeReference = tarefaInstance.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();

                tarefaInstance.GetComponent<Tarefa>().SetName(GameManager.Instance.LoadEventProperty(aplicaveis[i], "nome"));
                if (filterDropdown.value == 0)
                {
                    if (bool.Parse(GameManager.Instance.LoadEventProperty(aplicaveis[i], "simples")))
                    {
                        tarefaInstance.GetComponent<Tarefa>().SetTimeReference(GameManager.Instance.LoadEventProperty(aplicaveis[i], "horaInicial"));
                    }
                    else
                    {
                        string date = GameManager.Instance.LoadEventProperty(aplicaveis[i], "dataInicial") + " - " + GameManager.Instance.LoadEventProperty(aplicaveis[i], "dataFinal");
                        tarefaInstance.GetComponent<Tarefa>().SetTimeReference(date);
                    }
                }
                else
                {
                    if(bool.Parse(GameManager.Instance.LoadEventProperty(aplicaveis[i], "simples")))
                    {
                        //Debug.Log(GameManager.Instance.LoadEventProperty(aplicaveis[i], "dataInicial"));
                        tarefaInstance.GetComponent<Tarefa>().SetTimeReference(GameManager.Instance.LoadEventProperty(aplicaveis[i], "dataInicial"));
                    }
                    else
                    {
                        string date = GameManager.Instance.LoadEventProperty(aplicaveis[i], "dataInicial") + " - " + GameManager.Instance.LoadEventProperty(aplicaveis[i], "dataFinal");
                        tarefaInstance.GetComponent<Tarefa>().SetTimeReference(date);
                    }
                }
            }
        }
    }

    public void OpenTaskPanel(int taskIndex)
    {
        actualTaskIndex = taskIndex;
        taskToggle.isOn = bool.Parse(GameManager.Instance.LoadEventProperty(taskIndex, "concluido"));
        taskName.text = GameManager.Instance.LoadEventProperty(taskIndex, "nome");
        if(GameManager.Instance.LoadEventProperty(taskIndex, "descricao") != "null") 
        {
            taskDescription.text = GameManager.Instance.LoadEventProperty(taskIndex, "descricao");
        }
    }

    public void SetTaskName()
    {
        JSONObject evento = GameManager.Instance.LoadEvent(actualTaskIndex);
        evento["nome"] = taskName.text;
        GameManager.Instance.UpdateEvent(actualTaskIndex, evento);
    }

    public void SetDescription()
    {
        JSONObject evento = GameManager.Instance.LoadEvent(actualTaskIndex);
        evento["descricao"] = taskDescription.text;
        GameManager.Instance.UpdateEvent(actualTaskIndex, evento);
    }

    public void SetPriority(string type)
    {
        JSONObject evento = GameManager.Instance.LoadEvent(actualTaskIndex);
        evento["prioridade"] = type;
        GameManager.Instance.UpdateEvent(actualTaskIndex, evento);
        prioridades.SetActive(false);
    }

    public void SetCategoria(string type)
    {
        JSONObject evento = GameManager.Instance.LoadEvent(actualTaskIndex);
        evento["categoria"] = type;
        GameManager.Instance.UpdateEvent(actualTaskIndex, evento);
        categorias.SetActive(false);
    }

    public void DeleteTask()
    {
        GameManager.Instance.RemoveEvent(actualTaskIndex);

    }

    public void ShowDateTime()
    {
        if(GameManager.Instance.LoadEventProperty(actualTaskIndex, "simples") != "null")
        {
            if(bool.Parse(GameManager.Instance.LoadEventProperty(actualTaskIndex, "simples")))
            {
                DataClick();
                dataLabelU.text = GameManager.Instance.LoadEventProperty(actualTaskIndex, "dataInicial");
                timeLabelU.text = GameManager.Instance.LoadEventProperty(actualTaskIndex, "horaInicial");
            }
            else
            {
                DuracaoClick();
                dataLabelMI.text = GameManager.Instance.LoadEventProperty(actualTaskIndex, "dataInicial");
                timeLabelMI.text = GameManager.Instance.LoadEventProperty(actualTaskIndex, "horaInicial");
                dataLabelMF.text = GameManager.Instance.LoadEventProperty(actualTaskIndex, "dataFinal");
                timeLabelMF.text = GameManager.Instance.LoadEventProperty(actualTaskIndex, "horaFinal");
            }
        }
    }

    public void UpdateDateTime()
    {
        if(dataButton.transform.GetChild(1).gameObject.activeSelf == true)
        {
            JSONObject evento = GameManager.Instance.LoadEvent(actualTaskIndex);
            evento["simples"] = true;
            evento["dataInicial"] = dataLabelU.text;
            evento["horaInicial"] = timeLabelU.text;
            evento["lembrete"] = lembrete.isOn;
            evento["repeticao"] = repeticao.isOn;
            GameManager.Instance.UpdateEvent(actualTaskIndex, evento);
        }
        else if(duracaoButton.transform.GetChild(1).gameObject.activeSelf == true)
        {
            JSONObject evento = GameManager.Instance.LoadEvent(actualTaskIndex);
            evento["simples"] = false;
            evento["dataInicial"] = dataLabelMI.text;
            evento["horaInicial"] = timeLabelMI.text;
            evento["dataFinal"] = dataLabelMF.text;
            evento["horaFinal"] = timeLabelMF.text;
            evento["lembrete"] = lembrete.isOn;
            evento["repeticao"] = false;
            GameManager.Instance.UpdateEvent(actualTaskIndex, evento);
        }
    }

    public void ShowTask(int index)
    {
        actualTaskIndex = index;
        taskPanel.SetActive(true);
        OpenTaskPanel(actualTaskIndex);
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
