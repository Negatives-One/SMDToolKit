using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class Main : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hiUser;
    [SerializeField]
    private GameObject legendas;
    [SerializeField]
    private TMP_Dropdown dropdown;
    
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
}
