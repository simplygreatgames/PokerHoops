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
        public List<Player> Players { get; set; }
        public SeasonStats Stats { get; set; }

        [SerializeField] private int roundNumber = 0;
        public int RoundNumber { get => roundNumber; set => roundNumber = value; }

        public void SetSeasonSettings(SeasonSettings settings) 
        {
            Settings = settings;
        }

        private void GetComponents()
        {
            if (stateMachine)
                StateMachine = GetComponent<SeasonStateMachine>();
        }

        public void Awake()
        {
            GetComponents();
            RegisterComponents();
        }

        private void RegisterComponents()
        {
            StateMachine.RegisterStateMachine(this);
        }
    }
}
