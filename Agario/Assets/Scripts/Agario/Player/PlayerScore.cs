using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class PlayerScore : MonoBehaviour
    {
        public int Score;
        private TMP_Text ScoreUI;
        
        private void Start()
        {
            Score = 0;
            ScoreUI = FindObjectOfType<TextMeshProUGUI>().GetComponent<TMP_Text>();
        }

        public void IncreaseScore(int enemyScore)
        {
            Score += enemyScore;
            ScoreUI.text = "Score: " + Score;
        }
    }
}