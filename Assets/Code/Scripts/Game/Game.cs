using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Game : MonoBehaviour
    {
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

        [Header("State Machine")]
        [SerializeField] private GameStateMachine gameStateMachine;
        public GameStateMachine GameStateMachine
        {
            get => gameStateMachine;
            private set => gameStateMachine = value;
        }

        public void InitializeSettings()
        {
            Debug.Log("Initializing Game Settings (Not Implemented)");
        }
    }

    public class GameSettings
    {

    }

    public class DefaultGameSettings : GameSettings
    {

    }
}
