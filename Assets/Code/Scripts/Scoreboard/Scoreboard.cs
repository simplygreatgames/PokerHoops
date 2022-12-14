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
        [SerializeField] private TextMeshProUGUI HomePokerTypeTextMesh;
        [SerializeField] private TextMeshProUGUI AwayPokerTypeTextMesh;

        [Header("Debug Info")]
        [SerializeField] private string clockText = "00 : 00";
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

        public void SetScoreBoard(RecordData recordData)
        {
            if (recordData != null)
            {
                ScoreHomeText = recordData.PlayerBasketballScore;
                ScoreAwayText = recordData.OpponentBasketballScore;

                HomePokerTypeTextMesh.text = recordData.PlayerHandType.ToString();
                AwayPokerTypeTextMesh.text = recordData.OpponentHandType.ToString();

                ClockText = "00 : 00";
                PeriodText = 4;
            }

            else
            {
                ScoreHomeText = 0;
                ScoreAwayText = 0;

                HomePokerTypeTextMesh.text = string.Empty;
                AwayPokerTypeTextMesh.text = string.Empty;

                ClockText = "00 : 00";
                PeriodText = 1;
            }
        }
    }
}
