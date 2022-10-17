using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Game : MonoBehaviour
    {
        [Header("Settings")]
        private GameSettings gameSettings;
        public GameSettings GameSettings 
        {
            get { return gameSettings; }
            set
            {
                gameSettings = value;
                InitializeSettings();
            }
        }

        [Header("Game Info")]
        [SerializeField] private Enums.OpponentType gameType;
        public Enums.OpponentType GameType { get => gameType; set => gameType = value; }

        [SerializeField] private List<Player> playersInGame;
        public List<Player> PlayersInGame { get => playersInGame; set => playersInGame = value; }

        [Header("State Machine")]
        [SerializeField] private GameStateMachine gameStateMachine;
        public GameStateMachine GameStateMachine { get => gameStateMachine; private set => gameStateMachine = value; }

        #region Initialize

        public void InitializeSettings()
        {
            Debug.Log("Initializing Game Settings (Not Implemented)");
        }


        #endregion
    }

    public class GameSettings
    {

    }

    public class DefaultGameSettings : GameSettings
    {

    }
}
