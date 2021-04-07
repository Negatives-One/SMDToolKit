using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GerenciadorCurso : MonoBehaviour
{
    [SerializeField]
    private GameObject contentDisciplinas;
    [SerializeField]
    private GameObject disciplinas;
    [SerializeField]
    private GameObject disciplinaPrefab;

    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private GameObject saveButton;
    [SerializeField]
    private GameObject editButton;

    public bool EditMode = false;

    [SerializeField]
    private TMP_Text selectedSemester;

    void Start()
    {
        SetRollSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMeuCurso()
    {
        if (contentDisciplinas.transform.childCount > 0)
        {
            for (int i = 0; i < contentDisciplinas.transform.childCount; i++)
            {
                Destroy(contentDisciplinas.transform.GetChild(i).gameObject);
            }
        }
        switch (selectedSemester.text)
        {
            case "1° Semestre":
                string path = Application.persistentDataPath + "/Disciplinas.json";
                string jsonString = File.ReadAllText(path);
                JSONObject disciplinas = (JSONObject)JSON.Parse(jsonString);
                int quantidade = disciplinas.Count;
                for(int i = 0; i < quantidade; i++)
                {
                    GameObject disciplina = Instantiate(disciplinaPrefab, contentDisciplinas.transform.position, Quaternion.identity, contentDisciplinas.transform);
                    Disciplina script = disciplina.GetComponent<Disciplina>();
                    script.parent = this;
                    script.SetNome(GameManager.Instance.LoadDisciplineProperty(i, "nome"));
                    script.SetDescricao(GameManager.Instance.LoadDisciplineProperty(i, "descricao"));
                    script.SetPerspectiva(GameManager.Instance.LoadDisciplineProperty(i, "perspectiva"));
                    script.SetCarga(GameManager.Instance.LoadDisciplineProperty(i, "carga"));
                    script.SetCreditos(GameManager.Instance.LoadDisciplineProperty(i, "creditos"));
                    script.SetNecessario(GameManager.Instance.LoadDisciplineProperty(i, "necessario"));
                    script.SetAtivo(bool.Parse(GameManager.Instance.LoadDisciplineProperty(i, "ativo")));
                }
                break;
            default:
                break;
        }
        SetRollSize();
    }

    public void SetRollSize()
    {
        int childNum = contentDisciplinas.transform.childCount;
        float bottomValue = 0f;
        if (EditMode)
        {
            if((78f + 213f * childNum + 112f * childNum) > 1192)
            {
                bottomValue = (78f + 213f * childNum + 112f * childNum) - 1192;
            }
        }
        else
        {
            if((78f + 213f * childNum + 112f * childNum) > 1060)
            {
                bottomValue = (78f + 213f * childNum + 112f * childNum) - 1060;
            }
        }
        contentDisciplinas.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        contentDisciplinas.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -bottomValue);
    }

    public void EditButton()
    {
        disciplinas.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 131f);

        EditMode = true;
    }

    public void SaveButton()
    {
        disciplinas.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        EditMode = false;
    }

    public void BackButton()
    {
        disciplinas.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        saveButton.SetActive(false);
        editButton.SetActive(true);
        title.text = "Meu Curso";
        EditMode = false;
        SetRollSize();

    }

    public void ShowDetails()
    {

    }
}
