using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class CardController : MonoBehaviour
    {
        public PlayerCoach Owner { get; set; }

        public Card ActiveCard { get; private set; }

        public void OnCardLeftClicked(Card card)
        {
            if (card.CurrentOwner != null && card.CurrentOwner.CoachID != Owner.CoachID)
                return;

            ActiveCard = card;
            DetermineAction();
        }

        public void OnCardRightClicked(Card card)
        {

        }

        private void DetermineAction()
        {
            if (Owner.CurrentGame.StateMachine.CurrentState.GetType() == typeof(DiscardState))
                ToggleDiscard();
        }

        private void ToggleDiscard()
        {
            ActiveCard.DeclareForDiscard(!ActiveCard.IsToBeDiscarded);
        }
    }
}
