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

        [Header("Round Settings")]
        [SerializeField] private Enums.RoundType roundType;
        public Enums.RoundType RoundType { get => roundType; set => roundType = value; }

        [SerializeField] private Enums.OpponentType opponentTypeOverride;
        public Enums.OpponentType OpponentTypeOverride { get => opponentTypeOverride; set => opponentTypeOverride = value; }

        #region Unity Methods

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Saving / Loading Round

        public void GenerateRoundOfGames(RoundSettings roundSettings)
        {
            if (gamePrefab == null)
            {
                Debug.LogError("Must have Game Prefab Set");
                return;
            }

            ClearRoundOfGames();
            CurrentRound = SeasonManager.Instance.CurrentSeason.RoundNumber;

            Enums.OpponentType _roundOpponent;

            if (roundSettings.RoundType == Enums.RoundType.OverrideSchedule) _roundOpponent = OpponentTypeOverride;
            else _roundOpponent = ScheduleManager.Instance.ScheduleScriptable.Opponents[CurrentRound];

            switch (_roundOpponent)
            {
                case Enums.OpponentType.Unranked:
                case Enums.OpponentType.Ranked:

                    for (int i = 0; i < SeasonManager.Instance.CurrentSeason.Players.Count; i++)
                    {
                        GameObject loadedGameObj = Instantiate(gamePrefab, this.transform);
                        Game loadedGame = loadedGameObj.GetComponent<Game>();

                        loadedGame.RoundSettings = roundSettings;
                        CurrentRoundOfGames.Add(loadedGame);
                    }

                    break;

                case Enums.OpponentType.HeadToHead:

                    int numberOfGames = (int) Math.Round((double)SeasonManager.Instance.CurrentSeason.Players.Count / 2, MidpointRounding.AwayFromZero);
                    Debug.Log("Round Manager creating " + numberOfGames + " number of games");
                    for (int i = 0; i < numberOfGames; i++)
                    {
                        GameObject loadedGameObj = Instantiate(gamePrefab, this.transform);
                        Game loadedGame = loadedGameObj.GetComponent<Game>();

                        loadedGame.RoundSettings = roundSettings;
                        CurrentRoundOfGames.Add(loadedGame);
                    }

                    break;

                default:
                    Debug.LogError("Opponent Type Not Recongnized");
                    break;
            }

            PlayerPoolManager.Instance.AssignPlayersToGames(CurrentRoundOfGames, _roundOpponent);
            AssignGameParents();
        }

        private void ClearRoundOfGames()
        {
            CurrentRoundOfGames.Clear();

            foreach (Transform transform in gameRoundSpawnPoint.transform)
                Destroy(transform.gameObject);
        }

        private void AssignGameParents()
        {
            foreach (Game game in CurrentRoundOfGames)
                game.transform.SetParent(gameRoundSpawnPoint);
        }

        #endregion
    }
}
