using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] InputField nameInput;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //Displays the Highest score on the menu screen.
        if(MenuManager.Instance.GetHighScore() != null){
            string name = MenuManager.Instance.GetHighScore().name;
            int score = MenuManager.Instance.GetHighScore().score;
            scoreText.text = "High Score - " + name + ": " + score;
        }else{
            //If there is no high score, display a message instead
            scoreText.text = "No High Score"; 
        }
        //Set the input field to Empty
        if(MenuManager.Instance.playerName != ""){
            nameInput.text = MenuManager.Instance.playerName;
        }
    }

    public void SetName(){
        //Set the player name based on the nameInput field
        MenuManager.Instance.SetPlayerName(nameInput.text);
    }
}
