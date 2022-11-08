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

        [Header("State Machine")]
        [SerializeField] private GameStateMachine stateMachine;
        public GameStateMachine StateMachine
        {
            get => stateMachine;
            private set => stateMachine = value;
        }

        [Header("Runtime Info")]
        [SerializeField] private List<Coach> coachesInGame;
        public List<Coach> CoachesInGame 
        { 
            get => coachesInGame;
            private set => coachesInGame = value;
        }

        [SerializeField] private ScoringTableScriptable scoringTable;
        public ScoringTableScriptable ScoringTable
        {
            get => scoringTable;
            private set => scoringTable = value;
        }

        [SerializeField] private bool isTiedGame = false;
        public bool IsTiedGame
        {
            get => isTiedGame;
            set => isTiedGame = value;
        }

        [SerializeField] private PokerScore winningScore;
        public PokerScore WinningScore
        {
            get => winningScore;
            set => winningScore = value;
        }

        #region Unity Methods & Initialization

        public void Awake()
        {
            GetComponents();    
        }

        private void GetComponents()
        {
            if (StateMachine == null)
                StateMachine = GetComponent<GameStateMachine>();
        }

        public void InitializeGame(List<Coach> coachesInGame)
        {
            CoachesInGame = coachesInGame;
            RegisterCoachesToGame();
            SetScoringTable();
        }

        private void RegisterCoachesToGame()
        {
            for (int i = 0; i < CoachesInGame.Count; i++)
            {
                if (i == 0)
                {
                    CoachesInGame[i].CurrentGame = this;
                    CoachesInGame[i].IsHomePlayer = true;
                }

                if (i == 1)
                {
                    CoachesInGame[i].CurrentGame = this;
                    CoachesInGame[i].IsHomePlayer = false;
                }
            }
        }

        public void SetScoringTable()
        {
            ScoringTable = ScoreManager.Instance.DefaultScoringTable;

            if (!ScoringTable)
                Debug.LogError("Error! No Scoring Table Found");
        }

        #endregion
    }
}
