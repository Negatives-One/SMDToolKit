using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Disciplina : MonoBehaviour
{
    [SerializeField]
    private Sprite desativado;
    [SerializeField]
    private Sprite ativado;
    [SerializeField]
    private Sprite exibir;
    [SerializeField]
    private Sprite ocultar;

    public string nome;
    public string descricao;
    public string perspectiva;
    public string carga;
    public string creditos;
    public string necessario;
    public bool ativo;
    public bool oculto;

    private TMPro.TMP_Text nomeText;
    private TMPro.TMP_Text cargaText;
    private TMPro.TMP_Text creditosText;
    private GameObject semAcesso;
    private GameObject ocultoText;

    public GerenciadorCurso parent;

    private void Awake()
    {
        nomeText = transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>();
        cargaText = transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>();
        creditosText = transform.GetChild(2).gameObject.GetComponent<TMPro.TMP_Text>();
        semAcesso = transform.GetChild(3).gameObject;
        ocultoText = transform.GetChild(5).gameObject;
    }
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(clickDiscipline);
        transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(VisibilityButton);
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

    public void SetOculto(bool value)
    {
        oculto = value;
        if (oculto)
        {
            nomeText.color = new Color32(255, 255, 255, 76);
            GetComponent<Image>().sprite = desativado;
            GetComponent<Image>().color = new Color32(255, 255, 255, 38);
            cargaText.gameObject.SetActive(false);
            creditosText.gameObject.SetActive(false);
            semAcesso.SetActive(false);
            ocultoText.SetActive(true);
        }
        else
        {
            nomeText.color = new Color32(255, 255, 255, 255);
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (ativo)
            {
                GetComponent<Image>().sprite = ativado;
                cargaText.gameObject.SetActive(true);
                creditosText.gameObject.SetActive(true);
            }
            else
            {
                GetComponent<Image>().sprite = desativado;
                semAcesso.SetActive(true);
            }
            ocultoText.SetActive(false);
        }
        if(nome == "Desenho I")
        {
            GameManager.Instance.UpdateDiscipline(0, "oculto", oculto);
        }
        else if(nome == "Programação I")
        {
            GameManager.Instance.UpdateDiscipline(1, "oculto", oculto);
        }
        else if(nome == "Autoração Multimídia I")
        {
            GameManager.Instance.UpdateDiscipline(2, "oculto", oculto);
        }
        else if(nome == "História do Design")
        {
            GameManager.Instance.UpdateDiscipline(3, "oculto", oculto);
        }
        else if(nome == "Introdução a Sistemas e Mídias Digitais")
        {
            GameManager.Instance.UpdateDiscipline(4, "oculto", oculto);
        }
    }

    public void clickDiscipline()
    {
        if (!oculto && ativo && !parent.EditMode)
        {
            parent.ShowDetails(nome);
        }
    }

    public void VisibilityButton()
    {
        SetOculto(!oculto);
        if (oculto)
        {
            transform.GetChild(4).GetChild(1).gameObject.GetComponent<Image>().sprite = ocultar;
            transform.GetChild(4).GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color32(219, 125, 71, 255);
        }
        else
        {
            transform.GetChild(4).GetChild(1).gameObject.GetComponent<Image>().sprite = exibir;
            transform.GetChild(4).GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color32(152, 128, 215, 255);
        }
        transform.GetChild(4).GetChild(1).gameObject.GetComponent<Image>().SetNativeSize();
    }

    public void SetEdit(bool value)
    {
        transform.GetChild(4).gameObject.SetActive(value);
    }
}
