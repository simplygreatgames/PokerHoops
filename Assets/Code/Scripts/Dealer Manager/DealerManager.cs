using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class DealerManager : MonoBehaviour
    {
        public static DealerManager Instance;

        [Header("Card Elements")]
        [SerializeField] private DiscardPile discardPile;
        public DiscardPile DiscardPile
        {
            get => discardPile;
            set => discardPile = value;
        }

        [SerializeField] private Deck deck;
        public Deck Deck
        {
            get => deck;
            set => deck = value;
        }

        #region Unity Methods 

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Dealer Methods

        public void BuildDeck()
        {
            Deck.BuildDeck();
        }

        #endregion
    }
}
