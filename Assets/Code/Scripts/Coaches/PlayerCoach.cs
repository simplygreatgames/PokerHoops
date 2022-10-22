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
            CoachID = gameObject.GetInstanceID();

            GetComponents();
            RegisterComponents();
        }

        private void GetComponents()
        {
            if (Hand == null)
                Hand = GetComponentInChildren<Hand>();

            if (playerStateMachine == null)
                PlayerStateMachine = GetComponent<PlayerStateMachine>();

            if (mouseInput == null)
                MouseInput = GetComponent<MouseInput>();

            if (TeamSchedule == null)
                TeamSchedule = GetComponent<TeamSchedule>();

            if (TeamRecord == null)
                TeamRecord = GetComponent<TeamRecord>();
        }

        private void RegisterComponents()
        {
            Hand.RegisterHand(this);
            PlayerStateMachine.RegisterStateMachine(this);
        }

        #endregion
    }
}
