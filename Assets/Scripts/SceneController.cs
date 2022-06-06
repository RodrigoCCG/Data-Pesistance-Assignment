using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneController : MonoBehaviour
{
    public void LoadMenu(){
        SceneManager.LoadScene(0);
    }

    public void LoadMain(){
        SceneManager.LoadScene(1);
    }

    public void LoadScores(){
        SceneManager.LoadScene(2);
    }

    public void QuitGame(){
        MenuManager.Instance.SaveHighScore();
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }
}
