using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using SimpleJSON;
using UnityEngine.UI;
using System.IO;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Quarto quartinho;
    [SerializeField]
    private TMP_Text hiUser;
    [SerializeField]
    private GameObject legendas;
    [SerializeField]
    private TMP_Dropdown dropdown;

    [SerializeField]
    private GameObject calendario;
    [SerializeField]
    private GameObject eventCard;


    [SerializeField]
    private GameObject customizationPanel;
    [SerializeField]
    private GameObject movelSelectionCard;

    // Start is called before the first frame update
    void Start()
    {
        hiUser.text = "Hi, " + GameManager.Instance.username;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //Debug.Log(EventSystem.current.IsPointerOverGameObject());
    //    if (Input.touchCount <= 1)
    //    {
    //        if (EventSystem.current.IsPointerOverGameObject())
    //        {
    //            return;
    //        }

    //        //checar se clicou num movel
    //        InteractObjects();
    //    }

    //    //fechar legenda
    //    if (Input.touchCount > 0 || Input.GetMouseButtonDown(0) && legendas.activeSelf == true)
    //    {
    //        legendas.SetActive(false);
    //    }
    //}

    private void InteractObjects()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 position = new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform != null)
                {
                    Debug.Log(hit.transform.gameObject.name);
                }
            }
        }
    }


    public void ShowLegenda()
    {
        legendas.SetActive(!legendas.activeSelf);
    }

    public void ShowApagar()
    {
        if (GameManager.Instance.NumeroEventos > 0)
        {
            dropdown.ClearOptions();
            List<string> m_DropOptions = new List<string>();
            for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
            {
                m_DropOptions.Add(i.ToString());
            }
            dropdown.AddOptions(m_DropOptions);
        }
    }

    public void Delete()
    {
        Debug.Log(dropdown.value);
        GameManager.Instance.RemoveEvent(dropdown.value);
    }

    public void ShowCallendar()
    {
        calendario.SetActive(true);
        UpdateCalendar();
    }

    private void UpdateCalendar()
    {
        GameObject content = calendario.transform.GetChild(0).gameObject;

        for (int i = 0; i < GameManager.Instance.NumeroEventos; i++)
        {
            JSONObject evento = GameManager.Instance.LoadEvent(i);
            GameObject card = Instantiate(eventCard, content.transform.position, Quaternion.identity, content.transform);
            GameObject nome = card.transform.GetChild(0).gameObject; //nome
            GameObject data = card.transform.GetChild(1).gameObject; //data
            GameObject hora = card.transform.GetChild(2).gameObject; //hora

            nome.GetComponent<TMP_Text>().text = evento["nome"];
            data.GetComponent<TMP_Text>().text = evento["data"];
            hora.GetComponent<TMP_Text>().text = evento["hora"];
        }
    }

    public void ShowRoomCustomization()
    {
        int numMovel = 6;
        string[] moveis = { "Mesa", "Quadro", "Prateleira", "Cama", "Armario", "Janela" };
        string[] nomeSkin = { "Mesa Diferente", "Quadro Diferente", "Prateleira Diferente", "Cama Diferente", "Armário Diferente", "Janela Diferente" };

        if (File.Exists(Application.persistentDataPath + "/Quarto.json"))
        {
            JSONObject room = GameManager.Instance.LoadRoom();
            GameObject content = customizationPanel.transform.GetChild(0).gameObject;
            float windowSize = -(1440 - ((220 * numMovel) + (20 * numMovel + 1)) + 200);
            content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, windowSize); ;
            for (int i = 0; i < numMovel; i++)
            {
                GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                GameObject text = card.transform.GetChild(0).gameObject;
                text.GetComponent<TMP_Text>().text = moveis[i];
                TMP_Dropdown dropdown = card.transform.GetChild(1).gameObject.GetComponent<TMP_Dropdown>();
                dropdown.options.Clear();

                List<string> items = new List<string>();
                items.Add("Normal");
                items.Add(nomeSkin[i]);

                foreach (var item in items)
                {
                    TMP_Dropdown.OptionData a = new TMP_Dropdown.OptionData();
                    a.text = item;
                    dropdown.options.Add(a);
                }
                dropdown.value = room[moveis[i]];
            }
        }
        else
        {
            GameObject content = customizationPanel.transform.GetChild(0).gameObject;
            float windowSize = -(1440 - ((220 * numMovel) + (20 * numMovel + 1)) + 200);
            content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, windowSize); ;
            for (int i = 0; i < numMovel; i++)
            {
                GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                GameObject text = card.transform.GetChild(0).gameObject;
                text.GetComponent<TMP_Text>().text = moveis[i];
                TMP_Dropdown dropdown = card.transform.GetChild(1).gameObject.GetComponent<TMP_Dropdown>();
                dropdown.options.Clear();

                List<string> items = new List<string>();
                items.Add("Normal");
                items.Add(nomeSkin[i]);

                foreach (var item in items)
                {
                    TMP_Dropdown.OptionData a = new TMP_Dropdown.OptionData();
                    a.text = item;
                    dropdown.options.Add(a);
                }
            }
        }
    }

    private int[] GetRoomList()
    {
        int[] lista = new int[6];

        for (int i = 0; i < 6; i++)
        {
            GameObject content = customizationPanel.transform.GetChild(0).gameObject;
            GameObject movelPanel = content.transform.GetChild(i + 1).gameObject;
            TMP_Dropdown dropdown = movelPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Dropdown>();

            int index = dropdown.value;
            lista[i] = index;
                //dropdown.options[index].text;
        }
        return lista;
    }

    public void SaveRoom()
    {
        int[] roomData = GetRoomList();

        JSONObject roomJSON = new JSONObject();

        roomJSON.Add("Mesa", roomData[0]);
        roomJSON.Add("Quadro", roomData[1]);
        roomJSON.Add("Prateleira", roomData[2]);
        roomJSON.Add("Cama", roomData[3]);
        roomJSON.Add("Armario", roomData[4]);
        roomJSON.Add("Janela", roomData[5]);

        string path = Application.persistentDataPath + "/Quarto.json";

        File.WriteAllText(path, roomJSON.ToString());
        quartinho.UpdateRoom();
    }

    public void ClearRoomCardContent()
    {
        GameObject content = customizationPanel.transform.GetChild(0).gameObject;
        for(int i = 0; i < content.transform.childCount; i++)
        {
            if( i > 0)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }
    }
}
