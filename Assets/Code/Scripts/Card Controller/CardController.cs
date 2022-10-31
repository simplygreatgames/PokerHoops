using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class CardController : MonoBehaviour
    {
        public PlayerCoach Owner { get; set; }

        public Card ActiveCard { get; private set; }

        public void OnCardClicked(Card cardClicked, Enums.MouseInputType mouseInputType)
        {
            if (cardClicked.CurrentOwner != null && cardClicked.CurrentOwner.CoachID != Owner.CoachID)
                return;

            ActiveCard = cardClicked;
            DetermineAction(cardClicked, mouseInputType);
        }

        private void DetermineAction(Card cardClicked, Enums.MouseInputType mouseInputType)
        {
            PlayerState currentPlayerState = Owner.StateMachine.CurrentState;

            if (currentPlayerState is Interfaces.IHandleCards cardHandlerState)
            {
                cardHandlerState.OnCardClicked(cardClicked, mouseInputType);
            }
        }
    }
}
