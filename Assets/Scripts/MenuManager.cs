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
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
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
        public string name;
        public int score;
    }

    [System.Serializable]
    public class ScoreBoard{
        public List<HighScore> scoreBoard = new List<HighScore>();
    }

    public void UpdateHighScore(int newScore){
        HighScore newHighScore = new HighScore();
        newHighScore.score = newScore;
        newHighScore.name = playerName;
        scoreList.scoreBoard.Add(newHighScore);
        scoreList.scoreBoard.Sort(delegate(HighScore hScore1,HighScore hScore2){
            if(hScore1 == null && hScore2 == null) return 0;
            if(hScore1 == null) return 1;
            if(hScore2 == null) return -1;
            return hScore2.score.CompareTo(hScore1.score);
        });
        while(scoreList.scoreBoard.Count > 10){
            scoreList.scoreBoard.RemoveAt(scoreList.scoreBoard.Count-1);
        }
        bestScore = scoreList.scoreBoard[0];
    }

    public void SaveHighScore(){
        string json = JsonUtility.ToJson(scoreList);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public string GetScoreBoardString(){
        int pos = 1;
        string highScores = "";
        foreach(HighScore score in scoreList.scoreBoard){
            highScores += $"{pos}.Name {score.name}: Score {score.score} \n";
            pos++;
        }
        return highScores;
    }

    public HighScore GetHighScore(){
        return this.bestScore;
    }

    public void SetPlayerName(string name){
        playerName = name;
    }
}
