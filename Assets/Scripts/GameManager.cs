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
        if(!File.Exists(Application.persistentDataPath + "/Quarto.json"))
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

        if (!File.Exists(Application.persistentDataPath + "/Eventos.json"))
        {
            NumeroEventos = 0;
        }
        else
        {
            string path = Application.persistentDataPath + "/Eventos.json";
            string jsonString = File.ReadAllText(path);
            JSONObject events = (JSONObject)JSONObject.Parse(jsonString);

            NumeroEventos = events.Count;
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
        //UpdateNotifications();
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
            //UpdateNotifications();
        }
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
        DateTime currentIteration = InitialDateTime;
        while(currentIteration.Date != FinalDateTime.Date)
        {
            Notify("SMD Toolkit", text, currentIteration);
            currentIteration.AddDays(1);
        }
        Notify("SMD Toolkit", "Fim do Evento: " + text, FinalDateTime);
    }

    public void UpdateNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
        for(int i = 0; i < NumeroEventos; i++)
        {
            JSONObject evento = LoadEvent(i);
            DateTime myDate = stringToDateTime(evento["data"], evento["hora"]);// DateTime.ParseExact(evento["data"], "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            if(!HasPassed(myDate, DateTime.Now))
            {
                Notify(evento["nome"], string.Empty, myDate);
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


    public DateTime stringToDateTime(string data, string hora)
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
}
