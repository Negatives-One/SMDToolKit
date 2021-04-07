using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using Unity.Notifications.Android;
using System;

public class GameManager : MonoBehaviour
{

    #region SINGLETOM
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    #endregion SINGLETOM

    public string username;

    private int LoadingSceneNumber = 1;

    private int TargetScene;

    public int NumeroEventos;

    public int GetLoadingSceneNumber
    {
        get { return LoadingSceneNumber; }
    }

    public int GetTargetScene
    {
        get { return TargetScene; }
    }
    public int SetTargetScene
    {
        set { TargetScene = value; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/Disciplinas.json"))
        {
            string path = Application.persistentDataPath + "/Disciplinas.json";
            JSONObject file = new JSONObject();

            JSONObject DesenhoI = new JSONObject();
            DesenhoI.Add("nome", "Desenho I");
            DesenhoI.Add("descricao", "A cadeira visa orientar o aluno para treinar suas habilidades em desenho. São diversas atividades que melhoram a noção de dimensionalidade e criatividade do aluno. Os conhecimentos básicos sobre desenho que um profissional de Sistemas e Mídias Digitais deve ter serão abordados na cadeira, desde o entendimento sobre formas e proporção até a noção do uso de cores e a criação de desenhos criativamente.");
            DesenhoI.Add("perspectiva", "Aprender a desenhar não é necessário apenas para artistas profissionais, como é um conhecimento importante para quem trabalha com jogos, mídias ou design gráfico. Visando trabalhar com mídias, o profissional de SMD deve estar preparado para ter perspectivas realistas sobre propostas gráficas e abordagens publicitárias. Dessa forma, o conhecimento sobre cores e sobre desenho se torna essencial.");
            DesenhoI.Add("carga", "64 horas");
            DesenhoI.Add("creditos", "4");
            DesenhoI.Add("necessario", "Desenho 2, Animações 2D, Concepções de Cenários e Personagens.");
            DesenhoI.Add("ativo", true);
            DesenhoI.Add("oculto", false);

            file.Add("0", DesenhoI);
            
            JSONObject ProgI = new JSONObject();
            ProgI.Add("nome", "Programação I");
            ProgI.Add("descricao", string.Empty);
            ProgI.Add("perspectiva", string.Empty);
            ProgI.Add("carga", string.Empty);
            ProgI.Add("creditos", string.Empty);
            ProgI.Add("necessario", string.Empty);
            ProgI.Add("ativo", false);
            ProgI.Add("oculto", false);

            file.Add("1", ProgI);
            
            JSONObject AutoI = new JSONObject();
            AutoI.Add("nome", "Autoração Multimídia I");
            AutoI.Add("descricao", string.Empty);
            AutoI.Add("perspectiva", string.Empty);
            AutoI.Add("carga", string.Empty);
            AutoI.Add("creditos", string.Empty);
            AutoI.Add("necessario", string.Empty);
            AutoI.Add("ativo", false);
            AutoI.Add("oculto", false);

            file.Add("2", AutoI);
            
            JSONObject Hist = new JSONObject();
            Hist.Add("nome", "História do Design");
            Hist.Add("descricao", string.Empty);
            Hist.Add("perspectiva", string.Empty);
            Hist.Add("carga", string.Empty);
            Hist.Add("creditos", string.Empty);
            Hist.Add("necessario", string.Empty);
            Hist.Add("ativo", false);
            Hist.Add("oculto", false);

            file.Add("3", Hist);
            
            JSONObject Intr = new JSONObject();
            Intr.Add("nome", "Introdução a Sistemas e Mídias Digitais");
            Intr.Add("descricao", string.Empty);
            Intr.Add("perspectiva", string.Empty);
            Intr.Add("carga", string.Empty);
            Intr.Add("creditos", string.Empty);
            Intr.Add("necessario", string.Empty);
            Intr.Add("ativo", false);
            Intr.Add("oculto", false);

            file.Add("4", Intr);



            File.WriteAllText(path, file.ToString());
        }

        if (!File.Exists(Application.persistentDataPath + "/Quarto.json"))
        {
            JSONObject roomJSON = new JSONObject();

            roomJSON.Add("Mesa", 0);
            roomJSON.Add("Quadro", 0);
            roomJSON.Add("Prateleira", 0);
            roomJSON.Add("Cama", 0);
            roomJSON.Add("Armario", 0);
            roomJSON.Add("Janela", 0);

            string path = Application.persistentDataPath + "/Quarto.json";
            File.WriteAllText(path, roomJSON.ToString());
        }

        if (File.Exists(Application.persistentDataPath + "/Eventos.json"))
        {
            string path = Application.persistentDataPath + "/Eventos.json";
            string jsonString = File.ReadAllText(path);
            JSONObject events = (JSONObject)JSONObject.Parse(jsonString);

            NumeroEventos = events.Count;
        }
        else
        {
            NumeroEventos = 0;
        }
    }

    //public string LoadProperty(string property)
    //{
    //    string path = Application.persistentDataPath + "/Dados.json";
    //    string jsonString = File.ReadAllText(path);
    //    JSONObject dados = (JSONObject)JSON.Parse(jsonString);
    //    return dados[property].ToString();
    //}

    public void UpdateCount()
    {
        string path = Application.persistentDataPath + "/Eventos.json";
        string jsonString = File.ReadAllText(path);
        JSONObject events = (JSONObject)JSON.Parse(jsonString);
        NumeroEventos = events.Count;
    }

    public JSONObject LoadEvent(int eventIndex)
    {
        string eventIndexStr = eventIndex.ToString();
        string path = Application.persistentDataPath + "/Eventos.json";
        string jsonString = File.ReadAllText(path);
        JSONObject events = (JSONObject)JSON.Parse(jsonString);
        JSONObject evento = (JSONObject)JSON.Parse(events[eventIndex].ToString());
        return evento;
        //test.text = events[eventIndex].ToString();
        //evento[property].ToString();
    }

    public string LoadEventProperty(int eventIndex, string property)
    {
        string eventIndexStr = eventIndex.ToString();
        string path = Application.persistentDataPath + "/Eventos.json";
        string jsonString = File.ReadAllText(path);
        JSONObject events = (JSONObject)JSON.Parse(jsonString);
        JSONObject evento = (JSONObject)JSON.Parse(events[eventIndex].ToString());
        return evento[property];
    }

    public void UpdateEvent(int eventIndex, JSONObject createdEvent)
    {
        string eventIndexStr = eventIndex.ToString();
        string path = Application.persistentDataPath + "/Eventos.json";
        string jsonString = File.ReadAllText(path);
        JSONObject events = (JSONObject)JSON.Parse(jsonString);
        events.Add(eventIndex.ToString(), createdEvent);
        File.WriteAllText(path, events.ToString());
        UpdateNotifications();
    }

    public void RemoveEvent(int eventIndex)
    {
        if(eventIndex < NumeroEventos)
        {
            string path = Application.persistentDataPath + "/Eventos.json";
            string jsonString = File.ReadAllText(path);
            JSONObject events = (JSONObject)JSON.Parse(jsonString);
            JSONObject newEvents = new JSONObject();
            for (int i = 0; i < NumeroEventos; i++)
            {
                if(i != eventIndex)
                {
                    if(i < eventIndex)
                    {
                        newEvents.Add(i.ToString(), LoadEvent(i));
                    }
                    else
                    {
                        newEvents.Add((i - 1).ToString(), LoadEvent(i));
                    }
                }
            }
            File.WriteAllText(path, newEvents.ToString());
            UpdateCount();
            UpdateNotifications();
        }
    }

    public void DuplicateEvent(int eventIndex)
    {
        JSONObject targetEvent = LoadEvent(eventIndex);
        targetEvent["nome"] = targetEvent["nome"] + " (Cópia)";
        string path = Application.persistentDataPath + "/Eventos.json";
        string jsonString = File.ReadAllText(path);
        JSONObject file = (JSONObject)JSONObject.Parse(jsonString);
        file.Add(NumeroEventos.ToString(), targetEvent);

        File.WriteAllText(path, file.ToString());
        UpdateCount();
        UpdateNotifications();
    }

    public void Notify(string title, string text, DateTime date)
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = date;

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");
        
        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    public void MultipleNotify(string text, DateTime InitialDateTime, DateTime FinalDateTime )
    {
        DateTime reference = InitialDateTime;
        int diferencaDias = (int) (FinalDateTime - InitialDateTime).TotalDays;
        for(int i = 0; i < diferencaDias; i++)
        {
            Notify("SMD Toolkit", text, reference);
            reference.AddDays(1);
        }
        Notify("SMD Toolkit", "Fim do Evento: " + text, FinalDateTime);
    }

    public void UpdateNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
        UpdateCount();
        for(int i = 0; i < NumeroEventos; i++)
        {
            JSONObject evento = LoadEvent(i);
            bool isSimple = bool.Parse(evento["simples"]);
            if(evento["dataInicial"] != string.Empty)
            {
                DateTime data = StringToDateTime(evento["dataInicial"], evento["horaInicial"]);
                if (bool.Parse(LoadEventProperty(i, "lembrete")))
                {
                    if (!HasPassed(data, DateTime.Now))
                    {
                        if (isSimple)
                        {
                            if (LoadEventProperty(i, "dataInicial") != string.Empty)
                            {
                                Notify("SMD Toolkit", evento["nome"], data);
                            }
                        }
                        else
                        {
                            if (LoadEventProperty(i, "dataInicial") != string.Empty && LoadEventProperty(i, "dataFinal") != string.Empty)
                            {
                                DateTime final = StringToDateTime(evento["dataFinal"], evento["horaFinal"]);
                                Notify("SMD Toolkit", evento["nome"], data);
                                Notify("SMD Toolkit", "Final do evento: " + evento["nome"], final);
                            }
                        }
                    }
                }
            }
        }
    }
    public bool HasPassed(DateTime fromDate, DateTime expireDate)
    {
        return expireDate - fromDate > TimeSpan.FromSeconds(1);
    }

    public void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/Eventos.json");
        File.Delete(Application.persistentDataPath + "/Dados.json");
    }


    public DateTime StringToDateTime(string data, string hora)
    {
        string datinha = data.ToString() + " " + hora.ToString();
        DateTime myDate = DateTime.Parse(datinha.ToString(), System.Globalization.CultureInfo.InvariantCulture);
        return myDate;
    }

    public JSONObject LoadRoom()
    {
        string path = Application.persistentDataPath + "/Quarto.json";
        string jsonString = File.ReadAllText(path);
        JSONObject room = (JSONObject)JSON.Parse(jsonString);
        return room;
    }

    public string LoadDisciplineProperty(int eventIndex, string property)
    {
        string path = Application.persistentDataPath + "/Disciplinas.json";
        string jsonString = File.ReadAllText(path);
        JSONObject disciplines = (JSONObject)JSON.Parse(jsonString);
        JSONObject discipline = (JSONObject)JSON.Parse(disciplines[eventIndex].ToString());
        return discipline[property];
    }

    public JSONObject LoadDiscipline(int eventIndex)
    {
        string path = Application.persistentDataPath + "/Disciplinas.json";
        string jsonString = File.ReadAllText(path);
        JSONObject disciplines = (JSONObject)JSON.Parse(jsonString);
        JSONObject discipline = (JSONObject)JSON.Parse(disciplines[eventIndex].ToString());
        return discipline;
    }

    public void UpdateDiscipline(int index, string key, bool value)
    {
        string path = Application.persistentDataPath + "/Disciplinas.json";
        string jsonString = File.ReadAllText(path);
        JSONObject disciplinas = (JSONObject)JSON.Parse(jsonString);

        JSONObject newDiscipline = LoadDiscipline(index);

        newDiscipline[key] = value;

        disciplinas.Add(index.ToString(), newDiscipline);
        File.WriteAllText(path, disciplinas.ToString());
    }
}