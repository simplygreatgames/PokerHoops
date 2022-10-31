using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Interfaces : MonoBehaviour
    {
        public interface IInteractable
        {
            public void OnLeftClick(PlayerCoach inputOwner);
            public void OnRightClick(PlayerCoach inputOwner);
        }

        public interface IHandleCards
        {
            public void OnCardClicked(Card cardClicked, Enums.MouseInputType mouseInputType);
        }
    }
}
