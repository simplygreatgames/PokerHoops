using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Hand : MonoBehaviour
    {
        public HandSlot[] HandSlots { get; private set; }
        public List<Card> Cards = new List<Card>();

        [Header("Debug Settings")]
        public List<Card> DebugCards = new List<Card>();
        public DiscardPile DebugDiscardPile = null;

        #region Unity Methods / Initialize 

        public void Awake()
        {
            RegisterHandSlots();
        }

        private void RegisterHandSlots()
        {
            HandSlots = transform.GetComponentsInChildren<HandSlot>();

            foreach (HandSlot handSlot in HandSlots)
                handSlot.RegisterHandSlot(this);
        }

        #endregion

        #region Add / Remove From Hand

        public void AddToHand(Card card)
        {
            if (IsCardAlreadyInHand(card))
            {
                Debug.LogWarning("Trying to add card that is already in hand");
                return;
            }

            bool slotFound = false;

            foreach (HandSlot handSlot in HandSlots)
            {
                if (!handSlot.IsFilled)
                {
                    handSlot.AddToSlot(card);
                    Cards.Add(card);
                    slotFound = true;
                    break;
                }
            }

            if (slotFound == false)
                Debug.Log("Could not find empty slot to place card");
        }

        public void DiscardFromHand(HandSlot handSlot)
        {
            if (handSlot.CardInSlot == null)
            {
                Debug.LogWarning("Trying to remove card from handslot, but card in slot can't be found: " + handSlot.gameObject.name);
                return;
            }

            if (Cards.Contains(handSlot.CardInSlot))
            {
                Cards.Remove(handSlot.CardInSlot);
                handSlot.DiscardFromSlot();
            }

            else Debug.LogWarning("Trying to remove card in slot but card doesn't exist in the list: " + handSlot.CardInSlot.name);
        }

        #endregion

        #region Helpers

        private bool IsCardAlreadyInHand(Card card)
        {
            if (Cards.Contains(card)) return true;
            else return false;
        }

        #endregion

        #region Debug Methods

        public void Debug_AddCardsToHand()
        {
            if (!Application.isPlaying)
                return;

            foreach (Card card in DebugCards)
                AddToHand(card);
        }

        public void Debug_RemoveCardsFromHand()
        {
            if (!Application.isPlaying)
                return;

            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.IsFilled)
                    DiscardFromHand(handSlot);
            }
        }

        #endregion
    }
}
