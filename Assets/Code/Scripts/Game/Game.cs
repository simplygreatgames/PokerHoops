using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Game : MonoBehaviour
    {
        #region State & Data

        [Header("State Machine")]
        [SerializeField] private GameStateMachine gameStateMachine;
        public GameStateMachine GameStateMachine
        {
            get => gameStateMachine;
            private set => gameStateMachine = value;
        }

        #endregion
    }
}
