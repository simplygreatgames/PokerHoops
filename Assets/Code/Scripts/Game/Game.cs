using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Game : MonoBehaviour
    {
        [Header("Settings")]
        private RoundSettings gameSettings;
        public RoundSettings GameSettings 
        {
            get => gameSettings;
            set => gameSettings = value;
        }

        [Header("Game Info")]

        [SerializeField] private List<PlayerCoach> playersInGame;
        public List<PlayerCoach> PlayersInGame { get => playersInGame; set => playersInGame = value; }

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

    public abstract class RoundSettings
    {
        public abstract Enums.OpponentType OpponentType { get; set; }
    }

    [System.Serializable]
    public class DefaultGameSettings : RoundSettings
    {
        public override Enums.OpponentType OpponentType { get; set; }

        public DefaultGameSettings()
        {
            OpponentType = Enums.OpponentType.Unranked;
        }
    }
}
