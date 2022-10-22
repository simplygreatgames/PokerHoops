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
        [SerializeField] private List<PlayerCoach> playersInGame;
        public List<PlayerCoach> PlayersInGame { get => playersInGame; set => playersInGame = value; }

        [Header("State Machine")]
        [SerializeField] private GameStateMachine gameStateMachine;
        public GameStateMachine GameStateMachine { get => gameStateMachine; private set => gameStateMachine = value; }
    }
}
