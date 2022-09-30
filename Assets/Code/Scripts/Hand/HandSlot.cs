using UnityEngine;
using DG.Tweening;

namespace SimplyGreatGames.PokerHoops
{
    public class HandSlot : MonoBehaviour
    {
        public Hand HandOwner { get; private set; }
        public bool IsFilled { get; private set; }
        public Card CardInSlot { get; private set; }
        public Card CardPendingSlot { get; private set; }

        public void RegisterHandSlot(Hand hand)
        {
            HandOwner = hand;
            IsFilled = false;
            CardInSlot = null;
        }

        #region Add / Remove From Slot

        public void AddToSlot(Card card)
        {
            CardPendingSlot = card;
            IsFilled = true;
            MoveCardToSlot();
        }

        public void DiscardFromSlot()
        {
            MoveCardToDispile();
            IsFilled = false;
        }

        #endregion

        #region Move Card To / From Hand Slot

        private void MoveCardToSlot()
        {
            if (CardPendingSlot == null)
            {
                Debug.LogWarning("Trying to move card to slot but it doesn't exists: " + gameObject.name);
                return;
            }

            CardPendingSlot.transform.SetParent(this.transform);
            CardPendingSlot.transform.DOLocalMove(Vector3.zero, 1f).OnComplete(MoveCardToSlotCompleted);
        }

        private void MoveCardToSlotCompleted()
        {
            Debug.Log("Move Card To Slot Completed");
            CardInSlot = CardPendingSlot;
            CardPendingSlot = null;
        }

        private void MoveCardToDispile()
        {
            Debug.LogWarning("Temporary Solution! This should be refactored when player discard piles are created");

            CardInSlot.transform.SetParent(HandOwner.DebugDiscardPile.transform);
            CardInSlot.transform.DOLocalMove(Vector3.zero, 1);

            CardInSlot = null;
        }

        #endregion
    }
}
