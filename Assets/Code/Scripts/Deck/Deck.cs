using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private DeckScriptable deckScriptable;
        public DeckScriptable DeckScriptable
        {
            get { return deckScriptable; }
            private set 
            { 
                deckScriptable = value;

                if (deckScriptable)
                    BuildDeck();
            }
        }

        [SerializeField] private List<CardScriptable> currentDeck;
        public List<CardScriptable> CurrentDeck
        {
            get { return currentDeck; }
            private set { currentDeck = value; }
        }

        #region Deck Methods

        public void BuildDeck()
        {
            if (DeckScriptable == null)
            {
                Debug.LogWarning("Deck Scriptable Not Found!");
                return;
            }

            ClearDeck();

            foreach (CardScriptable cardScriptable in DeckScriptable.Cards)
                CurrentDeck.Add(cardScriptable);

            ShuffleDeck();
        }

        public void ClearDeck()
        {
            CurrentDeck.Clear();
        }

        public void ShuffleDeck()
        {
            System.Random random = new System.Random();
            CurrentDeck = CurrentDeck.OrderBy(x => random.Next()).ToList();
        }

        public List<Card> DrawFromDeck(int numberOfCards, Coach coach)
        {
            List<Card> drawnCards = new List<Card>();
            List<CardScriptable> cardScriptablesDrawn = new List<CardScriptable>();

            for (int i = 0; i < numberOfCards; i++)
                cardScriptablesDrawn.Add(CurrentDeck[i]);

            foreach (CardScriptable cardScriptable in cardScriptablesDrawn)
            {
                GameObject cardObj = CardSpawner.Instance.SpawnCard(this.transform, cardScriptable);
                coach.Hand.AddToHand(cardObj.GetComponent<Card>());
                currentDeck.Remove(cardScriptable);
            }

            return drawnCards;
        }

        public void DiscardFromDeck(int numberOfCards)
        {
            List<CardScriptable> cardScriptablesDrawn = new List<CardScriptable>();

            for (int i = 0; i < numberOfCards; i++)
            {
                if (i >= CurrentDeck.Count)
                {
                    Debug.LogWarning("No Cards Available to Discard");
                    return;
                }

                cardScriptablesDrawn.Add(CurrentDeck[i]);
            }

            foreach (CardScriptable cardScriptable in cardScriptablesDrawn)
            {
                GameObject cardObj = CardSpawner.Instance.SpawnCard(this.transform, cardScriptable);
                DealerManager.Instance.DiscardPile.AddToDiscardPile(cardObj.GetComponent<Card>());
                currentDeck.Remove(cardScriptable);
            }
        }

        #endregion
    }
}
