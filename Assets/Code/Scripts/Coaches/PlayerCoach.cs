using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [RequireComponent(typeof(PlayerStateMachine),typeof(TeamRecord),typeof(TeamSchedule))]
    public class PlayerCoach : Coach
    {
        public TeamSchedule TeamSchedule { get; private set; }
        public TeamRecord TeamRecord { get; private set; }

        #region State & Data

        [Header("State Machine")]
        [SerializeField] private PlayerStateMachine playerStateMachine;
        public PlayerStateMachine StateMachine
        {
            get => playerStateMachine;
            private set => playerStateMachine = value;
        }

        #endregion

        #region Input

        [Header("Input")]
        [SerializeField] private bool isLocalPlayer = false;
        public bool IsLocalPlayer 
        { 
            get => isLocalPlayer;
            set
            {
                isLocalPlayer = value;

                if (IsLocalPlayer)
                    GameUIPanel.Instance.RegisterOwner(this);
            }
        }

        [SerializeField] private MouseInput mouseInput;
        public MouseInput MouseInput
        {
            get => mouseInput;
            private set
            {
                Debug.Log("Setting Mouse Input to " + value);
                mouseInput = value;
            }
        }

        [SerializeField] private CardController cardController;
        public CardController CardController
        {
            get => cardController;
            private set => cardController = value;
        }

        #endregion

       #region Unity Methods & Initialization

        public void Awake()
        {
            CoachID = gameObject.GetInstanceID();

            GetComponents();
            RegisterComponents();
        }

        private void GetComponents()
        {
            if (Hand == null)
                Hand = GetComponentInChildren<Hand>();

            if (StateMachine == null)
                StateMachine = GetComponent<PlayerStateMachine>();

            if (MouseInput == null)
                MouseInput = GetComponent<MouseInput>();

            if (CardController == null)
                CardController = GetComponent<CardController>();

            if (TeamSchedule == null)
                TeamSchedule = GetComponent<TeamSchedule>();

            if (TeamRecord == null)
                TeamRecord = GetComponent<TeamRecord>();
        }

        private void RegisterComponents()
        {
            MouseInput.InputOwner = this;
            CardController.Owner = this;
            Hand.RegisterHand(this);
            StateMachine.RegisterStateMachine(this);
        }

        #endregion
    }
}
