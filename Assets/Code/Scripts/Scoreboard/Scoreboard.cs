using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SimplyGreatGames.PokerHoops
{
    public class Scoreboard : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TextMeshProUGUI clockTextMesh;
        [SerializeField] private TextMeshProUGUI scoreHomeTextMesh;
        [SerializeField] private TextMeshProUGUI scoreAwayTextMesh;
        [SerializeField] private TextMeshProUGUI periodTextMesh;

        [Header("Debug Info")]
        [SerializeField] private string clockText = "0 : 00";
        public string ClockText 
        { 
            get => clockText;
            set 
            { 
                clockText = value;
                clockTextMesh.text = ClockText;
            }
        }

        [SerializeField] [Range(0, 100)] private int scoreHomeText = 0;
        public int ScoreHomeText
        {
            get => scoreHomeText;
            set
            {
                scoreHomeText = value;
                scoreHomeTextMesh.text = ScoreHomeText.ToString();
            }
        }

        [SerializeField] [Range(0, 100)] private int scoreAwayText = 0;
        public int ScoreAwayText
        {
            get => scoreAwayText;
            set
            {
                scoreAwayText = value;
                scoreAwayTextMesh.text = scoreAwayText.ToString();
            }
        }

        [SerializeField] [Range(0,4)] private int periodText = 0;
        public int PeriodText
        {
            get => periodText;
            set
            {
                periodText = value;
                periodTextMesh.text = periodText.ToString();
            }
        }

        public void OnValidate()
        {
            UpdateScoreBoardTextOnValidate();
        }

        private void UpdateScoreBoardTextOnValidate()
        {
            clockTextMesh.text = ClockText;
            scoreHomeTextMesh.text = ScoreHomeText.ToString();
            scoreAwayTextMesh.text = ScoreAwayText.ToString(); ;
            periodTextMesh.text = PeriodText.ToString();
        }
    }
}
