using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using System.IO;

public class Quarto : MonoBehaviour
{
    public Sprite[] Normais = new Sprite[6];
    
    public Sprite[] Diferente = new Sprite[6];

    [SerializeField]
    private Image Mesa;
    [SerializeField]
    private Image Quadro;
    [SerializeField]
    private Image Prateleira;
    [SerializeField]
    private Image Cama;
    [SerializeField]
    private Image Armario;
    [SerializeField]
    private Image Janela;

    void Start()
    {
        UpdateRoom();
    }

    public void UpdateRoom()
    {
        if (File.Exists(Application.persistentDataPath + "/Quarto.json"))
        {
            JSONObject room = GameManager.Instance.LoadRoom();
            Mesa.sprite = Normais[0];
            Quadro.sprite = Normais[1];
            Prateleira.sprite = Normais[2];
            Cama.sprite = Normais[3];
            Armario.sprite = Normais[4];
            Janela.sprite = Normais[5];
            if (room["Mesa"] == 1)
            {
                Mesa.sprite = Diferente[0];
                Mesa.SetNativeSize();
            }
            if (room["Quadro"] == 1)
            {
                Quadro.sprite = Diferente[1];
            }
            if (room["Prateleira"] == 1)
            {
                Prateleira.sprite = Diferente[2];
            }
            if (room["Cama"] == 1)
            {
                Cama.sprite = Diferente[3];
            }
            if (room["Armario"] == 1)
            {
                Armario.sprite = Diferente[4];
            }
            if (room["Janela"] == 1)
            {
                Janela.sprite = Diferente[5];
            }

        }
        else
        {
            Mesa.sprite = Normais[0];
            Quadro.sprite = Normais[1];
            Prateleira.sprite = Normais[2];
            Cama.sprite = Normais[3];
            Armario.sprite = Normais[4];
            Janela.sprite = Normais[5];
        }
    }
}
