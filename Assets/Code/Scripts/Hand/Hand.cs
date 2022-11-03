using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Hand : MonoBehaviour
    {
        public Coach Owner { get; private set; }
        public HandSlot[] HandSlots { get; private set; }

        [SerializeField] private int basketballScore;
        public int BasketballScore { get => basketballScore; private set => basketballScore = value; }

        [SerializeField] private PokerScore pokerScore;
        public PokerScore PokerScore 
        { 
            get => pokerScore;
            set
            {
                pokerScore = value;

                if (PokerScore != null)
                {
                    CurrentScoreType = PokerScore.PokerScoreType;
                    CurrentScore = PokerScore.ScoreValue;
                    BasketballScore = Owner.CurrentGame.ScoringTable.TranslatePokerScore(Owner.IsHomePlayer, PokerScore);
                }
            }
        }

        [Header("Run Time Data")]
        [SerializeField] private Enums.PokerScoreType CurrentScoreType;
        [SerializeField] private int CurrentScore;

        [Header("Debug Settings")]
        public List<Card> DebugCards = new List<Card>();
        public DiscardPile DebugDiscardPile = null;

        #region Hand Events

        public delegate void HandChange(List<Card> cardsInHand);
        public event HandChange OnHandChange;

        #endregion

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

        public void RegisterHand(Coach coach)
        {
            Owner = coach;
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
            int slotIndex = -1;

            for (int i = 0; i < HandSlots.Length; i++)
            {
                if (!HandSlots[i].IsFilled)
                {
                    HandSlots[i].AddToSlot(card);
                    slotFound = true;
                    slotIndex = i;
                    break;
                }
            }

            if (slotFound == false)
            {
                Debug.Log("Could not find empty slot to place card");
                return;
            }

            card.RegisterOwner(Owner, slotIndex);
            OnHandChange?.Invoke(GetCardsFromHand());
        }

        public void DiscardMarkedCards()
        {
            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.IsFilled && handSlot.CardInSlot.IsToBeDiscarded)
                    DiscardFromHandSlot(handSlot, Enums.DiscardType.ToOpponent);
            }
        }

        public void DiscardCard(Card card, Enums.DiscardType discardType)
        {
            DiscardFromHandSlot(HandSlots[card.CurrentSlot], discardType);
        }

        public void DiscardFromHandSlot(HandSlot handSlot, Enums.DiscardType discardType)
        {
            if (handSlot.CardInSlot == null)
            {
                Debug.LogWarning("Trying to remove card from handslot, but card in slot can't be found: " + handSlot.gameObject.name);
                return;
            }

            handSlot.CardInSlot.DeregisterOwner();

            switch (discardType)
            {
                case Enums.DiscardType.ToDealer:
                    handSlot.DiscardToDealer(DealerManager.Instance.DiscardPile.transform);
                    break;

                case Enums.DiscardType.ToOpponent:
                    foreach (var otherCoach in Owner.CurrentGame.CoachesInGame)
                    {
                        if (otherCoach.CoachID != Owner.CoachID)
                        {
                            otherCoach.Hand.AddToHand(handSlot.CardInSlot);
                            handSlot.ReleaseCardInSlot();
                        }
                    }
                    break;

                default:
                    Debug.Log("Discard Type {discardType} Not Found");
                    break;
            }

            OnHandChange?.Invoke(GetCardsFromHand());
        }

        #endregion

        #region Helpers

        public List<Card> GetCardsFromHand()
        {
            List<Card> cardsInHand = new List<Card>();

            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.CardPendingSlot != null)
                    cardsInHand.Add(handSlot.CardPendingSlot);

                else if (handSlot.IsFilled && handSlot.CardInSlot != null)
                    cardsInHand.Add(handSlot.CardInSlot);
            }

            return cardsInHand;
        }

        public List<int> GetCardValuesFromHandAceHigh()
        {
            List<int> cardValuesInHand = new List<int>();

            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.CardPendingSlot != null)
                    cardValuesInHand.Add(handSlot.CardPendingSlot.Value);

                else if (handSlot.IsFilled && handSlot.CardInSlot != null)
                    cardValuesInHand.Add(handSlot.CardInSlot.Value);
            }

            for (int i = 0; i < cardValuesInHand.Count; i++)
            {
                if (cardValuesInHand[i] == 1)
                    cardValuesInHand[i] = 14;
            }

            return cardValuesInHand;
        }

        public List<int> GetCardValuesFromHandAceLow()
        {
            List<int> cardValuesInHand = new List<int>();

            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.CardPendingSlot != null)
                    cardValuesInHand.Add(handSlot.CardPendingSlot.Value);

                else if (handSlot.IsFilled && handSlot.CardInSlot != null)
                    cardValuesInHand.Add(handSlot.CardInSlot.Value);
            }

            return cardValuesInHand;
        }

        public Card GetHighestValueCardAceHigh()
        {
            List<Card> cardsInHand = GetCardsFromHand().OrderByDescending(x => x.Value).ToList();
            Card highestCard = null;

            if (cardsInHand[cardsInHand.Count - 1].Value == 1) highestCard = cardsInHand[cardsInHand.Count - 1];
            else highestCard = cardsInHand[0];

            return highestCard;
        }

        public Card GetHighestValueCardAceLow()
        {
            List<Card> cardsInHand = GetCardsFromHand().OrderByDescending(x => x.Value).ToList();
            Card highestCard = cardsInHand[0];

            return highestCard;
        }

        public bool IsCardAlreadyInHand(Card card)
        {
            bool isDuplicateInHand = false;

            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.IsFilled && handSlot.CardInSlot == card)
                    isDuplicateInHand = true;
            }

            return isDuplicateInHand;
        }

        public void DrawToFill()
        {
            int _emptyHandSlots = 0;

            foreach (HandSlot handSlot in HandSlots)
            {
                if (!handSlot.IsFilled)
                    _emptyHandSlots++;
            }

            DealerManager.Instance.Deck.DrawFromDeck(_emptyHandSlots, Owner);
        }

        #endregion

        #region Debug Methods

        public void Debug_DrawCard()
        {
            if (!Application.isPlaying)
                return;

            DealerManager.Instance.Deck.DrawFromDeck(1, Owner);
        }

        public void Debug_DiscardToDealer()
        {
            if (!Application.isPlaying)
                return;

            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.IsFilled)
                {
                    DiscardFromHandSlot(handSlot, Enums.DiscardType.ToDealer);
                    break;
                }
            }
        }

        public void Debug_DiscardToOpponent()
        {
            if (!Application.isPlaying)
                return;

            foreach (HandSlot handSlot in HandSlots)
            {
                if (handSlot.IsFilled)
                {
                    DiscardFromHandSlot(handSlot, Enums.DiscardType.ToOpponent);
                    break;
                }
            }
        }

        #endregion
    }
}
