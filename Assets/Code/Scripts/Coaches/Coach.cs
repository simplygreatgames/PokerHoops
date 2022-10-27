using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Coach : MonoBehaviour
    {
        [Header("Coach Information")]

        [SerializeField] private int coachID = 0;
        public int CoachID { get => coachID; set => coachID = value; }

        [SerializeField] private string coachName = "Coach";
        public string CoachName 
        { 
            get => coachName;
            set
            {
                coachName = value;
                gameObject.name = CoachName;
            }
        }

        [Header("Game Information")]
        [SerializeField] private Game currentGame;
        public Game CurrentGame 
        { 
            get => currentGame; 
            set 
            { 
                currentGame = value;
            } 
        }

        [SerializeField] private Hand hand;
        public Hand Hand { get => hand; set => hand = value; }
    }
}
