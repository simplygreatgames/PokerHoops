using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Coach : MonoBehaviour
    {
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

        #region Card Properties

        [SerializeField] private Hand hand;
        public Hand Hand
        {
            get => hand;
            set => hand = value;
        }

        #endregion
    }
}
