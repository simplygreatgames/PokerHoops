using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [RequireComponent(typeof(PlayerStateMachine))]
    public class Player : MonoBehaviour
    {
        #region Player Properties

        public int PlayerID { get; private set; }
        public string PlayerName { get; private set; }

        public TeamSchedule TeamSchedule { get; private set; }

        #endregion

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

        #region State & Data

        [Header("State Machine")]
        [SerializeField] private PlayerStateMachine playerStateMachine;
        public PlayerStateMachine PlayerStateMachine
        {
            get => playerStateMachine;
            private set => playerStateMachine = value;
        }

        #endregion

        #region Input

        [Header("Input")]
        [SerializeField] private MouseInput mouseInput;
        public MouseInput MouseInput
        {
            get => mouseInput;
            private set => mouseInput= value;
        }

        #endregion

        #region Unity Methods & Initialization

        public void Awake()
        {
            PlayerID = gameObject.GetInstanceID();

            GetComponents();
            RegisterComponents();
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
                PlayerStateMachine = GetComponent<PlayerStateMachine>();

            if (mouseInput == null)
                MouseInput = GetComponent<MouseInput>();
        }

        private void RegisterComponents()
        {
            DiscardPile.RegisterDiscardPile(this);
            Hand.RegisterHand(this);
            Deck.RegisterDeck(this);
            PlayerStateMachine.RegisterStateMachine(this);
        }

        #endregion
    }
}
