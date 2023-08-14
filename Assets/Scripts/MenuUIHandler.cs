using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{ 
    public TMP_InputField TextField;

    public void StartNew()
    {
        string name = TextField.text;
        
        if (name != "")
        {
            SceneManager.LoadScene(1);

            Persistance.Instance.SetCurrentName(name);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
