using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public HighScore bestScore;
    public ScoreBoard scoreList;
    public string playerName;


    private void Awake()
    {
        //Set this as instance for static acess from other scripts
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //Load the save data of the highscores (If it exists)
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)){
            string json = File.ReadAllText(path);
            scoreList = JsonUtility.FromJson<ScoreBoard>(json);
            if(scoreList.scoreBoard.Count > 0){
                bestScore = scoreList.scoreBoard[0];
            }
        }else{
            bestScore = null;
        }
    }

    [System.Serializable]
    public class HighScore{
        //Structure responsible for storing a player's name and high score.
        public string name;
        public int score;
    }

    [System.Serializable]
    public class ScoreBoard{
        //Structure responsigle for storeing the best scores of the game.
        public List<HighScore> scoreBoard = new List<HighScore>();
    }

    public void UpdateHighScore(int newScore){
        //This function creates a new HighScore Structure and adds it to the scoreList
        HighScore newHighScore = new HighScore();
        newHighScore.score = newScore;
        newHighScore.name = playerName;
        scoreList.scoreBoard.Add(newHighScore);
        //Sorts the Score list to add the new High Score in its correct position
        scoreList.scoreBoard.Sort(delegate(HighScore hScore1,HighScore hScore2){
            if(hScore1 == null && hScore2 == null) return 0;
            if(hScore1 == null) return 1;
            if(hScore2 == null) return -1;
            return hScore2.score.CompareTo(hScore1.score);
        });
        //Removes all scores bellow the 10th best
        while(scoreList.scoreBoard.Count > 10){
            scoreList.scoreBoard.RemoveAt(scoreList.scoreBoard.Count-1);
        }
        //Set the bestScore to the highest score in the ScoreList
        bestScore = scoreList.scoreBoard[0];
    }

    public void SaveHighScore(){
        //Writes the ScoreList into a Json File
        string json = JsonUtility.ToJson(scoreList);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public string GetScoreBoardString(){
        //Creates a string that displays the scores saved in the scoreList and returns it
        int pos = 1;
        string highScores = "";
        foreach(HighScore score in scoreList.scoreBoard){
            highScores += $"{pos}.Name {score.name}: Score {score.score} \n";
            pos++;
        }
        return highScores;
    }

    public HighScore GetHighScore(){
        //Access HighScore
        return this.bestScore;
    }

    public void SetPlayerName(string name){
        //Set new playerName
        playerName = name;
    }
}
