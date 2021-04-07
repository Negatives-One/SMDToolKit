using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disciplina : MonoBehaviour
{
    [SerializeField]
    private Sprite desativado;
    [SerializeField]
    private Sprite ativado;

    public string nome;
    public string descricao;
    public string perspectiva;
    public string carga;
    public string creditos;
    public string necessario;
    public bool ativo;

    private TMPro.TMP_Text nomeText;
    private TMPro.TMP_Text cargaText;
    private TMPro.TMP_Text creditosText;
    private GameObject semAcesso;

    public GerenciadorCurso parent;

    private void Awake()
    {
        nomeText = transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>();
        cargaText = transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>();
        creditosText = transform.GetChild(2).gameObject.GetComponent<TMPro.TMP_Text>();
        semAcesso = transform.GetChild(3).gameObject;
    }
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(clickDiscipline);
    }

    public void SetNome(string value)
    {
        nome = value;
        nomeText.text = nome;
    }
    public void SetDescricao(string value)
    {
        descricao = value;
    }
    public void SetPerspectiva(string value)
    {
        perspectiva = value;
    }
    public void SetCarga(string value)
    {
        carga = value;
        cargaText.text = "Carga horária:  " + carga;
    }
    public void SetCreditos(string value)
    {
        creditos = value;
        creditosText.text = "Créditos:  " + creditos;
    }
    public void SetNecessario(string value)
    {
        necessario = value;
    }
    public void SetAtivo(bool value)
    {
        ativo = value;
        if (ativo)
        {
            GetComponent<Image>().sprite = ativado;
        }
        else
        {
            GetComponent<Image>().sprite = desativado;
            cargaText.gameObject.SetActive(false);
            creditosText.gameObject.SetActive(false);
            semAcesso.SetActive(true);
        }
    }

    public void clickDiscipline()
    {
        parent.ShowDetails();
    }
}
