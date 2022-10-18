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

        #region Unity Methods

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Saving / Loading Game

        public void GenerateRoundOfGames(RoundSettings roundSettings)
        {
            if (gamePrefab == null)
            {
                Debug.LogError("Must have Game Prefab Set");
                return;
            }

            ClearRoundOfGames();
            CurrentRound = SeasonManager.Instance.CurrentSeason.RoundNumber;

            switch (roundSettings.OpponentType)
            {
                case Enums.OpponentType.Unranked:

                    for (int i = 0; i < SeasonManager.Instance.CurrentSeason.Players.Count; i++)
                    {
                        GameObject loadedGameObj = Instantiate(gamePrefab, this.transform);
                        Game loadedGame = loadedGameObj.GetComponent<Game>();

                        loadedGame.GameSettings = roundSettings;
                        CurrentRoundOfGames.Add(loadedGame);
                    }

                    break;

                case Enums.OpponentType.Ranked:
                    break;

                case Enums.OpponentType.HeadToHead:
                    break;

                default:
                    break;
            }

            PlayerPoolManager.Instance.AssignPlayersToGames(CurrentRoundOfGames);
            AssignGameParents();
        }

        private void ClearRoundOfGames()
        {
            CurrentRoundOfGames.Clear();

            foreach (GameObject game in gameRoundSpawnPoint.transform)
                Destroy(game);
        }

        private void AssignGameParents()
        {
            foreach (Game game in CurrentRoundOfGames)
                game.transform.SetParent(gameRoundSpawnPoint);
        }

        #endregion
    }
}
