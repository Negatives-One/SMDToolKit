using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using SimpleJSON;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
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
        if(Input.touchCount > 0)
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
        if(GameManager.Instance.NumeroEventos > 0)
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
        GameObject text = new GameObject();
        text.AddComponent<CanvasRenderer>();
        text.AddComponent<RectTransform>();
        Text t = text.AddComponent<Text>();
        Instantiate(text, content.transform.position, Quaternion.identity, content.transform);
        text.GetComponent<Text>().text = "Março";
        text.GetComponent<Text>().color = Color.black;
        Debug.Log(content.GetComponent<RectTransform>());
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
}
