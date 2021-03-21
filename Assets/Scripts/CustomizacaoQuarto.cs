using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizacaoQuarto : MonoBehaviour
{
    [SerializeField]
    private GameObject movelSelectionCard;
    [SerializeField]
    private Quarto quartinho;

    private List<GameObject> cards = new List<GameObject>();

    [SerializeField]
    private Sprite[] Mesas = new Sprite[2];
    [SerializeField]
    private Sprite[] Quadros = new Sprite[2];
    [SerializeField]
    private Sprite[] Prateleiras = new Sprite[2];
    [SerializeField]
    private Sprite[] Camas = new Sprite[2];
    [SerializeField]
    private Sprite[] Armarios = new Sprite[2];
    [SerializeField]
    private Sprite[] Janelas = new Sprite[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowRoomCustomization()
    {
        //int numMovel = 6;
        //string[] moveis = { "Mesa", "Quadro", "Prateleira", "Cama", "Armario", "Janela" };
        //string[] nomeSkin = { "Mesa Diferente", "Quadro Diferente", "Prateleira Diferente", "Cama Diferente", "Armário Diferente", "Janela Diferente" };
        foreach(GameObject card in cards)
        {
            Destroy(card);
        }
        cards.Clear();
        GameObject content = transform.GetChild(0).gameObject;
        TMP_Dropdown dropdown = content.transform.GetChild(2).gameObject.GetComponent<TMP_Dropdown>();
        switch (dropdown.value)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Mesas[i];
                    card.GetComponent<movelSelecionavel>().parent = gameObject;
                    card.GetComponent<movelSelecionavel>().index = i;
                    card.GetComponent<movelSelecionavel>().type = "Mesa";
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
                    cards.Add(card);
                }
                break;
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Quadros[i];
                    card.GetComponent<movelSelecionavel>().parent = gameObject;
                    card.GetComponent<movelSelecionavel>().index = i;
                    card.GetComponent<movelSelecionavel>().type = "Quadro";
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
                    cards.Add(card);
                }
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Prateleiras[i];
                    card.GetComponent<movelSelecionavel>().parent = gameObject;
                    card.GetComponent<movelSelecionavel>().index = i;
                    card.GetComponent<movelSelecionavel>().type = "Prateleira";
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
                    cards.Add(card);
                }
                break;
            case 3:
                for (int i = 0; i < 2; i++)
                {
                    GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Camas[i];
                    card.GetComponent<movelSelecionavel>().parent = gameObject;
                    card.GetComponent<movelSelecionavel>().index = i;
                    card.GetComponent<movelSelecionavel>().type = "Cama";
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
                    cards.Add(card);
                }
                break;
            case 4:
                for (int i = 0; i < 2; i++)
                {
                    GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Armarios[i];
                    card.GetComponent<movelSelecionavel>().parent = gameObject;
                    card.GetComponent<movelSelecionavel>().index = i;
                    card.GetComponent<movelSelecionavel>().type = "Armario";
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
                    cards.Add(card);
                }
                break;
            case 5:
                for (int i = 0; i < 2; i++)
                {
                    GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Janelas[i];
                    card.GetComponent<movelSelecionavel>().parent = gameObject;
                    card.GetComponent<movelSelecionavel>().index = i;
                    card.GetComponent<movelSelecionavel>().type = "Janela";
                    card.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
                    cards.Add(card);
                }
                break;
            default:
                Debug.Log("Movel Incorreto");
                break;
        }
        //if (File.Exists(Application.persistentDataPath + "/Quarto.json"))
        //{
        //    JSONObject room = GameManager.Instance.LoadRoom();
            
        //    float windowSize = -(1440 - ((425 * numMovel) + (20 * numMovel + 1)) + 200);
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, windowSize); ;
        //    for (int i = 0; i < numMovel; i++)
        //    {
        //        GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
        //        GameObject text = card.transform.GetChild(0).gameObject;
        //        text.GetComponent<TMP_Text>().text = moveis[i];
        //        TMP_Dropdown dropdown = card.transform.GetChild(1).gameObject.GetComponent<TMP_Dropdown>();
        //        dropdown.options.Clear();

        //        List<string> items = new List<string>();
        //        items.Add("Normal");
        //        items.Add(nomeSkin[i]);

        //        foreach (var item in items)
        //        {
        //            TMP_Dropdown.OptionData a = new TMP_Dropdown.OptionData();
        //            a.text = item;
        //            dropdown.options.Add(a);
        //        }
        //        dropdown.value = room[moveis[i]];
        //    }
        //}
        //else
        //{
        //    GameObject content = transform.GetChild(0).gameObject;
        //    float windowSize = -(1440 - ((220 * numMovel) + (20 * numMovel + 1)) + 200);
        //    content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, windowSize); ;
        //    for (int i = 0; i < numMovel; i++)
        //    {
        //        GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
        //        GameObject text = card.transform.GetChild(0).gameObject;
        //        text.GetComponent<TMP_Text>().text = moveis[i];
        //        TMP_Dropdown dropdown = card.transform.GetChild(1).gameObject.GetComponent<TMP_Dropdown>();
        //        dropdown.options.Clear();

        //        List<string> items = new List<string>();
        //        items.Add("Normal");
        //        items.Add(nomeSkin[i]);

        //        foreach (var item in items)
        //        {
        //            TMP_Dropdown.OptionData a = new TMP_Dropdown.OptionData();
        //            a.text = item;
        //            dropdown.options.Add(a);
        //        }
        //    }
        //}
    }

    public void CardClicked(int index, string type)
    {
        for(int i = 0; i<2; i++)
        {
            if(index == i)
            {
                cards[i].GetComponent<movelSelecionavel>().SetMark(true);
                string path = Application.persistentDataPath + "/Quarto.json";
                string jsonString = File.ReadAllText(path);
                JSONObject roomJSON = (JSONObject)JSON.Parse(jsonString);
                roomJSON.Add(type, index);
                File.WriteAllText(path, roomJSON.ToString());
            }
            else
            {
                cards[i].GetComponent<movelSelecionavel>().SetMark(false);
            }
        }
    }

    //public void UpdateTab(GameObject content, Sprite[] mobilia)
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        GameObject card = Instantiate(movelSelectionCard, content.transform.position, Quaternion.identity, content.transform);
    //        card.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = mobilia[i];
    //        cards.Add(card);
    //    }
    //    if (!File.Exists(Application.persistentDataPath + "/Quarto.json"))
    //    {
            
    //    }
    //}

    //private int[] GetRoomList()
    //{
    //    int[] lista = new int[6];

    //    for (int i = 0; i < 6; i++)
    //    {
    //        GameObject content = transform.GetChild(0).gameObject;
    //        GameObject movelPanel = content.transform.GetChild(i + 1).gameObject;
    //        TMP_Dropdown dropdown = movelPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Dropdown>();

    //        int index = dropdown.value;
    //        lista[i] = index;
    //        //dropdown.options[index].text;
    //    }
    //    return lista;
    //}

    //public void SaveRoom()
    //{
    //    int[] roomData = GetRoomList();

    //    JSONObject roomJSON = new JSONObject();

    //    roomJSON.Add("Mesa", roomData[0]);
    //    roomJSON.Add("Quadro", roomData[1]);
    //    roomJSON.Add("Prateleira", roomData[2]);
    //    roomJSON.Add("Cama", roomData[3]);
    //    roomJSON.Add("Armario", roomData[4]);
    //    roomJSON.Add("Janela", roomData[5]);

    //    string path = Application.persistentDataPath + "/Quarto.json";

    //    File.WriteAllText(path, roomJSON.ToString());
    //    quartinho.UpdateRoom();
    //}

    //public void ClearRoomCardContent()
    //{
    //    GameObject content = transform.GetChild(0).gameObject;
    //    for (int i = 0; i < content.transform.childCount; i++)
    //    {
    //        if (i > 0)
    //        {
    //            Destroy(content.transform.GetChild(i).gameObject);
    //        }
    //    }
    //}
}
