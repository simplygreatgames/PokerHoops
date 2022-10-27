using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance = null;

        [Header("Dependencies")]
        [SerializeField] private GameObject gamePrefab = null;
        [SerializeField] private Transform gameRoundSpawnPoint = null;

        [Header("Round Info")]
        [SerializeField] private int currentRound;
        public int CurrentRound { get => currentRound; set => currentRound = value; }

        [SerializeField] private List<Game> currentRoundOfGames;
        public List<Game> CurrentRoundOfGames
        {
            get => currentRoundOfGames;
            set => currentRoundOfGames = value;
        }

        [SerializeField] private List<Game> currentRoundOfGamesComplete;
        public List<Game> CurrentRoundOfGamesComplete
        {
            get => currentRoundOfGamesComplete;
            set => currentRoundOfGamesComplete = value;
        }

        [Header("Debug Settings")]
        [SerializeField] private Enums.RoundType roundType;
        public Enums.RoundType RoundType { get => roundType; set => roundType = value; }

        // Replace this with a Round Scriptable

        #region Unity Methods

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Saving / Loading Round

        public void GenerateRoundOfGames()
        {
            if (gamePrefab == null)
            {
                Debug.LogError("Must have Game Prefab Set");
                return;
            }

            ClearRoundOfGames();
            CurrentRound = SeasonManager.Instance.CurrentSeason.RoundNumber;

            Enums.OpponentType _roundOpponent;

            if (RoundType == Enums.RoundType.OverrideSchedule) Debug.Log("Not Implemented, ToDo if (Roundtype == Override) Use Round Scriptable");
            _roundOpponent = ScheduleManager.Instance.ScheduleScriptable.Opponents[CurrentRound];

            switch (_roundOpponent)
            {
                case Enums.OpponentType.Unranked:
                case Enums.OpponentType.Ranked:

                    for (int i = 0; i < SeasonManager.Instance.CurrentSeason.PlayerCoaches.Count; i++)
                    {
                        GameObject loadedGameObj = Instantiate(gamePrefab, this.transform);
                        Game loadedGame = loadedGameObj.GetComponent<Game>();

                        loadedGame.RoundSettings = new DefaultRoundSettings();
                        CurrentRoundOfGames.Add(loadedGame);
                    }

                    break;

                case Enums.OpponentType.HeadToHead:

                    int numberOfGames = (int) Math.Round((double)SeasonManager.Instance.CurrentSeason.PlayerCoaches.Count / 2, MidpointRounding.AwayFromZero);

                    for (int i = 0; i < numberOfGames; i++)
                    {
                        GameObject loadedGameObj = Instantiate(gamePrefab, this.transform);
                        Game loadedGame = loadedGameObj.GetComponent<Game>();

                        loadedGame.RoundSettings = new DefaultRoundSettings();
                        CurrentRoundOfGames.Add(loadedGame);
                    }

                    break;

                default:
                    Debug.LogError("Opponent Type Not Recongnized");
                    break;
            }

            PlayerPoolManager.Instance.AssignPlayersToGames(CurrentRoundOfGames, _roundOpponent);

            AssignActiveGame();
            AssignGameParents();
            StartRound();
        }

        private void ClearRoundOfGames()
        {
            CurrentRoundOfGames.Clear();

            foreach (Transform transform in gameRoundSpawnPoint.transform)
                Destroy(transform.gameObject);
        }

        private void AssignActiveGame()
        {
            foreach (var game in CurrentRoundOfGames)
            {
                foreach (var coach in game.CoachesInGame)
                {
                    if (coach is PlayerCoach)
                    {
                        PlayerCoach playerCoach = (PlayerCoach)coach;

                        if (playerCoach.IsLocalPlayer)
                            ActiveGameInterface.Instance.ActiveGame = game;
                    }
                }
            }
        }

        private void AssignGameParents()
        {
            foreach (Game game in CurrentRoundOfGames)
                game.transform.SetParent(gameRoundSpawnPoint);
        }

        #endregion

        #region Starting And Stopping A Round

        private void StartRound()
        {
            foreach (Game game in CurrentRoundOfGames)
            {
                game.StateMachine.SetGameState(new InitializeGameState(game.StateMachine));
            }
        }

        private void StopRound()
        {

        }

        private void MoveRound()
        {
            Debug.Log("Round of games are ready to move to next phase");
        }

        public void MarkGameComplete(Game gameCompleted)
        {
            if (!CurrentRoundOfGamesComplete.Contains(gameCompleted))
                CurrentRoundOfGamesComplete.Add(gameCompleted);

            if (CurrentRoundOfGamesComplete.Count == CurrentRoundOfGames.Count)
                MoveRound();

            else Debug.Log("Round completed! Waiting for other games to complete");
        }

        #endregion
    }
}
