using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [RequireComponent(typeof(PlayerStateMachineOperator))]
    public class Player : MonoBehaviour
    {
        #region Card Properties

        [Header("Card Elements")]
        [SerializeField] private DiscardPile discardPile;
        public DiscardPile DiscardPile 
        {
            get => discardPile;
            private set => discardPile = value; 
        }

        [SerializeField] private Deck deck;
        public Deck Deck
        {
            get => deck;
            private set => deck = value;
        }

        [SerializeField] private Hand hand;
        public Hand Hand
        {
            get => hand;
            private set => hand = value;
        }

        #endregion

        #region Player State & Data

        [Header("State Machine")]
        [SerializeField] private PlayerStateMachineOperator playerStateMachine;
        public PlayerStateMachineOperator PlayerStateMachine
        {
            get => playerStateMachine;
            private set => playerStateMachine = value;
        }

        #endregion 

        #region Unity Methods & Initialization

        public void Awake()
        {
            GetComponents();
            RegisterCardElements();
        }

        private void GetComponents()
        {
            if (discardPile == null)
                discardPile = GetComponentInChildren<DiscardPile>();

            if (deck == null)
                deck = GetComponentInChildren<Deck>();

            if (hand == null)
                hand = GetComponentInChildren<Hand>();

            if (playerStateMachine == null)
                PlayerStateMachine = GetComponent<PlayerStateMachineOperator>();
        }

        private void RegisterCardElements()
        {
            DiscardPile.RegisterDiscardPile(this);
            Hand.RegisterHand(this);
            Deck.RegisterDeck(this);
            PlayerStateMachine.RegisterStateMachine(this);
        }

        #endregion
    }


}
