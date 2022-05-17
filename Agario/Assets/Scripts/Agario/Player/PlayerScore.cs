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
        public int HighScore;
        public int StoredHighScore;
        
        
        private void Start()
        {
            Score = 0;
            ScoreUI = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
            HighScoreUI = GameObject.Find("High Score No").GetComponent<TextMeshProUGUI>();
            Debug.Log($"Converted high score: "+ Convert.ToInt32(HighScoreUI.text));

            StoredHighScore = PlayerPrefs.GetInt("High Score");
            Debug.Log("Player Prefs High Score = " + StoredHighScore);
            HighScoreUI.text = StoredHighScore.ToString();
            HighScore = Convert.ToInt32(HighScoreUI.text); // if no player prefs int exists, convert & use UI string data which is 0 at launch, but updated otherwise
            Debug.Log("High Score = " + HighScore);
        }

        
        
        public void IncreaseScore(int enemyScore)
        {
            Score += enemyScore;
            ScoreUI.text = "Score: " + Score;
            
            if (Score > HighScore)
            {
                HighScore = Score;
                HighScoreUI.text = HighScore.ToString();
                
                if (HighScore > StoredHighScore)
                    PlayerPrefs.SetInt("High Score", HighScore);
            }
        }

        
        
        private static string ReverseIntToString(int score)
        {
            var text = new string(score.ToString().Reverse().ToArray());
            return text;
        }
    }
}