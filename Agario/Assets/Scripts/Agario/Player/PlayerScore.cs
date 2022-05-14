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
        
        private void Start()
        {
            Score = 0;
            ScoreUI = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
            HighScoreUI = GameObject.Find("High Score No").GetComponent<TextMeshProUGUI>();
            Debug.Log($"Converted highscore: "+ Convert.ToInt32(HighScoreUI.text));
            HighScore = Convert.ToInt32(HighScoreUI.text); // 0 at launch, but updated otherwise
        }

        
        
        private void Update()
        {
            if (Score > HighScore)
            {
                HighScore = Score;
                var reversed = ReverseIntToString(HighScore);
                HighScoreUI.text = reversed;
            }
        }
        
        
        
        public void IncreaseScore(int enemyScore)
        {
            Score += enemyScore;
            ScoreUI.text = "Score: " + Score;
        }

        
        
        private static string ReverseIntToString(int score)
        {
            var text = new string(score.ToString().Reverse().ToArray());
            return text;
        }
    }
}