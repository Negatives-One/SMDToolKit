using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginScript : MonoBehaviour
{
    private string username;
    private string password;

    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField passwordField;
    [SerializeField]
    private TMP_Text userWarning;
    [SerializeField]
    private TMP_Text passWarning;

    [SerializeField]
    private GameObject splash;
    void Start()
    {
    }

    public void CheckLogin()
    {
        username = usernameField.text;
        password = passwordField.text;
        Debug.Log(username);
        bool userValidation = isValidUsername(username);
        bool passValidation = isValidPassword(password);
        if (userValidation && passValidation)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            ClearField(usernameField);
            ClearField(passwordField);
        }
    }

    private bool isValidUsername(string user)
    {
        if(user.Length > 0)
        {
            GameManager.Instance.username = username;
            userWarning.color = Color.clear;
            return true;
        }
        else
        {
            userWarning.color = Color.red;
            return false;
        }
    }

    private bool isValidPassword(string pass)
    {
        if (password == "admin")
        {
            passWarning.color = Color.clear;
            return true;
        }
        else
        {
            passWarning.color = Color.red;
            return false;
        }
    }

    private void ClearField(TMP_InputField field)
    {
        field.Select();
        field.text = string.Empty;
    }
}
