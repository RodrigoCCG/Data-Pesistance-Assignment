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
        if(MenuManager.Instance.GetHighScore() != null){
            string name = MenuManager.Instance.GetHighScore().name;
            int score = MenuManager.Instance.GetHighScore().score;
            scoreText.text = "High Score - " + name + ": " + score;
        }else{
            scoreText.text = "No High Score"; 
        }
        if(MenuManager.Instance.playerName != ""){
            nameInput.text = MenuManager.Instance.playerName;
        }
    }

    public void SetName(){
        MenuManager.Instance.SetPlayerName(nameInput.text);
    }
}
