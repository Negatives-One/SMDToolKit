using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LoadSceneASync());
        }
    }

    IEnumerator LoadSceneASync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GameManager.Instance.GetTargetScene);

        while (asyncOperation.progress < 1)
        {
            slider.value = asyncOperation.progress;
            yield return new WaitForEndOfFrame();
        }
    }

}
