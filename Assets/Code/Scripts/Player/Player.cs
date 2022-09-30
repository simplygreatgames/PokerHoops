using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private DiscardPile discardPile;
        public DiscardPile DiscardPile 
        {
            get { return discardPile; } 
            private set { discardPile = value; } 
        }

        [SerializeField] private Deck deck;
        public Deck Deck
        {
            get { return deck; }
            set { deck = value; }
        }

        [SerializeField] private Hand hand;
        public Hand Hand
        {
            get { return hand; }
            set { hand = value; }
        }

        public void Awake()
        {
            RegisterCardElements();
        }

        private void RegisterCardElements()
        {
            DiscardPile.RegisterDiscardPile(this);
            Hand.RegisterHand(this);
            Deck.RegisterDeck(this);
        }


    }
}
