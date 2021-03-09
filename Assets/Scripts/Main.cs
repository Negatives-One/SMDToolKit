using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hiUser;
    
    // Start is called before the first frame update
    void Start()
    {
        hiUser.text = "Hi, " + GameManager.Instance.username;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
