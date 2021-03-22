using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tarefa : MonoBehaviour
{
    public int index;

    public TMP_Text nomeTarefa;
    public TMP_Text timeReference;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string str)
    {
        nomeTarefa.text = str;
    }

    public void SetTimeReference(string str)
    {
        timeReference.text = str;
    }
}
