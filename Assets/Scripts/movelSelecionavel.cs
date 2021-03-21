using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class movelSelecionavel : MonoBehaviour
{
    public GameObject parent;
    public int index;
    public string type;

    private void Start()
    {
        string path = Application.persistentDataPath + "/Quarto.json";
        string jsonString = File.ReadAllText(path);
        JSONObject room = (JSONObject)JSON.Parse(jsonString);
        if(room[type] == index)
        {
            SetMark(true);
        }
    }

    public void clicked()
    {
        parent.GetComponent<CustomizacaoQuarto>().CardClicked(index, type);
    }

    public void SetMark(bool a)
    {
        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(a);
    }

    public bool IsMarked()
    {
        return transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.activeSelf;
    }
}
