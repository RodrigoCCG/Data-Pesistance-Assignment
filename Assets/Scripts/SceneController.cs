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
        //Load Menu Scene
        SceneManager.LoadScene(0);
    }

    public void LoadMain(){
        //Load GameplayScene
        SceneManager.LoadScene(1);
    }

    public void LoadScores(){
        //Load Score Table Scene
        SceneManager.LoadScene(2);
    }

    public void QuitGame(){
        //Save current High Scores in a Json File and close the game
        MenuManager.Instance.SaveHighScore();
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }
}
