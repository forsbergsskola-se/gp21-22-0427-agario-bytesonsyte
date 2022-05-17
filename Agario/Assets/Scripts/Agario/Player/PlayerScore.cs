using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class PlayerScore : MonoBehaviour
    {
        public int Score;
        [HideInInspector] public TextMeshProUGUI ScoreUI;
        private  TextMeshProUGUI HighScoreUI;
        public int StoredHighScore;

        private void Start()
        {
            Score = 0;
            ScoreUI = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
            HighScoreUI = GameObject.Find("High Score No").GetComponent<TextMeshProUGUI>();

            StoredHighScore = PlayerPrefs.GetInt("High Score");
            Debug.Log("Player Prefs High Score = " + StoredHighScore);
            HighScoreUI.text = StoredHighScore.ToString();
        }

        
        
        public void IncreaseScore(int enemyScore)
        {
            Score += enemyScore;
            ScoreUI.text = "Score: " + Score;

            if (Score <= StoredHighScore) return;
            UpdateHighScore();
        }

        

        private void UpdateHighScore()
        {
            HighScoreUI.text = Score.ToString();
                
            if (Score > StoredHighScore)
                PlayerPrefs.SetInt("High Score", Score);
        }
        
        
        
        private static string ReverseIntToString(int score) // had an error where ui was reversing the output
        {
            var text = new string(score.ToString().Reverse().ToArray());
            return text;
        }
    }
}