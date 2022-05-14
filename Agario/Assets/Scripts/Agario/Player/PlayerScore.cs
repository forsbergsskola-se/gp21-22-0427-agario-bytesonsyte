using System;
using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class PlayerScore : MonoBehaviour
    {
        public int Score;
        public TMP_Text ScoreUI;
        public TMP_Text HighScoreUI;
        public int HighScore;
        
        private void Start()
        {
            Score = 0;
            HighScore = Convert.ToInt32(HighScoreUI.text); // 0 at launch, but updated otherwise
        }

        public void IncreaseScore(int enemyScore)
        {
            Score += enemyScore;
            ScoreUI.text = "Score: " + Score;

            if (Score !> HighScore) return;
            HighScore = Score;
            HighScoreUI.text = HighScore.ToString();
        }
    }
}