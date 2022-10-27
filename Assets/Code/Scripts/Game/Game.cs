using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Game : MonoBehaviour
    {
        [Header("Settings")]
        private RoundSettings roundSettings;
        public RoundSettings RoundSettings 
        {
            get => roundSettings;
            set => roundSettings = value;
        }

        [Header("Game Info")]
        [SerializeField] private List<Coach> coachesInGame;
        public List<Coach> CoachesInGame 
        { 
            get => coachesInGame;
            set => coachesInGame = value;
        }

        [Header("State Machine")]
        [SerializeField] private GameStateMachine stateMachine;
        public GameStateMachine StateMachine { get => stateMachine; private set => stateMachine = value; }

        public void Awake()
        {
            GetComponents();    
        }

        private void GetComponents()
        {
            if (StateMachine == null)
                StateMachine = GetComponent<GameStateMachine>();
        }

        public void RegisterCoachesInGame()
        {
            foreach (Coach coachInGame in CoachesInGame)
                coachInGame.CurrentGame = this;
        }
    }
}
