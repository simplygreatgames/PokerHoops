using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Coach : MonoBehaviour
    {
        public int CoachID { get; set; }
        public string CoachName { get; set; }

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
