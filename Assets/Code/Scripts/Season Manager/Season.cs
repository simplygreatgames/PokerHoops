using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Season : MonoBehaviour
    {
        [Header("State Machine")]
        [SerializeField] private SeasonStateMachine stateMachine;
        public SeasonStateMachine StateMachine
        {
            get => stateMachine;
            private set => stateMachine = value;
        }

        public bool Initialized { get; set; }

        [SerializeField] private SeasonSettings settings = null;
        public SeasonSettings Settings 
        {
            get => settings;
            set
            {
                settings = value;
                StateMachine.SetSeasonState(new InitializeSeasonState(StateMachine));
            } 
        }

        [SerializeField] private List<Player> players = null;
        public List<Player> Players { get => players; set => players = value; }

        [SerializeField] private SeasonStats stats = null;
        public SeasonStats Stats { get => stats; set => stats = value; }

        [SerializeField] private int roundNumber = 0;
        public int RoundNumber { get => roundNumber; set => roundNumber = value; }


        #region Unity Methods & Initializing

        public void Awake()
        {
            GetComponents();
            RegisterComponents();
        }

        private void GetComponents()
        {
            if (stateMachine)
                StateMachine = GetComponent<SeasonStateMachine>();
        }

        private void RegisterComponents()
        {
            StateMachine.RegisterStateMachine(this);
        }

        #endregion
    }
}
