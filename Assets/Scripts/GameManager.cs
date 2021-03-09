using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

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
        return evento[property].ToString();
    }
}
